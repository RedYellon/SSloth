/*
 	AnimalFriends.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 1, 2014
 	Last Edited:	December 14, 2014
 	
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
		// The reindeer sprites for the christmas update
		public tk2dSprite reindeerSprite;
		public tk2dSprite reindeerNoseSprite;
		// The winter scarf sprites for the animals
		public GameObject llamaScarf;
		public GameObject ostritchScarf;
		public GameObject giraffeScarf;
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
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
		
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
			case 3: reindeerNoseSprite.gameObject.SetActive (true); break;
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
			case 3: reindeerNoseSprite.gameObject.SetActive (false); break;
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
		// Reset sprites
		neckSprite.gameObject.SetActive (true);
		headSprite.gameObject.SetActive (true);
		reindeerSprite.gameObject.SetActive (false);
		reindeerNoseSprite.gameObject.SetActive (false);
		
		// Set a random animal sprite
		_animalNum = 0;
		if (_currentThemeIndex != 3) _animalNum = Random.Range (0, 3);
		else _animalNum = Random.Range (0, 4);

		// Activate the proper sprites for the chosen animal
		switch (_animalNum)
		{
			case 0: neckSprite.SetSprite ("ostritchNeck"); headSprite.SetSprite ("ostritchHead_1"); break;
			case 1: neckSprite.SetSprite ("llamaNeck"); headSprite.SetSprite ("llamaHead_1"); break;
			case 2: neckSprite.SetSprite ("giraffeNeck"); headSprite.SetSprite ("giraffeHead_1"); break;
			// The reindeer sprite will require us to do a little object manipulation
			case 3: 
				neckSprite.gameObject.SetActive (false);
				headSprite.gameObject.SetActive (false);
				reindeerSprite.gameObject.SetActive (true);
				reindeerNoseSprite.gameObject.SetActive (false);
			break;
		}

		// If we are in a correct theme, we need to add some sprites
		switch (_currentThemeIndex)
		{
			case 2: ActivateScarf (); break;
			case 3: ActivateScarf (); break;
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


	// Turns on the winter scarf for the appropriate animal
	// Called from GoMove ()
	void ActivateScarf ()
	{
		DeactivateScarfs ();
		switch (_animalNum)
		{
			case 0: ostritchScarf.SetActive (true); break;
			case 1: llamaScarf.SetActive (true); break;
			case 2: giraffeScarf.SetActive (true); break;
		}
	}


	// Turns off all the animal scarves
	// Called from ActivateScarf () and ChangeCurrentTheme (int index)
	void DeactivateScarfs ()
	{
		llamaScarf.SetActive (false);
		ostritchScarf.SetActive (false);
		giraffeScarf.SetActive (false);
	}
	

	//
	//
	IEnumerator WaitToGo ()
	{
		yield return new WaitForSeconds (currentWaitTime);
		GoMove ();
	}
	
	#endregion


	#region Themes

	//
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
		if (_currentThemeIndex == 1) DeactivateScarfs ();
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
