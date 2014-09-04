/*
 	TimeController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		-
 	Last Edited:	June 6, 2014
 	
 	Controls the changing of time.
*/


using UnityEngine;
using System.Collections;


public class TimeController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
			#region Colors
		
			// The different color tints for the main tinter
			public Color tinterDayColor;
			public Color tinterEveningColor;
			public Color tinterNightColor;
			public Color tinterMorningColor;
			// The different color tints for the objects in front of (and thus unaffected by) the main tinter
			public Color objectDayColor;
			public Color objectEveningColor;
			public Color objectNightColor;
			public Color objectMorningColor;
			// The color of grass in the day
			public Color grassDayColor;
			
			#endregion
			
		// The array of background star sprites
		public tk2dSprite [] backgroundStars;
		// The array of sprites that need to be tinted along with the tinter
		public tk2dSprite [] spritesToTint;
		// The array of grass sprites
		public tk2dSprite [] grasses;
		// How far the player must go before time changes
		public int changeDistance = 20;
		// How quickly the transitions between times are
		public float changeSpeed = 2.0f;
	
		#endregion
		
		#region Scripts
		
		// The score controller
		ScoreController scoreCont;
		// The audio controller
		AudioController audioCont;
		// The butterfly controller
		ButterflyBehavior butterfly;
		// The firefly controller
		FireflyBehavior firefly;
		// The leaderboard controller
		Leaderboard lb;
		// The data controller 
		DataController dataCont;
		
		#endregion
		
		#region Private
		
		// The timter object
		private tk2dSprite tinter;
		// The current adjusted score
		private int scoreVar = 0;
		// The current multiplier (used for correcting transitional values)
		private int multiplier = 0;
		// The current time
		// 1 = day, 2 = sunset, 3 = night, 4 = sunrise
		private int currentTime = 1;
		// Whether or not time is currently advancing
		private bool isChanging = true;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Fixed logic loop
	// Called every fixed frame (and thus is framerate independent)
	void FixedUpdate ()
	{
		// If time is advancing,,,
		if (isChanging)
		{
			// Depending on the current time...
			switch (currentTime)
			{
				// Day
				case 1:
					tinter.color = Color.Lerp (tinter.color, tinterDayColor, Time.deltaTime * changeSpeed);
					foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, objectDayColor, Time.deltaTime * changeSpeed); }
					foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, grassDayColor, Time.deltaTime * changeSpeed); }
				break;
				// Dusk
				case 2:
					tinter.color = Color.Lerp (tinter.color, tinterEveningColor, Time.deltaTime * changeSpeed);
					foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, objectEveningColor, Time.deltaTime * changeSpeed); }
					foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, objectEveningColor, Time.deltaTime * changeSpeed); }
				break;
				// Night
				case 3:
					tinter.color = Color.Lerp (tinter.color, tinterNightColor, Time.deltaTime * changeSpeed);
					foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.white, Time.deltaTime * changeSpeed); }
					foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, objectNightColor, Time.deltaTime * changeSpeed); }
					foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, objectNightColor, Time.deltaTime * changeSpeed); }
				break;
				// Dawn
				case 4:
					tinter.color = Color.Lerp (tinter.color, tinterMorningColor, Time.deltaTime * changeSpeed);
					foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.clear, Time.deltaTime * changeSpeed * 3); }
					foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, objectMorningColor, Time.deltaTime * changeSpeed); }
					foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, objectMorningColor, Time.deltaTime * changeSpeed); }
				break;
			}
			
			// Check to see if the time of day should be changing
			CheckForTimeChange ();
		}
	}
	
	
	// Checks the current score to determine if the time of day should change yet
	// Called every fixed frame from FixedUpdate (), assuming isChanging is true
	void CheckForTimeChange ()
	{
		// If the player's adjusted score is high enough...
		if (scoreVar >= changeDistance)
		{
			// Advance the current time
			currentTime++;
			
			// Depending on the time of day we are entering...
			switch (currentTime)
			{
				// Night
				case 3:
					// Tell the audio controller to switch to night music
					audioCont.SetIsDayTime (false); 
					
					// Enable night-specific grass ornaments
					firefly.SetIsActive (true);
					butterfly.SetIsActive (false);
				break;
				// Dawn
				case 4:
					// Tell the audio controller to switch to day music
					audioCont.SetIsDayTime (true);
					
					// Enable day-specific grass ornaments
					butterfly.SetIsActive (true);
					firefly.SetIsActive (false);
					
					// We've survived the night!
					dataCont.IncrementNightsSurvived (1);
				break;
				// Reset
				case 5:
					currentTime = 1;
					lb.GiveAchievement (1);
				break;
			}
			
			// Reset variables
			scoreVar = 0;
			multiplier++;
		}
		else
		{
			scoreVar = scoreCont.GetPlayerScoreInt () - (multiplier * changeDistance);
		}
	}
	
	#endregion
	
	
	#region Setup/Cleanup
	
	// Sets whether or not time should be changing
	//
	public void SetIsChanging (bool b)
	{
		isChanging = b;
		CancelInvoke ();
	}
	
	
	void StopIsChanging ()
	{
		isChanging = false;
		CancelInvoke ();
	}
	
	
	// Resets the time to the beginning of day
	//
	public void Reset ()
	{
		// Reset variables
		multiplier = 0;
		scoreVar = 0;
		currentTime = 1;
		
		// Reset colors
		tinter.color = tinterDayColor;
		foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.clear; }
		foreach (tk2dSprite sprite in spritesToTint) { sprite.color = objectDayColor; }
		foreach (tk2dSprite sprite in grasses) { sprite.color = grassDayColor; }
		isChanging = true;
		audioCont.ResetToDay ();
		
		// Reset time-specific ornaments
		butterfly.SetIsActive (true);
		firefly.SetIsActive (false);
	}
	
	#endregion
	
	
	#region Getters
	
	public int GetCurrentTime () { return currentTime; }
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundBegin += Reset;
		EventManager.OnRoundRestart += Reset;
		EventManager.OnRoundEnd += StopIsChanging;
		EventManager.OnBackToMainMenuFromGame += Reset;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundBegin -= Reset;
		EventManager.OnRoundRestart -= Reset;
		EventManager.OnRoundEnd -= StopIsChanging;
		EventManager.OnBackToMainMenuFromGame -= Reset;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning of game
	void Start () 
	{
		AssignVariables ();
	}
	
	
	// Assigns and caches initial variables
	// Called from Start ()
	void AssignVariables ()
	{
		tinter = GameObject.Find ("TimeTint").GetComponent <tk2dSprite> ();
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
		lb = GameObject.Find ("&MainController").GetComponent <Leaderboard> ();
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		butterfly = GameObject.Find ("Butterfly").GetComponent <ButterflyBehavior> ();
		firefly = GameObject.Find ("Firefly").GetComponent <FireflyBehavior> ();
	}
	
	#endregion
}
