/*
 	CheatsController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 22, 2014
 	Last Edited:	December 14, 2014
 	
 	Controls the cheat menu and functions.
*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CheatsController : MonoBehaviour 
{
	#region Variables

		#region Public

			#region Cheats

			// The array of displays
			public Text [] displayTexts;
			// The time it takes to register a cheat
			public float registerTime = 1.0f;
			// The entry buttons parent
			public Transform entryButtonsParent;

			#endregion

			#region Animations

			public Animation cheatsEntryDropAnimation;

			#endregion

		// The renderer that is tinted for the cheats menu background
		public tk2dSprite cheatsMenuBackground;
		// The alpha value we tint to when the cheats menu is active
		public float cheatsBackgroundAlpha = 0.8f;
		// The things that appear/disappear when the cheat menu appears
		public GameObject [] activateObjectsDuringMenu;
		public GameObject [] deactivateObjectsDuringMenu;

		#endregion

		#region Scripts

		// The main menu controller
		MainMenuController _mainMenuCont;
		// The theme controller
		ThemeController _themeCont;
		// The audio controller
		AudioController _audioCont;

		#endregion

		#region Private

		// The cheats string that is entered
		private string _cheatEntry = "";
		// If a cheat is in the process of being "checked"
		private bool _isProcessing = false;
		// The beginning position of the entry buttons parent
		private Vector3 _entryButtonsStartPos;

		#endregion

	#endregion


	#region Button Presses
	
	// Initializes the cheats menu and cheat related stuff
	// Called directly from uGUI button press
	public void CheatButtonPressed ()
	{
		//
		_mainMenuCont.OptionsFoldoutButtonPressed ();

		// Change the background color and deactivate the necessary objects
		cheatsMenuBackground.color = new Color (cheatsMenuBackground.color.r, cheatsMenuBackground.color.g, cheatsMenuBackground.color.b, cheatsBackgroundAlpha);
		for (int i = 0; i < deactivateObjectsDuringMenu.Length; i++)
			deactivateObjectsDuringMenu [i].SetActive (false);
		for (int i = 0; i < activateObjectsDuringMenu.Length; i++)
			activateObjectsDuringMenu [i].SetActive (true);
	}


	// Exits the cheats menu
	// Called directly from uGUI button press
	public void ExitCheatsMenuButtonPressed ()
	{
		// If we are processing a cheat, we can't leave
		if (_isProcessing)
			return;

		// Reset cheat values
		for (int i = 0; i < displayTexts.Length; i++)
			displayTexts [i].text = "";
		_cheatEntry = "";
		_isProcessing = false;
		cheatsEntryDropAnimation.Stop ();
		entryButtonsParent.position = _entryButtonsStartPos;

		// Change the background color and deactivate the necessary objects
		cheatsMenuBackground.color = new Color (cheatsMenuBackground.color.r, cheatsMenuBackground.color.g, cheatsMenuBackground.color.b, 0.0f);
		for (int i = 0; i < deactivateObjectsDuringMenu.Length; i++)
			deactivateObjectsDuringMenu [i].SetActive (true);
		for (int i = 0; i < activateObjectsDuringMenu.Length; i++)
			activateObjectsDuringMenu [i].SetActive (false);
	}

	#endregion


	#region Entry

	// Enters a character into the current cheat string
	// Called directly from uGUI button presses
	public void CheatCharacterEntered (string s)
	{
		// If we have more than 6 characters already entered, we shouldn't be here
		if (_cheatEntry.Length >= 6)
			return;

		// Add the entered character to the end of the cheat string
		_cheatEntry += s;

		// Set the correct display character to the entered character
		int index = _cheatEntry.Length - 1;
		displayTexts [index].text = s;

		// If we are at the end, we should register the entered cheat
		if (_cheatEntry.Length >= 6)
		{
			_isProcessing = true;
			cheatsEntryDropAnimation.Play ();
			Invoke ("RegisterCheat", registerTime);
		}
	}


	// Registers the entered cheat
	// Called from CheatCharacterEntered (string s)
	void RegisterCheat ()
	{
		// Let's see if the cheat exists
		switch (_cheatEntry)
		{
			case "SLOTH1":
				CheatSucceeded ();
				Invoke ("SwitchThemeToNormal", 1.0f);
			break;

			case "SLOTH2":
				CheatSucceeded ();
				Invoke ("SwitchThemeToWinter", 1.0f);
			break;

			case "SLOTH3":
				CheatSucceeded ();
				Invoke ("SwitchThemeToHoliday", 1.0f);
			break;

			// If it doesn't match a valid cheat, we should get out
			default: CheatFailed (); break;
		}
	}


	// Displays failed cheat indicator
	// Called from RegisterCheat ()
	void CheatFailed ()
	{
		//



		//
		_audioCont.PlaySound ("CheatFailed");
		_isProcessing = false;
		ExitCheatsMenuButtonPressed ();
	}


	// Displays successful cheat indicators
	// Called from RegisterCheat ()
	void CheatSucceeded ()
	{
		_audioCont.PlaySound ("CheatSucceeded");
	}

	#endregion


	#region Cheats

	// The cheat to switch back to the normal theme
	// Called from RegisterCheat ()
	void SwitchThemeToNormal ()
	{
		_themeCont.ChangeTheme (ThemeController.ThemeType.Normal);

		// Exit cheats menu
		_isProcessing = false;
		ExitCheatsMenuButtonPressed ();
	}


	// The cheat to switch to the winter theme
	// Called from RegisterCheat ()
	void SwitchThemeToWinter ()
	{
		_themeCont.ChangeTheme (ThemeController.ThemeType.Winter);

		// Exit cheats menu
		_isProcessing = false;
		ExitCheatsMenuButtonPressed ();
	}


	// The cheat to switch to the Holiday theme
	// Called from RegisterCheat ()
	void SwitchThemeToHoliday ()
	{
		_themeCont.ChangeTheme (ThemeController.ThemeType.Christmas);
		
		// Exit cheats menu
		_isProcessing = false;
		ExitCheatsMenuButtonPressed ();
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
		_mainMenuCont = GetComponent <MainMenuController> ();
		_themeCont = GetComponent <ThemeController> ();
		_audioCont = GetComponent <AudioController> ();
		_entryButtonsStartPos = entryButtonsParent.position;
	}
	
	#endregion
}
