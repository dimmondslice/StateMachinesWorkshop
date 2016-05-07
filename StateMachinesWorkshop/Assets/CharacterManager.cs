using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	public float walkSpeed;
	public float runSpeed;
	public float health;

	public CharacterState currentState { get; set; }
	public CharacterState previousState { get; set; }

	//////////////////////////////////////////////////////
	//
	//References to the character state components go here
	//
	//////////////////////////////////////////////////////


	//////////////////////////////////////////////////////
	void Awake()
	{

	}

	void Update()
	{
		currentState.UpdateState();
		currentState.ProcessInput();
		currentState.MovementMotor();
	}
}
