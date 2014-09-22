/*
 	EventManager.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		July 28, 2014
 	Last Edited:	August 8, 2014
 	
 	!!!				!!!
 	Handles all events.
 	!!!				!!!
*/


using UnityEngine;
using System.Collections;


public class EventManager : MonoBehaviour 
{
	#region Variables
	
		#region Events
		
		// When the game first boots
		public delegate void BootGame ();
		public static event BootGame OnBootGame;
		
		// When a round is started
		public delegate void RoundBegin ();
		public static event RoundBegin OnRoundBegin;
		
		//
		public delegate void RoundRestart ();
		public static event RoundRestart OnRoundRestart;
		
		// When a round is ended (player dies)
		public delegate void RoundEndScore ();
		public static event RoundEndScore OnRoundEndScore;
		public delegate void RoundEnd ();
		public static event RoundEnd OnRoundEnd;
		
		// 
		public delegate void PlayerJump ();
		public static event PlayerJump OnPlayerJump;
		
		// 
		public delegate void PlayerDoubleJump ();
		public static event PlayerDoubleJump OnPlayerDoubleJump;
		
		// 
		public delegate void PlayerLand ();
		public static event PlayerLand OnPlayerLand;
		
		// 
		public delegate void PlayerLandFirstPlatform ();
		public static event PlayerLandFirstPlatform OnPlayerLandFirstPlatform;
		
		//
		public delegate void BackToMainMenuFromGame ();
		public static event BackToMainMenuFromGame OnBackToMainMenuFromGame;
		
		#endregion
	
	#endregion
	
	
	#region Back to Main Meun From Game
	
	// 
	// 
	public void SetBackToMainMenuFromGame ()
	{
		if (OnBackToMainMenuFromGame != null)
			OnBackToMainMenuFromGame ();
	}
	
	#endregion
	
	
	#region Round Begin
	
	// 
	// 
	public void SetRoundBegin ()
	{
		if (OnRoundBegin != null)
			OnRoundBegin ();
	}
	
	#endregion
	
	
	#region Round Restart
	
	// 
	// 
	public void SetRoundRestart ()
	{
		if (OnRoundRestart != null)
			OnRoundRestart ();
	}
	
	#endregion
	
	
	#region Round End
	
	// 
	// 
	public void SetRoundEnd ()
	{
		if (OnRoundEndScore != null)
			OnRoundEndScore ();
			
		if (OnRoundEnd != null)
			OnRoundEnd ();
	}
	
	#endregion
	
	#region Player Jump
	
	// 
	// 
	public void SetPlayerJump ()
	{
		if (OnPlayerJump != null)
			OnPlayerJump ();
	}
	
	#endregion
	
	#region Player Double Jump
	
	// 
	// 
	public void SetPlayerDoubleJump ()
	{
		if (OnPlayerDoubleJump != null)
			OnPlayerDoubleJump ();
	}
	
	#endregion
	
	#region Player Land
	
	// 
	// 
	public void SetPlayerLand ()
	{
		if (OnPlayerLand != null)
			OnPlayerLand ();
	}
	
	#endregion
	
	
	#region Player Land First platform
	
	// 
	// 
	public void SetPlayerLandFirstPlatform ()
	{
		if (OnPlayerLandFirstPlatform != null)
			OnPlayerLandFirstPlatform ();
	}
	
	#endregion
	
	
	#region Boot Game
	
	// Used for initialization
	// Called automatically at beginning
	void Awake ()
	{
		// Trip game boot event
		if (OnBootGame != null)
			OnBootGame ();
	}
	
	#endregion
}
