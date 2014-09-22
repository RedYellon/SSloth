/*
 	NewHighScoreLabel.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 13, 2014
 	Last Edited:	September 13, 2014
 	
 	Controls the appearance of the new high score label
 	that appears on the death screen.
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NewHighScoreLabel : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The letter texts
		public Transform [] letters;
		// The parent of the letters
		public Transform letterParent;
		// The target Y position for the letters
		public float targetYLetterPos;
		// The speed that the letters move down
		public float letterMoveSpeed = 10.0f;
		// The gap inbetween the start of each letter move
		public float letterMoveGapTime = 0.05f;
		// The maximum up and down movement of the letters
		public float maxUpAndDown = 1;
		// The bob speed of the letters
		public float bobSpeed = 50;
		
		#endregion
		
		#region Scripts
		
		//
		DeathScreen deathScreen;
		
		#endregion
		
		#region Private
		
		// The default Y position for the letters
		private float _defaultLetterYPos;
		// If the letters should be moving or not
		private bool _isMoving = false;
		// The number of letters that are currently moving
		private int _numberOfMovingLetters = 0;
		//
		private float angle = -90;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		if (_isMoving)
			MoveLetters ();
	}
	
	
	/*
	// Physics frame loop
	// Called automatically every physics frame
	void FixedUpdate ()
	{
		if (_isMoving)
			BobLetters ();
	}*/
	
	#endregion
	
	
	#region Movement
	
	// Moves all of the letters that need to be moved
	// Called every frame from Update ()
	void MoveLetters ()
	{
		// Move each letter that should be moving
		for (int i = 0; i < _numberOfMovingLetters; i++)
		{
			letters [i].localPosition = Vector3.Lerp (letters [i].localPosition, new Vector3 (letters [i].localPosition.x, targetYLetterPos, letters [i].localPosition.z), Time.deltaTime * letterMoveSpeed);
		}
	}
	
	
	//
	//
	void BobLetters ()
	{
		// Move each letter that should be moving
		for (int i = 0; i < _numberOfMovingLetters; i++)
		{
			angle += bobSpeed * Time.deltaTime;
			letters [i].localPosition = new Vector3 (letters [i].localPosition.x, (targetYLetterPos + maxUpAndDown * (1 + Mathf.Sin (angle * (Mathf.PI / 180))) / 2), letters [i].localPosition.z);
		}
	}
	
	
	// Returns the letters to their default positions
	// Called every frame from Update ()
	IEnumerator ReturnLetters ()
	{	
		while (letters [0].localPosition.y <= (_defaultLetterYPos - 0.1f))
		{
			for (int i = 0; i < letters.Length; i++)
				letters [i].localPosition = Vector3.Lerp (letters [i].localPosition, new Vector3 (letters [i].localPosition.x, _defaultLetterYPos, letters [i].localPosition.z), Time.deltaTime * letterMoveSpeed);
			
			yield return null;
		}
	}
	
	
	// Moves the next letter down
	//
	void MoveNextLetter ()
	{
		// Only move the next letter if there us a next letter to move
		if (_numberOfMovingLetters < letters.Length)
			_numberOfMovingLetters ++;
			
		// If we are on the last letter, we need to tell the death screen
		if (_numberOfMovingLetters == letters.Length - 1)
			deathScreen.GoHighScoreFlash ();
	}
	
	#endregion
	
	
	#region Public
	
	// Starts the label moving
	//
	public void BeginMove ()
	{
		_isMoving = true;
		_numberOfMovingLetters = 0;
		InvokeRepeating ("MoveNextLetter", 0.0f, letterMoveGapTime);
	}
	
	
	// Ends the movement
	//
	public void EndMove ()
	{
		CancelInvoke ();
		StartCoroutine ("ReturnLetters");
		_numberOfMovingLetters = 0;
		_isMoving = false;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		_defaultLetterYPos = letters [0].localPosition.y;
		deathScreen = GameObject.Find ("&MainController").GetComponent <DeathScreen> ();
	}
	
	#endregion
}
