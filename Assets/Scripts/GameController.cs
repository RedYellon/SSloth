/*
 	GameController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 14, 2014
 	Last Edited:	June 8, 2014
 	
 	Routes through all of the game-scene related scripts and
 	coordinates the game scene in general.
*/


using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour 
{
	#region Variables
	
		#region Scripts
		
		// The scene controller
		SceneController sceneCont;
		// The time controller
		TimeController timeCont;
		// The background and foreground
		ParallaxScrollController foreground;
		// The score controller
		ScoreController scoreCont;
		// The platform manager
		PlatformManager platManager;
		// The gui controller
		GuiController guiCont;
		// The camera controller
		CameraController camCont;
	
		#endregion
	
		#region Private
		
		// The sloth sprite
		private Renderer slothSprite;
	
		#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start () 
	{
		AssignVariables ();
	}
	
	#endregion
	
	
	#region Scene Switching
	
	// Sets up the game scene
	//
	public void Setup ()
	{
		// Switch camera mode
		//camCont.SetIsInMainMenuMode (false);
		
		// Turn on score
		scoreCont.gameObject.renderer.enabled = true;
		
		// Show renderers that were hidden
		slothSprite.enabled = true;
		foreground.SetForegroundVisible (true);
	}
	
	
	// Controls the transition to the main menu scene
	public void BackToMainMenu ()
	{
		Cleanup ();
		sceneCont.ChangeScene (1);
	}
	
	
	// Cleans up the game scene
	//
	private void Cleanup ()
	{
		// Make the player disappear
		slothSprite.enabled = false;
		
		// Turn off score
		scoreCont.gameObject.renderer.enabled = false;
		
		// Remove all GUI elements
		guiCont.Cleanup ();
		timeCont.Reset ();
		timeCont.SetIsChanging (false);
		foreground.SetForegroundVisible (false);
		
		// Reset platforms
		//platManager.Cleanup ();
	}
	
	#endregion
	
	
	#region Utility
	
	//
	//
	private void AssignVariables ()
	{
		// Scripts
		sceneCont = GetComponent <SceneController> ();
		timeCont = GetComponent <TimeController> ();
		platManager = GetComponent <PlatformManager> ();
		guiCont = GetComponent <GuiController> ();
		foreground = GameObject.Find ("Foreground Grass").GetComponent <ParallaxScrollController> ();
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		slothSprite = GameObject.Find ("SlothSprite").renderer;
	}
	
	#endregion
}
