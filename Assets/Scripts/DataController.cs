/*
 	DataController.cs
 	Michael Stephens
 	
 	Created:		December 22, 2013
 	Last Edited:	February 13, 2014
 	
 	Controls saving and loading of game data.
*/


using UnityEngine;
using System.Collections;


public class DataController : MonoBehaviour 
{
	#region Variables
		
		#region Data
		
		// The list of level completion states
		private int [] highScores = new int [10];
		// The current sound effects volume level
		private float seVolume = 1;
		// The current state of music volume level
		private float musicVolume = 1;
		// The number of times played
		private int numberOfPlays = 0;
		// The number of platforms hit
		private int numberOfPlatsHit = 0;
		// The player's cumulative score
		private int totalScore = 0;
		// The new total score to be used for items
		private int totalScoreItemUsable = 0;
		// The camera resolution override type being used
		// 0 = fit, 1 = stretch
		private int camOverrideType = 0;
		// The achievement bools
		private bool doesHaveCheevo1 = false;
		private bool doesHaveCheevo2 = false;
		private bool doesHaveCheevo3 = false;
		private bool doesHaveCheevo4 = false;
		private bool doesHaveCheevo5 = false;
		
		#endregion
		
		#region Scripts
		
		// The leaderboard
		Leaderboard lb;
		
		#endregion
	
	#endregion
	
	
	// Used for initialization
	void Awake () 
	{
		//
		lb = GetComponent <Leaderboard> ();
		
		LoadHighscoreData ();
		LoadVolumeData ();
		LoadCamData ();
		LoadCheevoData ();
	}
	
	
	#region Load Data
	
	// Loads the level completion data, or creates some if there is none to load
	public void LoadHighscoreData ()
	{
		highScores = PlayerPrefsX.GetIntArray ("HighScores", 0, 10);
		numberOfPlays = PlayerPrefs.GetInt ("TimesPlayed", 0);
		numberOfPlatsHit = PlayerPrefs.GetInt ("PlatsHit", 0);
		totalScore = PlayerPrefs.GetInt ("TotalScore", 0);
		totalScoreItemUsable = PlayerPrefs.GetInt ("TotalScoreItemUsable", 0);
	}
	
	
	//
	//
	void LoadVolumeData ()
	{
		musicVolume = PlayerPrefs.GetFloat ("MusicVolume", 1);
		seVolume = PlayerPrefs.GetFloat ("SEVolume", 1);
	}
	
	
	//
	//
	void LoadCamData ()
	{
		camOverrideType = PlayerPrefs.GetInt ("CamType", 0);
	}
	
	
	//
	//
	void LoadCheevoData ()
	{
		doesHaveCheevo1 = PlayerPrefsX.GetBool ("Cheevo1", false);
		doesHaveCheevo2 = PlayerPrefsX.GetBool ("Cheevo2", false);
		doesHaveCheevo3 = PlayerPrefsX.GetBool ("Cheevo3", false);
		doesHaveCheevo4 = PlayerPrefsX.GetBool ("Cheevo4", false);
		doesHaveCheevo5 = PlayerPrefsX.GetBool ("Cheevo5", false);
	}
	
	
	//
	//
	public int [] GetHighScores ()
	{
		return highScores;
	}
	
	
	//
	//
	public int GetHighestScore ()
	{
		int h = 0;
		for (int i = 0; i < 10; i++)
		{
			if (highScores [i] > h)
				h = highScores [i];
		}
		return h;
	}
	
	
	//
	public float GetMusicVolume ()
	{
		return musicVolume;
	}
	
	
	//
	public float GetSEVolume ()
	{
		return seVolume;
	}
	
	
	//
	public int GetTimesPlayed ()
	{
		return numberOfPlays;
	}
	
	
	//
	public int GetPlatsHit ()
	{
		return numberOfPlatsHit;
	}
	
	
	//
	public int GetTotalScore ()
	{
		return totalScore;
	}
	
	
	//
	public int GetTotalScoreItemUsable ()
	{
		return totalScoreItemUsable;
	}
	
	
	//
	public int GetCamType ()
	{
		return camOverrideType;
	}
	
	
	//
	public bool GetDoesHaveCheevo (int n)
	{
		switch (n)
		{
			case 1: return doesHaveCheevo1; break;
			case 2: return doesHaveCheevo2; break;
			case 3: return doesHaveCheevo3; break;
			case 4: return doesHaveCheevo4; break;
			case 5: return doesHaveCheevo5; break;
		}
		return false;
	}
	
