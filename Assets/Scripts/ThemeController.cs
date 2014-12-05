/*
 	ThemeController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 22, 2014
 	Last Edited:	November 28, 2014
 	
 	Controls the changing and coordination of the different
 	visual themes.
*/


using UnityEngine;
using System.Collections;


public class ThemeController : MonoBehaviour 
{
	#region Variables

		#region Public

		// The possible "themes"
		public enum ThemeType
		{
			Normal,
			Winter,
			Christmas,
			Rasta
		}
		public ThemeType currentTheme = ThemeType.Normal;

			#region Winter

			// The snow particle controller
			ParticleSystem snowParticles;
			// The title words
			tk2dSprite titleSuper;
			tk2dSprite titleSloth;

			#endregion

		#endregion

		#region Scripts

		// The platfrom manager
		PlatformManager _platManager;
		// The player controller
		PlayerController _playerCont;
		// The snowflake manager
		SnowflakeManager _snowManager;
		// The grass controller
		ParallaxScrollController _grassCont;
		// The time controller
		TimeController _timeCont;
		// The camera controller
		CameraController _camCont;
		// The menu transition controller
		MenuBackgroundTransitionController _menuTransitionCont;
		// The animal friends script
		AnimalFriends _animals;

		#endregion

	#endregion


	#region Theme Changing

	// Changes the current theme
	public void ChangeTheme (ThemeType theme)
	{
		currentTheme = theme;
		switch (currentTheme)
		{
			case ThemeType.Normal: NormalThemeActivate (); break;
			case ThemeType.Winter: WinterThemeActivate (); break;
		}
	}


	// Activates the normal theme
	// Called from ChangeTheme (ThemeType theme)
	void NormalThemeActivate ()
	{
		_platManager.SetSnowIsActive (false);
		_playerCont.SetSnowLanding (false);
		_snowManager.TurnOffSnowflakes ();
		_grassCont.ChangeCurrentTheme (1);
		_timeCont.ChangeCurrentTheme (1);
		_camCont.ChangeCurrentTheme (1);
		_menuTransitionCont.ChangeCurrentTheme (1);
		_animals.ChangeCurrentTheme (1);
		titleSuper.SetSprite ("Super");
		titleSloth.SetSprite ("Sloth");
	}


	// Activates the winter theme
	// Called from ChangeTheme (ThemeType theme)
	void WinterThemeActivate ()
	{
		_platManager.SetSnowIsActive (true);
		_playerCont.SetSnowLanding (true);
		_snowManager.TurnOnSnowflakes ();
		_grassCont.ChangeCurrentTheme (2);
		_timeCont.ChangeCurrentTheme (2);
		_camCont.ChangeCurrentTheme (2);
		_menuTransitionCont.ChangeCurrentTheme (2);
		_animals.ChangeCurrentTheme (2);
		titleSuper.SetSprite ("Super_Winter");
		titleSloth.SetSprite ("Sloth_Winter");
	}

	#endregion


	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start ()  
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns initial variables
	// Called initially from Start ()
	private void AssignVariables ()
	{
		_platManager = GetComponent <PlatformManager> ();
		_snowManager = GetComponent <SnowflakeManager> ();
		_timeCont = GetComponent <TimeController> ();
		_menuTransitionCont = GetComponent <MenuBackgroundTransitionController> ();
		_camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		_playerCont = GameObject.Find ("Player").GetComponent <PlayerController> ();
		_grassCont = GameObject.Find ("Foreground Grass").GetComponent <ParallaxScrollController> ();
		titleSuper = GameObject.Find ("SUPER").GetComponent <tk2dSprite> ();
		titleSloth = GameObject.Find ("SLOTH").GetComponent <tk2dSprite> ();
		_animals = GameObject.Find ("Animal").GetComponent <AnimalFriends> ();

		// TEMP
		ChangeTheme (ThemeType.Winter);
	}
	
	#endregion
}
