/*
 	TimeController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		-
 	Last Edited:	November 28, 2014
 	
 	Controls the changing of time.
*/


using UnityEngine;
using System.Collections;


public class TimeController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
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
		
		// The colors controller
		ColorsController _colors;
		// The score controller
		ScoreController scoreCont;
		// The audio controller
		AudioController audioCont;
		// The butterfly controller
		ButterflyBehavior [] _butterflies;
		// The firefly controllers
		FireflyBehavior [] _fireflies;
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
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
		
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
			// Change colors based on the current theme
			switch (_currentThemeIndex)
			{
				case 1: ChangeColorsNormal (); break;
				case 2: ChangeColorsWinter (); break;
				case 3: ChangeColorsWinter (); break;
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
					foreach (FireflyBehavior f in _fireflies) { f.SetIsActive (true); }
					foreach (ButterflyBehavior b in _butterflies) { b.SetIsActive (false); }
				break;
				// Dawn
				case 4:
					// Tell the audio controller to switch to day music
					audioCont.SetIsDayTime (true);
					
					// Enable day-specific grass ornaments
					foreach (ButterflyBehavior b in _butterflies) { b.SetIsActive (true); }
					foreach (FireflyBehavior f in _fireflies) { f.SetIsActive (false); }
					
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


	#region Color Change

	// Changes the colors for the normal default theme
	// Called from FixedUpdate ()
	void ChangeColorsNormal ()
	{
		switch (currentTime)
		{
			// Day
			case 1:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterDayColor, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectDayColor, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassDayColor, Time.deltaTime * changeSpeed); }
			break;
			// Dusk
			case 2:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterEveningColor, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectEveningColor, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassEveningColor, Time.deltaTime * changeSpeed); }
			break;
			// Night
			case 3:
			tinter.color = Color.Lerp (tinter.color, _colors.tinterNightColor, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.white, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectNightColor, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassNightColor, Time.deltaTime * changeSpeed); }
			break;
			// Dawn
			case 4:
			tinter.color = Color.Lerp (tinter.color, _colors.tinterMorningColor, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.clear, Time.deltaTime * changeSpeed * 3); }
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectMorningColor, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassMorningColor, Time.deltaTime * changeSpeed); }
			break;
		}
	}


	// Changes the colors for the winter/christmas themes
	// Called from FixedUpdate ()
	void ChangeColorsWinter ()
	{
		switch (currentTime)
		{
			// Day
			case 1:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterDayColorWinter, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectDayColorWinter, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassDayColorWinter, Time.deltaTime * changeSpeed); }
			break;
			// Dusk
			case 2:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterEveningColorWinter, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectEveningColorWinter, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassEveningColorWinter, Time.deltaTime * changeSpeed); }
			break;
			// Night
			case 3:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterNightColorWinter, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.white, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectNightColorWinter, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassNightColorWinter, Time.deltaTime * changeSpeed); }
			break;
			// Dawn
			case 4:
				tinter.color = Color.Lerp (tinter.color, _colors.tinterMorningColorWinter, Time.deltaTime * changeSpeed);
				foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.Lerp (sprite.color, Color.clear, Time.deltaTime * changeSpeed * 3); }
				foreach (tk2dSprite sprite in spritesToTint) { sprite.color = Color.Lerp (sprite.color, _colors.objectMorningColorWinter, Time.deltaTime * changeSpeed); }
				foreach (tk2dSprite sprite in grasses) { sprite.color = Color.Lerp (sprite.color, _colors.grassMorningColorWinter, Time.deltaTime * changeSpeed); }
			break;
		}
	}


	// Resets the object colors for the normal theme
	// Called from Reset ()
	void ResetColorsNormal ()
	{
		tinter.color = _colors.tinterDayColor;
		foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.clear; }
		foreach (tk2dSprite sprite in spritesToTint) { sprite.color = _colors.objectDayColor; }
		foreach (tk2dSprite sprite in grasses) { sprite.color = _colors.grassDayColor; }
	}


	// Resets the object colors for the winter theme
	// Called from Reset ()
	void ResetColorsWinter ()
	{
		tinter.color = _colors.tinterDayColorWinter;
		foreach (tk2dSprite sprite in backgroundStars) { sprite.color = Color.clear; }
		foreach (tk2dSprite sprite in spritesToTint) { sprite.color = _colors.objectDayColorWinter; }
		foreach (tk2dSprite sprite in grasses) { sprite.color = _colors.grassDayColorWinter; }
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

		// Reset colors based on the current theme
		switch (_currentThemeIndex)
		{
			case 1: ResetColorsNormal (); break;
			case 2: ResetColorsWinter (); break;
			case 3: ResetColorsWinter (); break;
		}
		
		// Reset other variables
		isChanging = true;
		audioCont.ResetToDay ();
		
		// Reset time-specific ornaments
		foreach (ButterflyBehavior b in _butterflies) { b.SetIsActive (true); }
		foreach (FireflyBehavior f in _fireflies) { f.SetIsActive (false); }
	}
	
	#endregion
	
	
	#region Getters
	
	public int GetCurrentTime () { return currentTime; }
	
	#endregion


	#region Setters

	//
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
	}

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
		_colors = GameObject.Find ("_ColorsController").GetComponent <ColorsController> ();
		tinter = GameObject.Find ("TimeTint").GetComponent <tk2dSprite> ();
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
		lb = GameObject.Find ("&MainController").GetComponent <Leaderboard> ();
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		_butterflies = FindObjectsOfType (typeof (ButterflyBehavior)) as ButterflyBehavior [];
		_fireflies = FindObjectsOfType (typeof (FireflyBehavior)) as FireflyBehavior [];
	}
	
	#endregion
}
