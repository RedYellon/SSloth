////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayServiceExample : AndroidNativeExampleBase {
	
	private int score = 100;


	//example
	private const string LEADERBOARD_NAME = "leaderboard_best_scores";
	//private const string LEADERBOARD_NAME = "REPLACE_WITH_YOUR_NAME";


	private  const string PIE_GIFT_ID = "Pie";
	//private  const string PIE_GIFT_ID = "REPLACE_WITH_YOUR_ID";


	//example
	private const string LEADERBOARD_ID = "CgkIipfs2qcGEAIQAA";
	//private const string LEADERBOARD_ID = "REPLACE_WITH_YOUR_ID";



	private const string INCREMENTAL_ACHIEVEMENT_ID = "CgkIipfs2qcGEAIQCg";
	//private const string INCREMENTAL_ACHIEVEMENT_ID = "REPLACE_WITH_YOUR_ID";



	public GameObject avatar;
	private Texture defaulttexture;
	public Texture2D pieIcon;

	public DefaultPreviewButton connectButton;
	public DefaultPreviewButton scoreSubmit;
	public SA_Label playerLabel;

	public DefaultPreviewButton[] ConnectionDependedntButtons;




	public SA_Label a_id;
	public SA_Label a_name;
	public SA_Label a_descr;
	public SA_Label a_type;
	public SA_Label a_state;
	public SA_Label a_steps;
	public SA_Label a_total;


	public SA_Label b_id;
	public SA_Label b_name;
	public SA_Label b_all_time;




	void Start() {

		playerLabel.text = "Player Diconnected";
		defaulttexture = avatar.renderer.material.mainTexture;

		//listen for GooglePlayConnection events
		GooglePlayConnection.instance.addEventListener (GooglePlayConnection.PLAYER_CONNECTED, OnPlayerConnected);
		GooglePlayConnection.instance.addEventListener (GooglePlayConnection.PLAYER_DISCONNECTED, OnPlayerDisconnected);
		GooglePlayConnection.instance.addEventListener(GooglePlayConnection.CONNECTION_RESULT_RECEIVED, OnConnectionResult);



		//listen for GooglePlayManager events
		GooglePlayManager.instance.addEventListener (GooglePlayManager.ACHIEVEMENT_UPDATED, OnAchivmentUpdated);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.SCORE_SUBMITED, OnScoreSubmited);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.SCORE_REQUEST_RECEIVED, OnScoreUpdated);



		GooglePlayManager.instance.addEventListener (GooglePlayManager.SEND_GIFT_RESULT_RECEIVED, OnGiftResult);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.PENDING_GAME_REQUESTS_DETECTED, OnPendingGiftsDetected);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.GAME_REQUESTS_ACCEPTED, OnGameRequestAccepted);

		GooglePlayManager.instance.addEventListener (GooglePlayManager.AVALIABLE_DEVICE_ACCOUNT_LOADED, OnAccsLoaded);
		GooglePlayManager.instance.addEventListener (GooglePlayManager.OAUTH_TOCKEN_LOADED, OnToeknLoaded);


		GooglePlayManager.instance.addEventListener (GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchievmnetsLoadedInfoListner);


		if(GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED) {
			//checking if player already connected
			OnPlayerConnected ();
		} 

	}

	private void OnDestroy() {
		if(!GooglePlayConnection.IsDestroyed) {
			GooglePlayConnection.instance.removeEventListener (GooglePlayConnection.PLAYER_CONNECTED, OnPlayerConnected);
			GooglePlayConnection.instance.removeEventListener (GooglePlayConnection.PLAYER_DISCONNECTED, OnPlayerDisconnected);
			GooglePlayConnection.instance.removeEventListener(GooglePlayConnection.CONNECTION_RESULT_RECEIVED, OnConnectionResult);
		}

		if(!GooglePlayManager.IsDestroyed) {
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.ACHIEVEMENT_UPDATED, OnAchivmentUpdated);
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.SCORE_SUBMITED, OnScoreSubmited);
			
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.SEND_GIFT_RESULT_RECEIVED, OnGiftResult);
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.PENDING_GAME_REQUESTS_DETECTED, OnPendingGiftsDetected);
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.GAME_REQUESTS_ACCEPTED, OnGameRequestAccepted);
			
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.AVALIABLE_DEVICE_ACCOUNT_LOADED, OnAccsLoaded);
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.OAUTH_TOCKEN_LOADED, OnToeknLoaded);
			
			
			GooglePlayManager.instance.removeEventListener (GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchievmnetsLoadedInfoListner);
		}
	}

	private void ConncetButtonPress() {
		Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.state.ToString());
		if(GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED) {
			SA_StatusBar.text = "Disconnecting from Play Service...";
			GooglePlayConnection.instance.disconnect ();
		} else {
			SA_StatusBar.text = "Connecting to Play Service...";
			GooglePlayConnection.instance.connect ();
		}
	}

	private void GetAccs() {
		GooglePlayManager.instance.RetriveDeviceGoogleAccounts();
	}

	private void RetriveToken() {
		GooglePlayManager.instance.LoadTocken();
	}


	private void showLeaderBoardsUI() {
		GooglePlayManager.instance.showLeaderBoardsUI ();
		SA_StatusBar.text = "Showing Leader Boards UI";
	}

	private void loadLeaderBoards() {
		GooglePlayManager.instance.addEventListener (GooglePlayManager.LEADERBOARDS_LOEADED, OnLeaderBoardsLoaded);
		GooglePlayManager.instance.loadLeaderBoards ();
		SA_StatusBar.text = "Loading Leader Boards Data...";
	}

	private void showLeaderBoard() {
		GooglePlayManager.instance.showLeaderBoardById (LEADERBOARD_ID);
		SA_StatusBar.text = "Shwoing Leader Board UI for " + LEADERBOARD_ID;
	}

	private void submitScore() {
		score++;
		GooglePlayManager.instance.submitScore (LEADERBOARD_NAME, score);
		SA_StatusBar.text = "Score " + score.ToString() + " Submited for " + LEADERBOARD_NAME;
	}


	private void ResetBoard() {
		GooglePlayManager.instance.resetLeaderBoard(LEADERBOARD_ID);
		UpdateBoardInfo();
	}




	private void showAchivmentsUI() {
		GooglePlayManager.instance.showAchievementsUI ();
		SA_StatusBar.text = "Showing Achivments UI";

	}

	private void loadAchievements() {
		GooglePlayManager.instance.addEventListener (GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchivmentsLoaded);
		GooglePlayManager.instance.loadAchievements ();

		SA_StatusBar.text = "Loading Achivments Data...";
	}

	private void reportAchievement() {
		GooglePlayManager.instance.reportAchievement ("achievement_simple_achievement_example");
		SA_StatusBar.text = "Reporting achievement_prime...";
	}

	private void incrementAchievement() {
		GooglePlayManager.instance.incrementAchievementById (INCREMENTAL_ACHIEVEMENT_ID, 1);
		SA_StatusBar.text = "Incrementing achievement_bored...";
	}


	private void revealAchievement() {
		GooglePlayManager.instance.revealAchievement ("achievement_hidden_achievement_example");
		SA_StatusBar.text = "Revealing achievement_humble...";
	}

	private void ResetAchievement() {
		GooglePlayManager.instance.resetAchievement(INCREMENTAL_ACHIEVEMENT_ID);

		AndroidNative.showMessage ("Reset Complete: ", "Reset Complete, but since this is feature for testing only, achivment data cache will be updated after next interaction with acheivment");
	}

	private void ResetAllAchievements() {
		GooglePlayManager.instance.ResetAllAchievements();
		AndroidNative.showMessage ("Reset Complete: ", "Reset Complete, but since this is feature for testing only, achivment data cache will be updated after next interaction with acheivment");

	}



	private void SendGiftRequest() {
		GooglePlayManager.instance.SendGiftRequest(GPGameRequestType.TYPE_GIFT, 1, pieIcon, "Here is some pie", PIE_GIFT_ID);
	}


	private void OpenInbox() {
		GooglePlayManager.instance.ShowRequestsAccepDialog();
	}


	void FixedUpdate() {
		if(GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED) {
			if(GooglePlayManager.instance.player.icon != null) {
				avatar.renderer.material.mainTexture = GooglePlayManager.instance.player.icon;
			}
		}  else {
			avatar.renderer.material.mainTexture = defaulttexture;
		}


		string title = "Connect";
		if(GooglePlayConnection.state == GPConnectionState.STATE_CONNECTED) {
			title = "Disconnect";

			foreach(DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.EnabledButton();
			}


		} else {
			foreach(DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.DisabledButton();

			}
			if(GooglePlayConnection.state == GPConnectionState.STATE_DISCONNECTED || GooglePlayConnection.state == GPConnectionState.STATE_UNCONFIGURED) {

				title = "Connect";
			} else {
				title = "Connecting..";
			}
		}

		connectButton.text = title;


		scoreSubmit.text = "Submit Score: " + score;
	}



	
	//--------------------------------------
	// EVENTS
	//--------------------------------------


	private void OnAchievmnetsLoadedInfoListner() {
		GPAchievement achievement = GooglePlayManager.instance.GetAchievement(INCREMENTAL_ACHIEVEMENT_ID);


		if(achievement != null) {
			a_id.text 		= "Id: " + achievement.id;
			a_name.text 	= "Name: " +achievement.name;
			a_descr.text 	= "Description: " + achievement.description;
			a_type.text 	= "Type: " + achievement.type.ToString();
			a_state.text 	= "State: " + achievement.state.ToString();
			a_steps.text 	= "CurrentSteps: " + achievement.currentSteps.ToString();
			a_total.text 	= "TotalSteps: " + achievement.totalSteps.ToString();
		}
	}

	private void OnAchivmentsLoaded(CEvent e) {
		GooglePlayManager.instance.removeEventListener (GooglePlayManager.ACHIEVEMENTS_LOADED, OnAchivmentsLoaded);
		GooglePlayResult result = e.data as GooglePlayResult;
		if(result.isSuccess) {

			foreach(string achievementId in GooglePlayManager.instance.achievements.Keys) {
				GPAchievement achievement = GooglePlayManager.instance.GetAchievement(achievementId);
				Debug.Log(achievement.id);
				Debug.Log(achievement.name);
				Debug.Log(achievement.description);
				Debug.Log(achievement.type);
				Debug.Log(achievement.state);
				Debug.Log(achievement.currentSteps);
				Debug.Log(achievement.totalSteps);
			}

			SA_StatusBar.text = "Total Achivments: " + GooglePlayManager.instance.achievements.Count.ToString();
			AndroidNative.showMessage ("Achievments Loaded", "Total Achivments: " + GooglePlayManager.instance.achievements.Count.ToString());
		} else {
			SA_StatusBar.text = result.message;
			AndroidNative.showMessage ("Achievments Loaded error: ", result.message);
		}

	}

	private void OnAchivmentUpdated(CEvent e) {
		GP_GamesResult result = e.data as GP_GamesResult;
		SA_StatusBar.text = "Achievment Updated: Id: " + result.achievementId + "\n status: " + result.message;
		AndroidNative.showMessage ("Achievment Updated ", "Id: " + result.achievementId + "\n status: " + result.message);
	}

	

	private void OnLeaderBoardsLoaded(CEvent e) {
		GooglePlayManager.instance.removeEventListener (GooglePlayManager.LEADERBOARDS_LOEADED, OnLeaderBoardsLoaded);

		GooglePlayResult result = e.data as GooglePlayResult;
		if(result.isSuccess) {
			if( GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID) == null) {
				AndroidNative.showMessage("Leader boards loaded", LEADERBOARD_ID + " not found in leader boards list");
				return;
			}


			SA_StatusBar.text = LEADERBOARD_NAME + "  score  " + GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID).GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).score.ToString();
			AndroidNative.showMessage (LEADERBOARD_NAME + "  score",  GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID).GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).score.ToString());
		} else {
			SA_StatusBar.text = result.message;
			AndroidNative.showMessage ("Leader-Boards Loaded error: ", result.message);
		}

		UpdateBoardInfo();

	}

	private void UpdateBoardInfo() {
		GPLeaderBoard leaderboard = GooglePlayManager.instance.GetLeaderBoard(LEADERBOARD_ID);
		if(leaderboard != null) {
			b_id.text 		= "Id: " + leaderboard.id;
			b_name.text 	= "Name: " +leaderboard.name;
			b_all_time.text = "All Time Score: " + leaderboard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL).score;
			
		} else {
			b_all_time.text = "All Time Score: " + " -1";
		}
	}

	private void OnScoreSubmited(CEvent e) {
		GooglePlayResult result = e.data as GooglePlayResult;

		SA_StatusBar.text = "Score Submited:  " + result.message;



	}

	private void OnScoreUpdated() {
		UpdateBoardInfo();
	}



	private void OnPlayerDisconnected() {
		SA_StatusBar.text = "Player Diconnected";
		playerLabel.text = "Player Diconnected";
	}

	private void OnPlayerConnected() {
		SA_StatusBar.text = "Player Connected";
		playerLabel.text = GooglePlayManager.instance.player.name + "(" + GooglePlayManager.instance.currentAccount + ")";
	}

	private void OnConnectionResult(CEvent e) {

		GooglePlayConnectionResult result = e.data as GooglePlayConnectionResult;
		SA_StatusBar.text = "ConnectionResul:  " + result.code.ToString();
		Debug.Log(result.code.ToString());
	}

	private void OnGiftResult(CEvent e) {
		GooglePlayGiftRequestResult result = e.data as GooglePlayGiftRequestResult;
		SA_StatusBar.text = "Gift Send Result:  " + result.code.ToString();
	}

	private void OnPendingGiftsDetected(CEvent e) {
		AndroidDialog dialog = AndroidDialog.Create("Pending Gifts Detected", "You got few gifts from your friends, do you whant to take a look?");
		dialog.addEventListener(BaseEvent.COMPLETE, OnPromtGiftDialogClose);
	}

	private void OnPromtGiftDialogClose(CEvent e) {
		//removing listner
		(e.dispatcher as AndroidDialog).removeEventListener(BaseEvent.COMPLETE, OnPromtGiftDialogClose);
		
		//parsing result
		switch((AndroidDialogResult)e.data) {
		case AndroidDialogResult.YES:
			GooglePlayManager.instance.ShowRequestsAccepDialog();
			break;
		
			
		}
	}



	private void OnGameRequestAccepted(CEvent e) {
		List<GPGameRequest> gifts = e.data as List<GPGameRequest>;
		foreach(GPGameRequest g in gifts) {
			AndroidNative.showMessage("Gfit Accepted", g.playload + " is excepted");
		}
	}


	private void OnAccsLoaded() {
		string msg = "Device contains following google accounts:" + "\n";
		foreach(string acc in GooglePlayManager.instance.deviceGoogleAccountList) {
			msg += acc + "\n";
		} 

		AndroidNative.showMessage("Accounts Loaded", msg);
	}

	private void OnToeknLoaded() {

		AndroidNative.showMessage("Toekn Loaded", GooglePlayManager.instance.loadedAuthTocken);
	}





}
