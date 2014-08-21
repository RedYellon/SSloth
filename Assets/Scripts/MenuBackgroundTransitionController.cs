/*
 	MenuBackgroundTransitionController.cs
 	Michael Stephens
 	
 	Created:		May 29, 2014
 	Last Edited:	May 29, 2014
 	
 	Coordinates the changing of the background during
 	main menu transitions.
*/


using UnityEngine;
using System.Collections;


public class MenuBackgroundTransitionController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The background color bars
		public ColorChange [] colorBars;
		// The rate that color bars advance
		public float barColorAdvanceRate = 0.1f;
		// The speed that the background color changes
		public float backgroundColorChangeSpeed = 10.0f;
		// The colors for the main menu
		public Color mainMenuBackgroundColor;
		public Color mainMenuBarColor;
		// The colors for the high scores menu
		public Color scoresBackgroundColor;
		public Color scoresBarColor;
		// The colors for the options menu
		public Color optionsBackgroundColor;
		public Color optionsBarColor;
		// The colors for the credits
		public Color creditsBackgroundColor;
		public Color creditsBarColor;
		
		#endregion
		
		#region Scripts
		
		
		
		#endregion
		
		#region References
		
		
		
		#endregion
		
		#region Private
		
		//
		private Color targetBarColor = Color.white;
		private Color targetBackgroundColor = Color.white;
		//
		private int colorBarChangingIndex = 0;
		// The main camera
		private Camera cam;
		//
		private bool isChanging = false;
		
		#endregion
		
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		if (isChanging)
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, targetBackgroundColor, Time.deltaTime * backgroundColorChangeSpeed);
	}
	
	#endregion
	
	
	#region Change Color
	
	//
	//
	public void ChangeToScene (int sceneNum)
	{
		// Get the correct target colors
		colorBarChangingIndex = 0;
		isChanging = true;
		switch (sceneNum)
		{
			// Main menu
			case 0:
				targetBarColor = mainMenuBarColor;
				targetBackgroundColor = mainMenuBackgroundColor;
			break;
			// Options
			case 1:
				targetBarColor = optionsBarColor;
				targetBackgroundColor = optionsBackgroundColor;
			break;
			// High scores
			case 2:
				targetBarColor = scoresBarColor;
				targetBackgroundColor = scoresBackgroundColor;
			break;
			case 3:
				return;
			break;
			// Credits
			case 5:
				targetBarColor = creditsBarColor;
				targetBackgroundColor = creditsBackgroundColor;
			break;
		}
		InvokeRepeating ("ColorBarChange", barColorAdvanceRate, barColorAdvanceRate);
	}
	
	
	//
	//
	void ColorBarChange ()
	{
		colorBars [colorBarChangingIndex].ChangeColors (targetBarColor);
		if (colorBarChangingIndex >= colorBars.Length - 1)
		{
			CancelInvoke ("ColorBarChange");
			colorBarChangingIndex = 0;
			cam.backgroundColor = targetBackgroundColor;
			isChanging = false;
		}
		else
		{
			colorBarChangingIndex++;
		}
	}
	
	#endregion
	
	
	#region Private
	
	//
	//
	public bool IsAlreadyOnMainMenuColors ()
	{
		if (cam.backgroundColor == mainMenuBackgroundColor)
			return true;
		else
			return false;
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
		targetBackgroundColor = mainMenuBackgroundColor;
		cam = GameObject.Find ("Main Camera").camera;
	}
	
	#endregion
}
