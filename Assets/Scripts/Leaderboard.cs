using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;


public class Leaderboard : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		
		
		#endregion
		
		#region Scripts
		
		DataController dataCont;
		
		#endregion
		
		#region References
		
		
		
		#endregion
		
		#region Private
		
		// The name of the leaderboard to report to
		private string lbName = "lb3";
		//
		private string [] _lbStrings;
		
		#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		//
		/*if (Application.platform == RuntimePlatform.Android)
		{
			// Activate google play stuff
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate ();
			
			lbName = "CgkInZaV_KAKEAIQAA";
		}
		else*/ //if (Application.platform == RuntimePlatform.IPhonePlayer)
			//lbName = "lb3";
		
		// Authenticate the local user
		UM_GameServiceManager.instance.Connect ();
		//Social.localUser.Authenticate (ProcessAuthentication);
		
		/*if (Application.platform == RuntimePlatform.Android)
			GooglePlayConnection.instance.connect ();
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			GameCenterManager.init ();*/
			
			
	}
	
	
	// Processes the authentication of the local user
	// Called from Start ()
	void ProcessAuthentication (bool success) 
	{
		// If the authentication is a success...
		if (success) 
		{
			Debug.Log ("Authenticated, checking scores");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadScores (lbName, ProcessLoadedScores);
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}
	
	
	// Processes the loaded scores
	// This function gets called when the LoadScores call completes
	void ProcessLoadedScores (IScore [] scores) 
	{
		if (scores.Length == 0)
			Debug.Log ("Error: no scores found");
		else
			Debug.Log ("Got " + scores.Length + " scores");
	}
	
	
	// Processes the loaded achievements
	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement [] achievements) 
	{
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
	}
	
	#endregion
	
	
	#region Public
	
	//
	//
	public void ReportScore (int score) 
	{
		/*
		Debug.Log ("Reporting score " + score + " on leaderboard " + lbName);
		Social.ReportScore (score, lbName, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});*/
		/*if (Application.platform == RuntimePlatform.Android)
			GooglePlayManager.instance.submitScore ("CgkInZaV_KAKEAIQAA", score);
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			GameCenterManager.reportScore (score, "lb3");*/
			
		UM_GameServiceManager.instance.SubmitScore ("1.4lb", score);
	}
	
	
	// Shows the gamecenter UI
	//
	public void ShowGameCenter ()
	{
		//if (Application.platform == RuntimePlatform.IPhonePlayer)
		//	Social.ShowLeaderboardUI ();
		//else if (Application.platform == RuntimePlatform.Android)
			//((PlayGamesPlatform) Social.Active).ShowLeaderboardUI ("CgkInZaV_KAKEAIQAA");
			
		if (Application.platform == RuntimePlatform.Android)
			GooglePlayManager.instance.showLeaderBoardsUI ();
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			GameCenterManager.showLeaderBoard ("lb3");
			
		//UM_GameServiceManager.instance.ShowLeaderBoardUI ("1.4lb");
	}
	
	
	// Gives an achievement to the player
	//
	public void GiveAchievement (int id)
	{
		string aName = "";
		switch (id)
		{
			// 24 hour sloth
			case 1:
			
			/*
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth1";
				else if (Application.platform == RuntimePlatform.Android)
					aName = "CgkInZaV_KAKEAIQAg";*/
			/*if (Application.platform == RuntimePlatform.Android)
				GooglePlayManager.instance.incrementAchievementById ("CgkInZaV_KAKEAIQAg", 100);
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				GameCenterManager.submitAchievement (100f, "sloth1");*/
			//#endif
			aName = "24hoursloth";
					
			break;
			// Trampoline sloth
			case 2:
			
			/*
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth2";
				else if (Application.platform == RuntimePlatform.Android)
					aName = "CgkInZaV_KAKEAIQAw";*/
		/*	if (Application.platform == RuntimePlatform.Android)
				GooglePlayManager.instance.incrementAchievementById ("CgkInZaV_KAKEAIQAw", 100);
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				GameCenterManager.submitAchievement (100f, "sloth2");*/
			//#endif
			aName = "trampolinesloth";
			
			break;
			// Star sloth
			case 3:
				
				/*
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth3";
				else if (Application.platform == RuntimePlatform.Android)
					aName = "CgkInZaV_KAKEAIQBA";*/
			/*if (Application.platform == RuntimePlatform.Android)
				GooglePlayManager.instance.incrementAchievementById ("CgkInZaV_KAKEAIQBA", 100);
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				GameCenterManager.submitAchievement (100f, "sloth3");*/
			//#endif
			aName = "starsloth";
			
			break;
			// Persistent sloth
			case 4:
				
				/*
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth4";
				else if (Application.platform == RuntimePlatform.Android)
					aName = "CgkInZaV_KAKEAIQBQ";*/
		/*	if (Application.platform == RuntimePlatform.Android)
				GooglePlayManager.instance.incrementAchievementById ("CgkInZaV_KAKEAIQBQ", 100);
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				GameCenterManager.submitAchievement (100f, "sloth4");*/
			//#endif
			aName = "persistentsloth";
			
			break;
			// Bicentennial sloth
			case 5:
				
				/*
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth5";
				else if (Application.platform == RuntimePlatform.Android)
					aName = "CgkInZaV_KAKEAIQBg";*/
			/*if (Application.platform == RuntimePlatform.Android)
				GooglePlayManager.instance.incrementAchievementById ("CgkInZaV_KAKEAIQBg", 100);
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				GameCenterManager.submitAchievement (100f, "sloth5");*/
			//#endif
			aName = "bisloth";
			
			break;
		}
		//ReportAchievementProgress (aName, 100.0);
		UM_GameServiceManager.instance.IncrementAchievement (aName, 100.0f);
		UM_GameServiceManager.instance.ReportAchievement (aName);
		dataCont.SetCheevoGot (id);
	}
	
	
	//
	public string [] GetLeaderboardScores ()
	{
		//
		_lbStrings = new string [10];
		for (int i = 0; i < _lbStrings.Length; i++) { _lbStrings [i] = "-"; }
		
		//
		/*
		if (Application.platform == RuntimePlatform.Android)
		{
			GPLeaderBoard lb = GooglePlayManager.instance.GetLeaderBoard ("CgkInZaV_KAKEAIQAA");
			List <GPScore> sc = lb.GetScoresList (GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL);
			for (int i = 0; i < _lbStrings.Length; i++)
			{
				_lbStrings [i] = sc [i].score.ToString () + "\t (" + sc [i].playerId + ")";
			}
		}*/
		/*
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			GCLeaderBoard lb = GameCenterManager.GetLeaderBoard ("lb3");
			for (int i = 0; i < _lbStrings.Length; i++)
			{
				GCScore gc = lb.GetScore ((i + 1), GCBoardTimeSpan.ALL_TIME, GCCollectionType.GLOBAL);
				_lbStrings [i] = gc.score.ToString () + "\t (" + gc.playerId + ")";
			}
		}*/
		
		
		/*
		Social.LoadScores (lbName, scores => 
		{
			if (scores.Length > 0) 
			{
				for (int i = 0; i < scores.Length; i++)
				{
					_lbStrings [i] = scores [i].value + "\t (" + scores [i].userID + ")";
					if (i >= 9)
						break;
				}
			}
		});
		*/
		return _lbStrings;
	}
	
	
	#endregion
	
	
	#region Utility
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		dataCont = GetComponent <DataController> ();
	}
	
	#endregion
}
