/*
 	ScreenClutterMovement.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 1, 2014
 	Last Edited:	September 1, 2014
 	
 	Moves this screen clutter thing.
*/


using UnityEngine;
using System.Collections;


public class ScreenClutterMovement : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		//
		public float moveSpeed = 6.0f;
		// The time between appearances
		public Vector2 timeBetween = new Vector2 (90, 180);
		// The time the object stays on-screen
		public float holdTime = 2.0f;
		// The possible sprites
		public string [] sprites;
		// The transforms
		public Transform  [] trans;
		// The target position of this transform
		public Vector3 [] targetPositions;
	
		#endregion
		
		#region Private
		
		//
		private bool isGoOut = false;
		private bool isComeIn = false;
		// The transform of the object currently active
		private Transform activeTrans;
		//
		private Vector3 returnPosition;
		private Vector3 targetPosition;
		//
		private float currentWaitTime;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called once per frame automatically by Unity
	void Update () 
	{
		//
		if (isGoOut)
			MoveObjectOnScreen ();
		else if (isComeIn)
			MoveObjectOffScreen ();
	}
	
	
	// Moves the object to the target position
	// Called every frame by Update ()
	void MoveObjectOnScreen ()
	{
		activeTrans.localPosition = Vector3.Lerp (activeTrans.localPosition, targetPosition, Time.deltaTime * moveSpeed);
	}
	
	
	//
	//
	void MoveObjectOffScreen ()
	{
		activeTrans.localPosition = Vector3.Lerp (activeTrans.localPosition, returnPosition, Time.deltaTime * (moveSpeed * 2));
	}
	
	#endregion
	
	
	#region Activation
	
	// Makes the object start moving to the target position
	// Called externally
	public void GoMove ()
	{
		// Get a random trans
		int randInt = Random.Range (0, trans.Length);
		activeTrans = trans [randInt];
		targetPosition = targetPositions [randInt];
		returnPosition = activeTrans.position;
		
		// Set a random sprite
		activeTrans.GetComponent <tk2dSprite> ().SetSprite (sprites [Random.Range (0, sprites.Length)]);
		
		// Les GOOOOOoooooOOoOOooooOOoOoOooo
		isGoOut = true;
		StartCoroutine ("WaitToReturn");
	}
	
	
	//
	//
	IEnumerator WaitToReturn ()
	{
		yield return new WaitForSeconds (holdTime);
		isGoOut = false;
		isComeIn = true;
		
		//
		currentWaitTime = Random.Range (timeBetween.x, timeBetween.y);
		StartCoroutine ("WaitToGo");
	}
	
	
	//
	//
	IEnumerator WaitToGo ()
	{
		yield return new WaitForSeconds (currentWaitTime);
		GoMove ();
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		//
		currentWaitTime = Random.Range (timeBetween.x, timeBetween.y);
		StartCoroutine ("WaitToGo");
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
	
	}
	
	#endregion
}
