using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
//using GooglePlayGames;


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
		else*/ if (Application.platform == RuntimePlatform.IPhonePlayer)
			lbName = "lb3";
		
		// Authenticate the local user
		Social.localUser.Authenticate (ProcessAuthentication);
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
	public void ReportScore (long score) 
	{
		Debug.Log ("Reporting score " + score + " on leaderboard " + lbName);
		Social.ReportScore (score, lbName, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}
	
	
	// Shows the gamecenter UI
	//
	public void ShowGameCenter ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			Social.ShowLeaderboardUI ();
		//else if (Application.platform == RuntimePlatform.Android)
			//((PlayGamesPlatform) Social.Active).ShowLeaderboardUI ("CgkInZaV_KAKEAIQAA");
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
			
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth1";
				//else if (Application.platform == RuntimePlatform.Android)
					//aName = "CgkInZaV_KAKEAIQAg";
					
			break;
			// Trampoline sloth
			case 2:
			
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth2";
				//else if (Application.platform == RuntimePlatform.Android)
					//aName = "CgkInZaV_KAKEAIQAw";
				
			break;
			// Star sloth
			case 3:
				
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth3";
				//else if (Application.platform == RuntimePlatform.Android)
					//aName = "CgkInZaV_KAKEAIQBA";
				
			break;
			// Persistent sloth
			case 4:
				
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth4";
				//else if (Application.platform == RuntimePlatform.Android)
					//aName = "CgkInZaV_KAKEAIQBQ";
				
			break;
			// Bicentennial sloth
			case 5:
				
				if (Application.platform == RuntimePlatform.IPhonePlayer)
					aName = "sloth5";
				//else if (Application.platform == RuntimePlatform.Android)
					//aName = "CgkInZaV_KAKEAIQBg";
				
			break;
		}
		ReportAchievementProgress (aName, 100.0);
		dataCont.SetCheevoGot (id);
	}
	
	
	// Reports progress towards a particular achievement
	//
	void ReportAchievementProgress (string name, double percentCompleted)
	{
		IAchievement achievement = Social.CreateAchievement ();
		achievement.id = name;
		achievement.percentCompleted = percentCompleted;
		achievement.ReportProgress ( result => {
			if (result)
				Debug.Log ("Successfully reported progress");
			else
				Debug.Log ("Failed to report progress");
		});
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
