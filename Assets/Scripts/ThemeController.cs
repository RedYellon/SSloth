/*
 	ThemeController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 22, 2014
 	Last Edited:	November 23, 2014
 	
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

		// The snow particle controller
		ParticleSystem snowParticles;

		#endregion

		#region Scripts

		// The platfrom manager
		PlatformManager _platManager;
		// The player controller
		PlayerController _playerCont;
		// The snowflake manager
		SnowflakeManager _snowManager;

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
	}


	// Activates the winter theme
	// Called from ChangeTheme (ThemeType theme)
	void WinterThemeActivate ()
	{
		_platManager.SetSnowIsActive (true);
		_playerCont.SetSnowLanding (true);
		_snowManager.TurnOnSnowflakes ();
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
		_playerCont = GameObject.Find ("Player").GetComponent <PlayerController> ();

		// TEMP
		ChangeTheme (ThemeType.Winter);
	}
	
	#endregion
}