	#endregion
	
	
	#region Save Data
	
	// Saves the current level completion data, as well as the existence of data boolean
	public void SaveHighScoreData (int [] scores)
	{
		PlayerPrefsX.SetIntArray ("HighScores", scores);
		highScores = scores;
	}
	
	
	//
	public void SaveMusicVolume (float f)
	{
		musicVolume = f;
		PlayerPrefs.SetFloat ("MusicVolume", f);
	}
	
	
	//
	public void SaveSEVolume (float f)
	{
		seVolume = f;
		PlayerPrefs.SetFloat ("SEVolume", f);
	}
	
	
	//
	public void SaveCamOverrideType (int i)
	{
		camOverrideType = i;
		PlayerPrefs.SetInt ("CamType", i);
	}
	
	
	//
	public void IncrementNumberOfPlays ()
	{
		numberOfPlays += 1;
		PlayerPrefs.SetInt ("TimesPlayed", numberOfPlays);
	}
	
	
	//
	public void IncrementNumberOfPlatsHit ()
	{
		numberOfPlatsHit += 1;
		PlayerPrefs.SetInt ("PlatsHit", numberOfPlatsHit);
		
		//
		if (numberOfPlays >= 25 && !doesHaveCheevo4)
			lb.GiveAchievement (4);
	}
	
	
	//
	public void IncrementTotalScore (int toAdd)
	{
		totalScore += toAdd;
		PlayerPrefs.SetInt ("TotalScore", totalScore);
	}
	
	
	//
	public void IncrementTotalScoreItemUsable (int toAdd)
	{
		totalScoreItemUsable += toAdd;
		PlayerPrefs.SetInt ("TotalScoreItemUsable", totalScoreItemUsable);
	}
	
	
	// "Erases" all data (actually just sets values to defaults)
	// Called from Erase () in SettingsController.cs
	public void EraseData ()
	{
		// Create blank level data
		for (int i = 0; i < 10; ++i)
			highScores [i] = 0;
		PlayerPrefsX.SetIntArray ("HighScores", highScores);
		numberOfPlays = 0;
		PlayerPrefs.SetInt ("TimesPlayed", 0);
		numberOfPlatsHit = 0;
		PlayerPrefs.SetInt ("PlatsHit", 0);
		totalScore = 0;
		PlayerPrefs.SetInt ("TotalScore", 0);
		totalScoreItemUsable = 0;
		PlayerPrefs.SetInt ("TotalScoreItemUsable", 0);
	}
	
	
	//
	//
	public void SetCheevoGot (int num)
	{
		switch (num)
		{
			case 1:
				doesHaveCheevo1 = true;
				PlayerPrefsX.SetBool ("Cheevo1", true);
			break;
			case 2:
				doesHaveCheevo2 = true;
				PlayerPrefsX.SetBool ("Cheevo2", true);
				break;
			case 3:
				doesHaveCheevo3 = true;
				PlayerPrefsX.SetBool ("Cheevo3", true);
				break;
			case 4:
				doesHaveCheevo4 = true;
				PlayerPrefsX.SetBool ("Cheevo4", true);
				break;
			case 5:
				doesHaveCheevo5 = true;
				PlayerPrefsX.SetBool ("Cheevo5", true);
			break;
		}
	}
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundBegin += IncrementNumberOfPlays;
		EventManager.OnRoundRestart += IncrementNumberOfPlays;
		//EventManager.OnRoundEnd += GameEnded;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundBegin -= IncrementNumberOfPlays;
		EventManager.OnRoundRestart -= IncrementNumberOfPlays;
		//EventManager.OnRoundEnd -= GameEnded;
	}
	
	#endregion
}
