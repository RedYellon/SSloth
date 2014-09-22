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
		private string [] highScoresTimes = new string [10];
		// The current sound effects volume level
		private float seVolume = 1;
		// The current state of music volume level
		private float musicVolume = 1;
		
			#region Stats
			
			private int numberOfPlays = 0;
			private int numberOfPlatsHit = 0;
			private int totalScore = 0;
			private int secondsPlayed = 0;
			private int jumps = 0;
			private int doubleJumps = 0;
			private int landings = 0;
			private int angryPlatsHit = 0;
			private int dirtyPlatsHit = 0;
			private int sadPlatsHit = 0;
			private int totalStars = 0;
			private int goldStars = 0;
			private int silverStars = 0;
			private int bronzeStars = 0;
			private int nightsSurvived = 0;
			private int recordsBroken = 0;
			private int balloonsSeen = 0;
			private int butterfliesSeen = 0;
			private int firefliesSeen = 0;
			private int animalsSeen = 0;
			private int factsLearned = 0;
			private string factsIndex = "";
			private float peakAir = 0;
			private float maxHangtime = 0;
			
			#endregion
		
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
		highScoresTimes = PlayerPrefsX.GetStringArray ("HighScoresTimes", "", 10);
		numberOfPlays = PlayerPrefs.GetInt ("TimesPlayed", 0);
		numberOfPlatsHit = PlayerPrefs.GetInt ("PlatsHit", 0);
		totalScore = PlayerPrefs.GetInt ("TotalScore", 0);
		totalScoreItemUsable = PlayerPrefs.GetInt ("TotalScoreItemUsable", 0);
		secondsPlayed = PlayerPrefs.GetInt ("SecondsPlayed", 0);
		jumps = PlayerPrefs.GetInt ("Jumps", 0);
		doubleJumps = PlayerPrefs.GetInt ("DoubleJumps", 0);
		landings = PlayerPrefs.GetInt ("Landings", 0);
		angryPlatsHit = PlayerPrefs.GetInt ("AngryPlatsHit", 0);
		dirtyPlatsHit = PlayerPrefs.GetInt ("DirtyPlatsHit", 0);
		sadPlatsHit = PlayerPrefs.GetInt ("SadPlatsHit", 0);
		totalStars = PlayerPrefs.GetInt ("TotalStars", 0);
		goldStars = PlayerPrefs.GetInt ("GoldStars", 0);
		silverStars = PlayerPrefs.GetInt ("SilverStars", 0);
		bronzeStars = PlayerPrefs.GetInt ("BronzeStars", 0);
		nightsSurvived = PlayerPrefs.GetInt ("NightsSurvived", 0);
		recordsBroken = PlayerPrefs.GetInt ("RecordsBroken", 0);
		balloonsSeen = PlayerPrefs.GetInt ("BalloonsSeen", 0);
		butterfliesSeen = PlayerPrefs.GetInt ("ButterfliesSeen", 0);
		firefliesSeen = PlayerPrefs.GetInt ("FirefliesSeen", 0);
		animalsSeen = PlayerPrefs.GetInt ("AnimalsSeen", 0);
		factsLearned = PlayerPrefs.GetInt ("FactsLearned", 0);
		factsIndex = PlayerPrefs.GetString ("FactsIndex", "");
		peakAir = PlayerPrefs.GetFloat ("PeakAir", 0);
		maxHangtime = PlayerPrefs.GetFloat ("MaxHangtime", 0);
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
	public string [] GetHighScoresTimes ()
	{
		return highScoresTimes;
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
	public int GetTimesPlayed () { return numberOfPlays; }
	public int GetPlatsHit () { return numberOfPlatsHit; }
	public int GetTotalScore () { return totalScore; }
	public int GetSecondsPlayed () { return secondsPlayed; }
	public int GetJumps () { return jumps; }
	public int GetDoubleJumps () { return doubleJumps; }
	public int GetLandings () { return landings; }
	public int GetAngryPlatsHit () { return angryPlatsHit; }
	public int GetDirtyPlatsHit () { return dirtyPlatsHit; }
	public int GetSadPlatsHit () { return sadPlatsHit; }
	public int GetTotalStars () { return totalStars; }
	public int GetGoldStars () { return goldStars; }
	public int GetSilverStars () { return silverStars; }
	public int GetBronzeStars () { return bronzeStars; }
	public int GetNightsSurvived () { return nightsSurvived; }
	public int GetRecordsBroken () { return recordsBroken; }
	public int GetBalloonsSeen () { return balloonsSeen; }
	public int GetButterfliesSeen () { return butterfliesSeen; }
	public int GetFirefliesSeen () { return firefliesSeen; }
	public int GetAnimalsSeen () { return animalsSeen; }
	public int GetFactsLearned () { return factsLearned; }
	public float GetPeakAir () { return peakAir; }
	public float GetMaxHangtime () { return maxHangtime; }
	
	
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
	
	
	public void SaveHighScoreTimeData (string [] scoresTimes)
	{
		PlayerPrefsX.SetStringArray ("HighScoresTimes", scoresTimes);
		highScoresTimes = scoresTimes;
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
	
	public void AddToSecondsPlayed (int toAdd)
	{
		secondsPlayed += toAdd;
		PlayerPrefs.SetInt ("SecondsPlayed", secondsPlayed);
	}
	public void IncrementJumps (int toAdd)
	{
		jumps += toAdd;
		PlayerPrefs.SetInt ("Jumps", jumps);
	}
	public void IncrementDoubleJumps (int toAdd)
	{
		doubleJumps += toAdd;
		PlayerPrefs.SetInt ("DoubleJumps", doubleJumps);
	}
	public void IncrementLandings (int toAdd)
	{
		landings += toAdd;
		PlayerPrefs.SetInt ("Landings", landings);
	}
	public void IncrementAngryPlatsHit (int toAdd)
	{
		angryPlatsHit += toAdd;
		PlayerPrefs.SetInt ("AngryPlatsHit", angryPlatsHit);
	}
	public void IncrementDirtyPlatsHit (int toAdd)
	{
		dirtyPlatsHit += toAdd;
		PlayerPrefs.SetInt ("DirtyPlatsHit", dirtyPlatsHit);
	}
	public void IncrementSadPlatsHit (int toAdd)
	{
		sadPlatsHit += toAdd;
		PlayerPrefs.SetInt ("SadPlatsHit", sadPlatsHit);
	}
	public void IncrementTotalStars (int toAdd)
	{
		totalStars += toAdd;
		PlayerPrefs.SetInt ("TotalStars", totalStars);
	}
	public void IncrementGoldStars (int toAdd)
	{
		goldStars += toAdd;
		PlayerPrefs.SetInt ("GoldStars", goldStars);
	}
	public void IncrementSilverStars (int toAdd)
	{
		silverStars += toAdd;
		PlayerPrefs.SetInt ("SilverStars", silverStars);
	}
	public void IncrementBronzeStars (int toAdd)
	{
		bronzeStars += toAdd;
		PlayerPrefs.SetInt ("BronzeStars", bronzeStars);
	}
	public void IncrementNightsSurvived (int toAdd)
	{
		nightsSurvived += toAdd;
		PlayerPrefs.SetInt ("NightsSurvived", nightsSurvived);
	}
	public void IncrementRecordsBroken (int toAdd)
	{
		recordsBroken += toAdd;
		PlayerPrefs.SetInt ("RecordsBroken", recordsBroken);
	}
	public void IncrementBalloonsSeen (int toAdd)
	{
		balloonsSeen += toAdd;
		PlayerPrefs.SetInt ("BalloonsSeen", balloonsSeen);
	}
	public void IncrementButterfliesSeen (int toAdd)
	{
		butterfliesSeen += toAdd;
		PlayerPrefs.SetInt ("ButterfliesSeen", butterfliesSeen);
	}
	public void IncrementFirefliesSeen (int toAdd)
	{
		firefliesSeen += toAdd;
		PlayerPrefs.SetInt ("FirefliesSeen", firefliesSeen);
	}
	public void IncrementAnimalsSeen (int toAdd)
	{
		animalsSeen += toAdd;
		PlayerPrefs.SetInt ("AnimalsSeen", animalsSeen);
	}
	public void CheckFactLearned (int factNum)
	{
		string fns = factNum.ToString ();
		if (!factsIndex.Contains ("," + fns + ","))
		{
			factsIndex += "," + fns + ",";
			factsLearned ++;
			PlayerPrefs.SetInt ("FactsLearned", factsLearned);
			PlayerPrefs.SetString ("FactsIndex", factsIndex);
		}
	}
	public void CheckPeakAir (float f)
	{
		if (f > peakAir)
		{
			peakAir = f;
			PlayerPrefs.SetFloat ("PeakAir", peakAir);
		}
	}
	public void CheckMaxHangtime (float i)
	{
		if (i > maxHangtime)
		{
			maxHangtime = i;
			PlayerPrefs.SetFloat ("MaxHangtime", maxHangtime);
		}
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
		for (int i = 0; i < 10; ++i)
			highScoresTimes [i] = "";
		PlayerPrefsX.SetStringArray ("HighScoresTimes", highScoresTimes);
		numberOfPlays = 0;
		PlayerPrefs.SetInt ("TimesPlayed", 0);
		numberOfPlatsHit = 0;
		PlayerPrefs.SetInt ("PlatsHit", 0);
		totalScore = 0;
		PlayerPrefs.SetInt ("TotalScore", 0);
		secondsPlayed = 0;
		PlayerPrefs.SetInt ("SecondsPlayed", 0);
		totalScoreItemUsable = 0;
		PlayerPrefs.SetInt ("TotalScoreItemUsable", 0);
		secondsPlayed = 0;
		PlayerPrefs.SetInt ("SecondsPlayed", 0);
		jumps = 0;
		PlayerPrefs.SetInt ("Jumps", 0);
		doubleJumps = 0;
		PlayerPrefs.SetInt ("DoubleJumps", 0);
		landings = 0;
		PlayerPrefs.SetInt ("Landings", 0);
		angryPlatsHit = 0;
		PlayerPrefs.SetInt ("AngryPlatsHit", 0);
		dirtyPlatsHit = 0;
		PlayerPrefs.SetInt ("DirtyPlatsHit", 0);
		sadPlatsHit = 0;
		PlayerPrefs.SetInt ("SadPlatsHit", 0);
		totalStars = 0;
		PlayerPrefs.SetInt ("TotalStars", 0);
		goldStars = 0;
		PlayerPrefs.SetInt ("GoldStars", 0);
		silverStars = 0;
		PlayerPrefs.SetInt ("SilverStars", 0);
		bronzeStars = 0;
		PlayerPrefs.SetInt ("BronzeStars", 0);
		nightsSurvived = 0;
		PlayerPrefs.SetInt ("NightsSurvived", 0);
		recordsBroken = 0;
		PlayerPrefs.SetInt ("RecordsBroken", 0);
		balloonsSeen = 0;
		PlayerPrefs.SetInt ("BalloonsSeen", 0);
		butterfliesSeen = 0;
		PlayerPrefs.SetInt ("ButterfliesSeen", 0);
		firefliesSeen = 0;
		PlayerPrefs.SetInt ("FirefliesSeen", 0);
		animalsSeen = 0;
		PlayerPrefs.SetInt ("AnimalsSeen", 0);
		totalScoreItemUsable = 0;
		PlayerPrefs.SetInt ("TotalScoreItemUsable", 0);
		factsLearned = 0;
		PlayerPrefs.SetInt ("FactsLearned", 0);
		factsIndex = "";
		PlayerPrefs.SetString ("FactsIndex", "");
		peakAir = 0;
		PlayerPrefs.SetFloat ("PeakAir", 0);
		maxHangtime = 0;
		PlayerPrefs.SetFloat ("MaxHangtime", 0);
		
		
		//PlayerPrefs.DeleteAll ();
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
