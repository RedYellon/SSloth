/*
 	SceneController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 14, 2014
 	Last Edited:	June 18, 2014
 	
 	Coordinates the changing of "scenes".
*/


using UnityEngine;
using System.Collections;


public class SceneController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The scene we start at
		public int startScene = 1;
		
		#endregion
		
		#region Scripts
		
		// The main menu controller
		MainMenuController mainMenuCont;
		// The game controller
		GameController gameCont;
		// The high score scene controller
		HighScoreSceneController highScoreCont;
		// The credits controller
		CreditsController creditsCont;
		// The options scene controller
		OptionsController optionsCont;
		// The bounce controller
		BounceObject cloudBounce;
		
		#endregion
		
		#region Private
		
		// The scene we are currently at
		private int currentScene = 1;
		
		#endregion
	
	#endregion
	
	/*
	#if UNITY_ANDROID
		#region Update
		
		//
		//
		void Update ()
		{
			// If we hit "back" on an android device
			if (Input.GetKeyDown (KeyCode.Escape))
			{ 
				ChangeScene (1);
			}
		}
		
		#endregion
	#endif
	*/
	
	#region Change Scene
	
	//
	// 1 = main menu, 2 = game, 3 = high scores, 4 = credits
	public void ChangeScene (int scene)
	{
		switch (scene)
		{
			// If we are changing to the main menu scene
			case 1:
				mainMenuCont.Setup ();
			break;
			// If we are changing to the game scene
			case 2:
				//gameCont.Setup ();
			break;
			// If we are changing to the high score scene
			case 3:
				highScoreCont.Setup ();
				cloudBounce.StopBounce ();
			break;
			// If we are changing to the credits scene
			case 4:
				creditsCont.Setup ();
				cloudBounce.StopBounce ();
			break;
			// If we are changing to the options scene
			case 5:
				optionsCont.Setup ();
				cloudBounce.StopBounce ();
			break;
		}
		currentScene = scene;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called initially at beginning
	void Awake () 
	{
		AssignVariables ();
	}
	
	
	// Assigns the initial variables
	// Called from Start ()
	private void AssignVariables ()
	{
		// Scripts
		mainMenuCont = gameObject.GetComponent <MainMenuController> ();
		gameCont = gameObject.GetComponent <GameController> ();
		highScoreCont = gameObject.GetComponent <HighScoreSceneController> ();
		creditsCont = gameObject.GetComponent <CreditsController> ();
		optionsCont = gameObject.GetComponent <OptionsController> ();
		
		currentScene = startScene;
		cloudBounce = GameObject.Find ("Cloudtops").GetComponent <BounceObject> ();
	}
	
	#endregion
}
