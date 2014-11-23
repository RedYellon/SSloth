/*
 	AnimalFriends.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 1, 2014
 	Last Edited:	November 23, 2014
 	
 	Controls the appearance of the animal friends.
*/


using UnityEngine;
using System.Collections;


public class AnimalFriends : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The head and neck sprites for the animal
		public tk2dSprite neckSprite;
		public tk2dSprite headSprite;
		// The animation for the animal
		public Animation animalAnimation;
		// The time between appearances
		public Vector2 timeBetween = new Vector2 (90, 180);
	
		#endregion
		
		#region Scripts
		
		// The audio controller
		AudioController audioCont;
		
		#endregion
		
		#region Private
		
		//
		private float currentWaitTime;
		// The current animal we are
		private int _animalNum = 0;
		
		#endregion
	
	#endregion


	#region Animation Events

	// Plays the initial "whoooo" sound
	public void PlayAppearSound ()
	{
		audioCont.PlaySound ("Dexter");
	}

	// Makes the animal blink
	public void Blink ()
	{
		switch (_animalNum)
		{
			case 0: headSprite.SetSprite ("ostritchHead_2"); break;
			case 1: headSprite.SetSprite ("llamaHead_2"); break;
			case 2: headSprite.SetSprite ("giraffeHead_2"); break;
		}
		audioCont.PlaySound ("AnimalBlink");
	}


	// Makes the animal "un-blink"
	public void Unblink ()
	{
		switch (_animalNum)
		{
			case 0: headSprite.SetSprite ("ostritchHead_1"); break;
			case 1: headSprite.SetSprite ("llamaHead_1"); break;
			case 2: headSprite.SetSprite ("giraffeHead_1"); break;
		}
	}


	// Plays the end "whoooosh" sound
	public void PlayDisappearSound ()
	{
		audioCont.PlaySound ("AnimalLeave");
	}


	// Resets the animation
	public void EndAnimation ()
	{
		// Stop the current animation
		animalAnimation.Stop ();

		// Let's go again!
		currentWaitTime = Random.Range (timeBetween.x, timeBetween.y);
		StartCoroutine ("WaitToGo");
	}

	#endregion
	
	
	#region Activation
	
	// Makes the object start moving to the target position
	// Called externally
	public void GoMove ()
	{
		// Set a random animal sprite
		_animalNum = Random.Range (0, 3);
		switch (_animalNum)
		{
			case 0: neckSprite.SetSprite ("ostritchNeck"); headSprite.SetSprite ("ostritchHead_1"); break;
			case 1: neckSprite.SetSprite ("llamaNeck"); headSprite.SetSprite ("llamaHead_1"); break;
			case 2: neckSprite.SetSprite ("giraffeNeck"); headSprite.SetSprite ("giraffeHead_1"); break;
		}

		// Set a random animation
		int n = Random.Range (0, 4);
		switch (n)
		{
			case 0: animalAnimation.Play ("AnimalAnimation1"); break;
			case 1: animalAnimation.Play ("AnimalAnimation2"); break;
			case 2: animalAnimation.Play ("AnimalAnimation3"); break;
			case 3: animalAnimation.Play ("AnimalAnimation4"); break;
		}
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
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
	}
	
	#endregion
}
