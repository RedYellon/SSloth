/*
 	HighScoreSceneController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 26, 2014
 	Last Edited:	September 13, 2014
 	
 	Controls the high score scene.
*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HighScoreSceneController : MonoBehaviour 
{
	#region Variables
	
		#region Public
	
		// The back to main menu button
		public GameObject backToMenuButton;
		// The collection of static rendererd for this scene
		public Renderer [] staticRends;
		// The list of score slots
		public Text [] highScoreSlots;
		public Text [] highScoreTimeSlots;
		public Text [] lbScoreSlots;
		// The view leaderboards button
		public GameObject leaderboardsButton;
		// The tab buttons
		public GameObject statsTabButton;
		public GameObject scoresTabButton;
		public GameObject leaderboardsTabButton;
		// The objects that are enabled/disabled for each tab
		public GameObject [] statsObjects;
		public GameObject [] scoresObjects;
		public GameObject [] leaderboardsObjects;
		//
		public ParticleSystem dustHitParticles;
		//
		public GameObject sceneObj;
	
	
			#region Stats
			
			public Text timesPlayedNum;
			public Text avgScoreNum;
			public Text platsNum;
			public Text totalScoreNum;
			public Text totalTimeNum;
			public Text jumpNum;
			public Text doubleJumpNum;
			public Text slothsLaunchedNum;
			public Text mudSplatteredNum;
			public Text sadsHitNum;
			public Text starsTotalNum;
			public Text starsGoldNum;
			public Text starsSilverNum;
			public Text starsBronzeNum;
			public Text nightsSurvivedNum;
			public Text recordsBrokenNum;
			public Text balloonsSeenNum;
			public Text butterfliesSeenNum;
			public Text firefliesSeenNum;
			public Text mammalsSeenNum;
			public Text factsLearnedNum;
			public Text peakAltitudeNum;
			public Text maxHangtimeNum;
			
			#endregion
		
		#endregion
	
		#region Scripts
	
		// The scene controller
		SceneController sceneCont;
		// The data controller
		DataController dataCont;
		// The input controller
		InputController inputCont;
		// The audio controller
		AudioController audioCont;
		// The leaderboard script
		Leaderboard lb;
		// The bounce script for this gameobject
		BounceObject bounce;
		// The menu transition controller
		MenuBackgroundTransitionController transitionCont;
		// The main menu controller
		MainMenuController mainMenuCont;
	
		#endregion
		
		#region Canvasses
		
		// The scores n stats canvas
		GameObject _scoresStatsCanvas;
		
		#endregion
	
		#region Private
	
		//
		private Transform transParent;
		//
		private float transitionDropSpeed = 0;
		// If we are currently transitioning between menus
		private bool isTransitioning = false;
		// The reset mask object renderer
		private Renderer resetMaskRend;
		//
		private bool isFadingInResetMask = false;
		private bool isFadingOutResetMask = false;
		// The currently selected tab
		private string currentlySelectedTab = "stats";
		// The heights of the buttons
		float buttonTabEnabledHeight = 37.42f;
		float buttonTabDisabledHeight = 30f;
		// The colors of the tab buttons
		Color buttonTabEnabledColor;
		Color buttonTabDisabledColor;
		// The erase data button (trashcan)
		private	GameObject _eraseDataButton;
		// The number of times the user has confirmed they want to erase their data
		private int _dataEraseConfirmationCount = 0;
		// The screen mask used during the erase data process
		private tk2dSprite _eraseDataScreenMask;
		// The warning text displayed when erasing data
		private Text _eraseDataWarningText;
		// The YES and NO confirmation buttons
		private GameObject _yesEraseDataButton;
		private GameObject _noEraseDataButton;
	
	#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		if (!isTransitioning) CheckForInput ();
		if (isFadingInResetMask) FadeInResetMask ();
		if (isFadingOutResetMask) FadeOutResetMask ();
	}
	
	
	//
	//
	private void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// React
				//ButtonTouched (inputCont.GetTouchRaycastObject ().name);
			}
		}
	}
	
	
	//
	//
	public void BackButtonPressed ()
	{	
		// Transition to main menu
		transitionCont.ChangeToScene (0);
		audioCont.PlayButtonPressSound ();
		StartCoroutine ("Rise");
	}
	
	
	//
	//
	public void LBButtonPressed ()
	{	
		audioCont.PlaySound ("Button");
		lb.ShowGameCenter ();
	}
	
	
	//
	//
	void FadeInResetMask ()
	{
		resetMaskRend.material.color = Color.Lerp (resetMaskRend.material.color, new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.8f), Time.deltaTime * 15.0f);
	}
	
	
	//
	//
	void FadeOutResetMask ()
	{
		resetMaskRend.material.color = Color.Lerp (resetMaskRend.material.color, new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f), Time.deltaTime * 15.0f);
	}
	
	#endregion
	
	
	#region Erase Data
	
	// Called when the erase data button is pressed
	// Called directly from Unity UI
	public void EraseDataButtonPressed ()
	{
		// Depending on how many times the user has confirmed erase data action...
		switch (_dataEraseConfirmationCount)
		{
			case 0:
				// Activate confirmation objects
				_eraseDataScreenMask.color = new Color (_eraseDataScreenMask.color.r, _eraseDataScreenMask.color.g, _eraseDataScreenMask.color.b, 0.85f);
				_scoresStatsCanvas.SetActive (false);	
				_eraseDataButton.SetActive (false);
				_yesEraseDataButton.SetActive (true);
				_noEraseDataButton.SetActive (true);
				_eraseDataWarningText.text = "are you SURE you want to erase your data?";
				audioCont.PlayButtonPressSound ();
			break;
			case 1:
				_eraseDataWarningText.text = "are you REALLY sure? You can't undo this action";
			break;
			case 2:
				_eraseDataWarningText.text = "okay for real it's gonna happen, but are you SURE?";
			break;
			case 3:
				// Erase the data
				_eraseDataWarningText.text = "erasing data...";
				_yesEraseDataButton.SetActive (false);
				_noEraseDataButton.SetActive (false);
				mainMenuCont.SetPlayAsActiveButton ();
				EraseData ();
				
				// Clean up the erase data stuff
				Invoke ("CleanUpEraseProcess", 2.0f);
			break;
		}
		
		_dataEraseConfirmationCount ++;
	}
	
	
	// Cancels the erase data process
	// Called directly from Unity UI
	public void CancelEraseDataProcess ()
	{
		_scoresStatsCanvas.SetActive (true);
		_eraseDataButton.SetActive (true);	
		_eraseDataScreenMask.color = Color.clear;
		_eraseDataWarningText.text = "";
		_yesEraseDataButton.SetActive (false);
		_noEraseDataButton.SetActive (false);
		_dataEraseConfirmationCount = 0;
	}
	
	
	// Actually erases the game data
	// Called from EraseDataButtonPressed ()
	void EraseData ()
	{
		dataCont.EraseData ();
	}
	
	
	// Cleans up the erase data gameobjects
	// Called from EraseDataButtonPressed ()
	void CleanUpEraseProcess ()
	{
		_eraseDataScreenMask.color = Color.clear;
		_eraseDataWarningText.text = "";
		_dataEraseConfirmationCount = 0;
		_scoresStatsCanvas.SetActive (true);
		_eraseDataButton.SetActive (true);
		BackButtonPressed ();
	}
	
	#endregion
	
	
	#region Scene Switching
	
	//
	//
	public void SetTab (string currentTab)
	{
		// Set the tab
		if (currentlySelectedTab == currentTab)
			return;
		currentlySelectedTab = currentTab;
		
		// Turn off all buttons
		statsTabButton.GetComponent <Image> ().color = buttonTabDisabledColor;
		statsTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (statsTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabDisabledHeight);
		scoresTabButton.GetComponent <Image> ().color = buttonTabDisabledColor;
		scoresTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (scoresTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabDisabledHeight);
		leaderboardsTabButton.GetComponent <Image> ().color = buttonTabDisabledColor;
		leaderboardsTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (leaderboardsTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabDisabledHeight);
		
		// Turn off all renderers
		foreach (GameObject o in statsObjects) { o.SetActive (false); }
		foreach (GameObject o in scoresObjects) { o.SetActive (false); }
		foreach (GameObject o in leaderboardsObjects) { o.SetActive (false); }
		
		// Depending on the tab button we press
		switch (currentlySelectedTab)
		{
			case "stats":
				statsTabButton.GetComponent <Image> ().color = buttonTabEnabledColor;
				statsTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (statsTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabEnabledHeight);
				foreach (GameObject o in statsObjects) { o.SetActive (true); }
			break;
			case "scores":
				scoresTabButton.GetComponent <Image> ().color = buttonTabEnabledColor;
				scoresTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (scoresTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabEnabledHeight);
				foreach (GameObject o in scoresObjects) { o.SetActive (true); }
			break;
			case "leaderboard":
				leaderboardsTabButton.GetComponent <Image> ().color = buttonTabEnabledColor;
				leaderboardsTabButton.GetComponent <RectTransform> ().sizeDelta = new Vector2 (leaderboardsTabButton.GetComponent <RectTransform> ().sizeDelta.x, buttonTabEnabledHeight);
				foreach (GameObject o in leaderboardsObjects) { o.SetActive (true); }
			break;
		}	
	}
	
	
	//
	//
	public void Setup ()
	{
		//
		GetHighScores ();
		GetOtherData ();
		isTransitioning = true;	
		
		// Begin the transition
		//audioCont.PlaySound ("Whoosh");
		StartCoroutine ("Drop");
	}
	
	
	//
	// Called from Setup ()
	IEnumerator Drop ()
	{
		// While the transform parent is still above the target, drop it
		while (transParent.position.y > 0.5f)
		{
			transitionDropSpeed += Time.deltaTime * 2f;
			transParent.Translate (Vector3.down * transitionDropSpeed);
			
			yield return null;
		}
		
		// Now that the transform parent is out of the way, we can cleanup the scene
		transitionDropSpeed = 0;
		bounce.Bounce ();
		dustHitParticles.Play ();
		audioCont.PlaySound ("OptionsLand");
		isTransitioning = false;
	}
	
	
	//
	//
	IEnumerator Rise ()
	{
		// While the transform parent is still above the target, drop it
		audioCont.PlaySound ("Button");
		isTransitioning = true;
		while (transParent.position.y < 11.63509f)
		{
			transitionDropSpeed += Time.deltaTime * 1.5f;
			transParent.Translate (Vector3.up * transitionDropSpeed);
			
			yield return null;
		}
		
		// Now that the transform parent is out of the way, we can cleanup the scene
		transitionDropSpeed = 0;
		transParent.position = new Vector3 (0, 12.63509f, 0);
		
		// Cleanup the scene objects
		Cleanup ();
	}
	
	
	//
	//
	private void Cleanup ()
	{
		// Fade out the reset mask
		isFadingInResetMask = false;
		isFadingOutResetMask = false;
		resetMaskRend.material.color = new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f);
		transitionDropSpeed = 0;
		
		sceneCont.ChangeScene (1);
	}
	
	#endregion
	
	
	#region Utility
	
	//
	//
	void FadeInResetMaskStart ()
	{
		isFadingOutResetMask = false;
		isFadingInResetMask = true;
	}
	
	
	//
	//
	private void GetHighScores ()
	{
		// Local scores
		int [] highScores = dataCont.GetHighScores ();
		for (int i = 0; i < 10; i++)
		{
			highScoreSlots [i].text = highScores [i].ToString ();
			if (highScoreSlots [i].text == "0")
				highScoreSlots [i].text = "-";
		}
		
		string [] highScoreTimes = dataCont.GetHighScoresTimes ();
		for (int i = 0; i < 10; i++)
		{
			highScoreTimeSlots [i].text = highScoreTimes [i];
		}
		
		// Leaderboard scores
		string [] lbScores = lb.GetLeaderboardScores ();
		for (int i = 0; i < lbScores.Length; i++)
		{
			lbScoreSlots [i].text = lbScores [i];
		}
	}
	
	
	//
	//
	private void GetOtherData ()
	{
		// Calculate play time
		int pt = dataCont.GetSecondsPlayed ();
		int hoursPlayed = 0;
		if (pt >= 3600)
			hoursPlayed = pt / 3600;
		pt -= (hoursPlayed * 3600);
		int minutesPlayed = 0;
		if (pt >= 60)
			minutesPlayed = pt / 60;
		pt -= (minutesPlayed * 60);
		int secondsPlayed = pt;
		string hps = "00"; if (hoursPlayed > 0) hps = hoursPlayed.ToString (); if (hps.Length == 1) hps = "0" + hps;
		string mps = "00"; if (minutesPlayed > 0) mps = minutesPlayed.ToString (); if (mps.Length == 1) mps = "0" + mps;
		string sps = "00"; if (secondsPlayed > 0) sps = secondsPlayed.ToString (); if (sps.Length == 1) sps = "0" + sps;
		totalTimeNum.text = hps + ":" + mps + ":" + sps;
		
		timesPlayedNum.text = dataCont.GetTimesPlayed ().ToString ();
		platsNum.text = dataCont.GetPlatsHit ().ToString ();
		totalScoreNum.text = dataCont.GetTotalScoreItemUsable ().ToString ();
		jumpNum.text = dataCont.GetJumps ().ToString ();
		doubleJumpNum.text = dataCont.GetDoubleJumps ().ToString ();
		slothsLaunchedNum.text = dataCont.GetAngryPlatsHit ().ToString ();
		mudSplatteredNum.text = dataCont.GetDirtyPlatsHit ().ToString ();
		sadsHitNum.text = dataCont.GetSadPlatsHit ().ToString ();
		starsTotalNum.text = dataCont.GetTotalStars ().ToString ();
		starsGoldNum.text = dataCont.GetGoldStars ().ToString ();
		starsSilverNum.text = dataCont.GetSilverStars ().ToString ();
		starsBronzeNum.text = dataCont.GetBronzeStars ().ToString ();
		nightsSurvivedNum.text = dataCont.GetNightsSurvived ().ToString ();
		recordsBrokenNum.text = dataCont.GetRecordsBroken ().ToString ();
		balloonsSeenNum.text = dataCont.GetBalloonsSeen ().ToString ();
		butterfliesSeenNum.text = dataCont.GetButterfliesSeen ().ToString ();
		firefliesSeenNum.text = dataCont.GetFirefliesSeen ().ToString ();
		mammalsSeenNum.text = dataCont.GetAnimalsSeen ().ToString ();
		factsLearnedNum.text = dataCont.GetFactsLearned ().ToString ();
		peakAltitudeNum.text = dataCont.GetPeakAir ().ToString ("F2") + " m";
		maxHangtimeNum.text = dataCont.GetMaxHangtime ().ToString ("F2") + " sec";
		if (dataCont.GetTimesPlayed () != 0)
			avgScoreNum.text = (dataCont.GetTotalScore () / dataCont.GetTimesPlayed ()).ToString ();
		else
			avgScoreNum.text = "-";
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		transParent = GameObject.Find ("*HighScoreScene").transform;
		bounce = GameObject.Find ("*HighScoreScene").GetComponent <BounceObject> ();
		sceneCont = gameObject.GetComponent <SceneController> ();
		dataCont = gameObject.GetComponent <DataController> ();
		inputCont = gameObject.GetComponent <InputController> ();
		audioCont = gameObject.GetComponent <AudioController> ();
		mainMenuCont = gameObject.GetComponent <MainMenuController> ();
		lb = gameObject.GetComponent <Leaderboard> ();
		resetMaskRend = GameObject.Find ("ScreenMaskEraseData").renderer;
		transitionCont = GetComponent <MenuBackgroundTransitionController> ();
		buttonTabEnabledColor = statsTabButton.GetComponent <Image> ().color;
		buttonTabDisabledColor = scoresTabButton.GetComponent <Image> ().color;
		_scoresStatsCanvas = GameObject.Find ("ScoresStatsCanvas");
		_eraseDataButton = GameObject.Find ("EraseDataButton");
		_eraseDataScreenMask = GameObject.Find ("ScreenMask").GetComponent <tk2dSprite> ();
		_yesEraseDataButton = GameObject.Find ("EraseDataYesButt");
		_eraseDataWarningText = GameObject.Find ("EraseDataConfirmationLabel").GetComponent <Text> ();
		_noEraseDataButton = GameObject.Find ("EraseDataNoButt");
		_yesEraseDataButton.SetActive (false);
		_noEraseDataButton.SetActive (false);

		//
		sceneObj.SetActive (false);
	}
	
	#endregion
}
