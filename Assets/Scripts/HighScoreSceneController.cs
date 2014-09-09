/*
 	HighScoreSceneController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 26, 2014
 	Last Edited:	June 9, 2014
 	
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
				ButtonTouched (inputCont.GetTouchRaycastObject ().name);
			}
		}
	}
	
	
	//
	//
	private void ButtonTouched (string buttonName)
	{
		switch (buttonName)
		{
			case "BackToMainMenuButtonHS":
				transitionCont.ChangeToScene (0);
				audioCont.PlayButtonPressSound ();
				StartCoroutine ("Rise");
			break;
			case "LeaderboardsButton":
				audioCont.PlaySound ("Button");
				lb.ShowGameCenter ();
			break;
		}
	}
	
	
	//
	//
	public void BackButtonPressed ()
	{
		transitionCont.ChangeToScene (0);
		audioCont.PlayButtonPressSound ();
		StartCoroutine ("Rise");
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
		int [] highScores = dataCont.GetHighScores ();
		for (int i = 0; i < 10; i++)
		{
			if (i >= 9)
				highScoreSlots [i].text = (i + 1).ToString () + ")\t" + highScores [i].ToString ();
			else
				highScoreSlots [i].text = (i + 1).ToString () + ")\t\t" + highScores [i].ToString ();
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
		lb = gameObject.GetComponent <Leaderboard> ();
		resetMaskRend = GameObject.Find ("ScreenMask").renderer;
		transitionCont = GetComponent <MenuBackgroundTransitionController> ();
		buttonTabEnabledColor = statsTabButton.GetComponent <Image> ().color;
		buttonTabDisabledColor = scoresTabButton.GetComponent <Image> ().color;
	}
	
	#endregion
}
