/*
 	MenuBackgroundTransitionController.cs
 	Michael Stephens
 	
 	Created:		May 29, 2014
 	Last Edited:	November 28, 2014
 	
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
		
		#endregion

		#region Scripts

		// The colors controller
		ColorsController _colors;

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
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
		
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
				//	targetBarColor = mainMenuBarColor;
				//targetBackgroundColor = mainMenuBackgroundColor;
				ChangeToMainMenuColors ();
			break;
			// High scores
			case 2:
				///targetBarColor = scoresBarColor;
				//targetBackgroundColor = scoresBackgroundColor;
				ChangeToScoresMenuColors ();
			break;
			case 3:
				return;
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


	// Changes the main menu colors
	void ChangeToMainMenuColors ()
	{
		switch (_currentThemeIndex)
		{
			case 1:
				targetBarColor = _colors.stripesColor;
				targetBackgroundColor = _colors.backgroundColor;
			break;
			case 2:
				targetBarColor = _colors.stripesColorWinter;
				targetBackgroundColor = _colors.backgroundColorWinter;
			break;
		}
	}


	// Changes the stats menu colors
	void ChangeToScoresMenuColors ()
	{
		switch (_currentThemeIndex)
		{
			case 1:
				targetBarColor = _colors.statsStripesColor;
				targetBackgroundColor = _colors.statsBackgroundColor;
			break;
			case 2:
				targetBarColor = _colors.statsStripesColorWinter;
				targetBackgroundColor = _colors.statsBackgroundColorWinter;
			break;
		}
	}
	
	#endregion
	
	
	#region Private
	
	//
	//
	public bool IsAlreadyOnMainMenuColors ()
	{
		if (cam.backgroundColor == targetBackgroundColor)
			return true;
		else
			return false;
	}
	
	#endregion


	#region Public

	//
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
		switch (_currentThemeIndex)
		{
			case 1:
				targetBarColor = _colors.stripesColor;
				targetBackgroundColor = _colors.backgroundColor;
			break;
			case 2:
				targetBarColor = _colors.stripesColorWinter;
				targetBackgroundColor = _colors.backgroundColorWinter;
			break;
			case 3:
				targetBarColor = _colors.stripesColorWinter;
				targetBackgroundColor = _colors.backgroundColorWinter;
			break;
		}
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
		cam = GameObject.Find ("Main Camera").camera;
		_colors = GameObject.Find ("_ColorsController").GetComponent <ColorsController> ();
		targetBackgroundColor = _colors.backgroundColor;
	}
	
	#endregion
}
