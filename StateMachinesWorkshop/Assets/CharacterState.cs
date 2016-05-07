using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterManager))]
public abstract class CharacterState : MonoBehaviour
{
	//reference to this gameobjects rigidbody
	private Rigidbody rb;
	//can this behavior be used
	private bool canUse;
	//reference to the characterManager on this gameobject
	private CharacterManager charMan;
	//number used to converts number of frames at 60fps to number of seconds
	public float FPS60ToSeconds = .016667f;
	//time since this state has started most recently, reset on changeState()
	public float timeSinceStarted { get; private set; }

	protected virtual void Awake()
	{
		enabled = false;        //every state starts disabled, and is enabled when needed with ChangeState()
		canUse = true;
		charMan = GetComponent<CharacterManager>();
		rb = GetComponent<Rigidbody>();
		timeSinceStarted = 0f;
	}
	//called when the state is activated
	protected abstract void OnEnable();
	//called when the state is deactivated
	protected abstract void OnDisable();
	//Used to change the character manager current state to newState, also stores the last used state as previousState
	public bool ChangeState(CharacterState newState)
	{
		if(newState.GetCanUse())
		{
			charMan.previousState = charMan.currentState;
			charMan.currentState.enabled = false;
			newState.timeSinceStarted = 0f;
			charMan.currentState = newState;
			newState.enabled = true;
			return true;
		}
		else return false;
	}
	//returns whether or not this state can be transitioned into at this time
	public virtual bool GetCanUse() { return canUse; }
	//called from characterManager every frame. Use this function instead of update
	public abstract void UpdateState();
	//called from characterManager every frame. use this for logic involving input
	public abstract void ProcessInput();
	//Called from characgerManager ever frame. Use this for logic involving moving
	public abstract void MovementMotor();
}
