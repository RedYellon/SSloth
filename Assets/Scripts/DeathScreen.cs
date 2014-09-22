/*
 	DeathScreen.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	September 14, 2014
 	
 	Coordinates the end-of-game screen and info.
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;	// Used for lists


public class DeathScreen : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The text that holds your score for this round
		public Text yourScoreNumber;
		// The text that holds your total cumulative score
		public TextMesh totalScoreNumber;
		// The text that shows the score added from the stars
		public Text addedScoreNumber;
		// The social tab title text
		public Text socialTabTitle;
		// The high score particle systems
		public ParticleSystem [] confettiHighScoreSystems;
		//
		public Text dykText1;
		public Text dykText2;
		// The gradient levels for the pre-finished score number
		public Color topGradientPreFinishColor;
		public Color bottomGradientPreFinishColor;
		// The gradient levels for the finished score number
		public Color topGradientPostFinishColor;
		public Color bottomGradientPostFinishColor;
		// How long the player must wait beofre they can interact with the screen
		public float inputFreezeLength = 0.4f;
		// The text that shows the player's highest score
		public Text highScoreNumber;
		//
		public float transitionSlideSpeed = 10.0f;
		//
		public float fadeInResetMaskDelayTime = 3.0f;
		// 
		public float fadeInResetMaskRate = 10.0f;
		public float resetMaskFadeInAlpha = 0.85f;
		//
		public float fadeOutResetMaskRate = 20.0f;
		//
		public Renderer [] rends;
		//
		public GameObject mainMenuButton;
		//
		public GameObject playAgainButton;
		//
		public Text goldStarNumText;
		public Text silverStarNumText;
		public Text bronzeStarNumText;
		//
		public Transform highlighter;
		//
		public Transform leftMoveInParent;
		public Transform rightMoveInParent;
		public Transform topMoveInParent;
		public Transform bottomMoveInParent;
		//
		public float leftMoveXTarget;
		public float rightMoveXTarget;
		public float topMoveYTarget;
		public float bottomMoveYTargetClosed;
		public float bottomMoveYTargetOpen;
		//
		
		//
		public float highlighterPlayAgainYPos;
		public float highlighterMainMenuYPos;
		//
		public Color buttonInactiveColor;
	
		#endregion
	
		#region Scripts
		
		// The input controller
		InputController inputCont;
		// The score controller
		ScoreController scoreCont;
		// The data controller
		DataController dataCont;
		// The game controller
		GameController gameCont;
		// The audio controller
		AudioController audioCont;
		// The screen flasher
		Flash screenFlash;
		// The player controller
		PlayerController playerCont;
		// The social sharing
		SocialShare shareCont;
		//
		EventManager eventManager;
		// The high score label controller
		NewHighScoreLabel newHighLabel;
		// The camera controller
		CameraController camCont;
		// The UI gradient controller of the score number
		Gradient scoreGradient;
	
		#endregion
	
		#region Private
		
		// The reset mask object renderer
		private Renderer resetMaskRend;
		//
		private bool isFadingInResetMask = false;
		private bool isFadingOutResetMask = false;
		//
		private Transform glassesTrans;
		private Vector3 glassesEndPos;
		private Vector3 glassesStartPos;
		private bool moveGlasses = false;
		//
		private Renderer newLabel;
		// 1 = playAgain, 2 = main menu
		private int buttonSelected = 1;
		//
		private int setSize = 1;
		//
		private Vector3 starsCount = Vector3.zero;
		private int runningStarsInt = 0;
		//
		private int defaultBronzeStarFontSize;
		private int defaultSilverStarFontSize;
		private int defaultGoldStarFontSize;
		//
		private Vector3 _leftMoveDefault;
		private Vector3 _rightMoveDefault;
		private Vector3 _topMoveDefault;
		private Vector3 _bottomMoveDefault;
		//
		private Vector3 _leftMoveTarget;
		private Vector3 _rightMoveTarget;
		private Vector3 _topMoveTarget;
		private Vector3 _bottomMoveTargetClosed;
		private Vector3 _bottomMoveTargetOpen;
		// If the objects should be in their target position
		private bool _isInTargetMode = false;
		// If we are currently calculating the total scroe
		private bool _isCalculatingScore = false;
		// Used to display the stars count on the death screen
		private Vector3 _starsCountDisplayCopy = Vector3.zero;
		// Whether or not the social media foldout is open
		private bool _socialShareFoldoutOpen = false;
		// If the score the player got this round was a new high score
		private bool _isHighScore = false;
		// If the user can interact with the screen
		private bool _canInteract = false;
	
	#endregion
	
	#endregion
	
	
	#region Update
	
	// Main update loop
	// Called automatically once per frame
	void Update () 
	{ 
		//
		CheckForInput ();
		MoveObjects ();
		LerpFontSizes ();
		
		if (isFadingInResetMask) FadeInResetMask ();
		if (isFadingOutResetMask) FadeOutResetMask ();
		if (moveGlasses) MoveGlasses ();
		
		MoveHighlighter ();
	}
	
	
	//
	//
	void LerpFontSizes ()
	{
		yourScoreNumber.fontSize = (int) Mathf.Lerp (yourScoreNumber.fontSize, setSize, Time.deltaTime * 9.0f);
		bronzeStarNumText.fontSize = (int) Mathf.Lerp (bronzeStarNumText.fontSize, defaultBronzeStarFontSize, Time.deltaTime * 9.0f);
		silverStarNumText.fontSize = (int) Mathf.Lerp (silverStarNumText.fontSize, defaultSilverStarFontSize, Time.deltaTime * 9.0f);
		goldStarNumText.fontSize = (int) Mathf.Lerp (goldStarNumText.fontSize, defaultGoldStarFontSize, Time.deltaTime * 9.0f);
	}
	
	
	// Moves the button highlighter into the right position
	// Called every frame from Update ()
	void MoveHighlighter ()
	{
		float ypos = 0;
		switch (buttonSelected)
		{	
			// Play again
			case 1:
				ypos = highlighterPlayAgainYPos;
			break;
			// mainMenu
			case 2:
				ypos = highlighterMainMenuYPos;
			break;
		}
		highlighter.localPosition =  Vector3.Lerp (highlighter.localPosition, new Vector3 (highlighter.localPosition.x, ypos,highlighter.localPosition.z), Time.deltaTime * 15.0f);
	}
	
	
	// Fades in the reset mask
	// Called every frame from Update ()
	void FadeInResetMask ()
	{
		resetMaskRend.material.color = Color.Lerp (resetMaskRend.material.color, new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, resetMaskFadeInAlpha), Time.deltaTime * fadeInResetMaskRate);
	}
	
	
	// Fades out the reset mask
	// Called every frame from Update ()
	void FadeOutResetMask ()
	{
		resetMaskRend.material.color = Color.Lerp (resetMaskRend.material.color, new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f), Time.deltaTime * fadeOutResetMaskRate);
	}
	
	
	// Moves the high score glasses down from the top of the screen
	// Called every frame from Update ()
	void MoveGlasses ()
	{	
	/*
		if (glassesTrans.position.y > glassesEndPos.y)
		{
			glassesTrans.Translate (Vector3.down * Time.deltaTime * 4.0f);
		}
		else
		{
		//	SetMoveGlasses (false);
			glassesTrans.position = glassesStartPos;
			glassesTrans.renderer.enabled = false;
			
			//
			audioCont.PlaySound ("Cymbal");
			playerCont.ApplyShadesIfNeeded ();
			audioCont.StopDrumroll ();
			newLabel.enabled = true;
			
			screenFlash.FlashObject (20.0f, 0.01f, 20.0f);
		}*/
	}
	
	#endregion
	
	
	#region Object Lerp
	
	// Moves the on-screen objects into their current target positions
	// Called every frame from Update ()
	void MoveObjects ()
	{
		// If we aren't in target mode, we need to move our object offscreen
		if (!_isInTargetMode)
		{
			leftMoveInParent.localPosition = Vector3.Lerp (leftMoveInParent.localPosition, _leftMoveDefault, Time.deltaTime * transitionSlideSpeed);
			rightMoveInParent.localPosition = Vector3.Lerp (rightMoveInParent.localPosition, _rightMoveDefault, Time.deltaTime * transitionSlideSpeed);
			topMoveInParent.localPosition = Vector3.Lerp (topMoveInParent.localPosition, _topMoveDefault, Time.deltaTime * transitionSlideSpeed);
			bottomMoveInParent.localPosition = Vector3.Lerp (bottomMoveInParent.localPosition, _bottomMoveDefault, Time.deltaTime * transitionSlideSpeed);
		}
		else
		{
			leftMoveInParent.localPosition = Vector3.Lerp (leftMoveInParent.localPosition, _leftMoveTarget, Time.deltaTime * transitionSlideSpeed);
			rightMoveInParent.localPosition = Vector3.Lerp (rightMoveInParent.localPosition, _rightMoveTarget, Time.deltaTime * transitionSlideSpeed);
			topMoveInParent.localPosition = Vector3.Lerp (topMoveInParent.localPosition, _topMoveTarget, Time.deltaTime * transitionSlideSpeed);
			
			// If the social media foldout is open, we need to move it there. If not, it should be out of the way
			if (_socialShareFoldoutOpen)
				bottomMoveInParent.localPosition = Vector3.Lerp (bottomMoveInParent.localPosition, _bottomMoveTargetOpen, Time.deltaTime * transitionSlideSpeed * 2);
			else
				bottomMoveInParent.localPosition = Vector3.Lerp (bottomMoveInParent.localPosition, _bottomMoveTargetClosed, Time.deltaTime * transitionSlideSpeed * 2);
		}
	}
	
	#endregion
	
	
	#region Input
	
	// Allows user interaction after the death screen appears
	// Called from InitiateDeathScreen ()
	void AllowInteraction ()
	{
		_canInteract = true;
	} 
	
	
	// Responds to user input
	// Called every frame from Update ()
	void CheckForInput ()
	{
		// If we can't interact, get outta here
		if (!_canInteract)
			return;
			
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// If we are calculating the score, and it's a high score...
				if (_isCalculatingScore && _isHighScore)
				{
					SkipScoreCalc ();
					return;
				}
				
				// React to button touched
				ButtonTouched (inputCont.GetTouchRaycastObject ().name);
			}
			else if (_isCalculatingScore)
			{
				// If we tap on the screen, automatically skip to the score stuff
				SkipScoreCalc ();
			}
		}
	}
	
	
	// Responds to a specific button being touched
	// Called from CheckForInput ()
	void ButtonTouched (string buttonName)
	{
		// React differently based on the button that was touched...
		switch (buttonName)
		{
			case "MainMenuButton":
				if (buttonSelected != 2)
				{
					buttonSelected = 2;
					playAgainButton.GetComponent <tk2dSprite> ().color = buttonInactiveColor;
					mainMenuButton.GetComponent <tk2dSprite> ().color = Color.white;
					audioCont.PlayButtonPressSound ();
				}
				else
				{
					audioCont.PlaySound ("Button");
					eventManager.SetBackToMainMenuFromGame ();
				}
			break;
			case "PlayAgainButton":
				if (buttonSelected != 1)
				{
					buttonSelected = 1;
					playAgainButton.GetComponent <tk2dSprite> ().color = Color.white;
					mainMenuButton.GetComponent <tk2dSprite> ().color = buttonInactiveColor;
					audioCont.PlayButtonPressSound ();
				}
				else
				{
					audioCont.PlaySound ("Button");
					eventManager.SetRoundRestart ();
				}
			break;
		}
	}
	
	
	// Opens/closes the social media sharing foldout
	// Called automatically from Unity UI
	public void ToggleSocialFoldout ()
	{
		// If we can't interact, get outta here
		if (!_canInteract)
			return;
			
		audioCont.PlaySound ("Tab");
		_socialShareFoldoutOpen = !_socialShareFoldoutOpen;
		
		// Change the title of the social share button when you open it
		if (_socialShareFoldoutOpen) socialTabTitle.text = "time to brag";
		else socialTabTitle.text = "share";
	}
	
	
	// Initiates the tweeting process
	// Called automatically from Unity IU
	public void BeginTweeting ()
	{
		_socialShareFoldoutOpen = false;
		bottomMoveInParent.localPosition = _bottomMoveTargetClosed;
		
		audioCont.PlaySound ("Tab");
		shareCont.Tweet (scoreCont.GetCurrentTotalScoreWithStars ());
		
	}
	
	
	// Initiates the facebook sharing process
	// Called automatically from Unity IU
	public void BeginFacebookSharing ()
	{
		_socialShareFoldoutOpen = false;
		bottomMoveInParent.localPosition = _bottomMoveTargetClosed;
		
		audioCont.PlaySound ("Tab");
		shareCont.ShareToFacebook (scoreCont.GetCurrentTotalScoreWithStars ());
	}
	
	
	// Initiates the instagram posting process
	// Called automatically from Unity IU
	public void BeginInstagramPosting ()
	{
		_socialShareFoldoutOpen = false;
		bottomMoveInParent.localPosition = _bottomMoveTargetClosed;
		
		audioCont.PlaySound ("Tab");
		shareCont.PostToInstagram (scoreCont.GetCurrentTotalScoreWithStars ());
	}
	
	#endregion
	
	
	#region Scene Transitions
	
	// Sets up the death screen and displays it (happens immediately after the player has died)
	// Called from OnRoundEnd event trigger
	void InitiateDeathScreen ()
	{
		// Calculate the score for this round as well as the new total score
		SetActualScoreData ();
		
		// Set the initial on-screen score, before star calculation
		SetInitialPlayerScore ();
		
		// Freeze user interaction for a little bit to prevent accidental score skipping
		_canInteract = false;
		Invoke ("AllowInteraction", inputFreezeLength);
		
		// Start the rolling score calc process, assuming there's anything to process
		if (starsCount != Vector3.zero)
		{
			// Change the color gradient of the score number
			scoreGradient.topColor = topGradientPreFinishColor;
			scoreGradient.bottomColor = bottomGradientPreFinishColor;
			
			_isCalculatingScore = true;
			StartCoroutine ("BeginRollingScoring");
		}
		else
		{
			scoreGradient.topColor = topGradientPostFinishColor;
			scoreGradient.bottomColor = bottomGradientPostFinishColor;
		}
		
		// If the player got a high score this round...
		if (_isHighScore)
		{
			playerCont.SetIsWearingShades (true);
			playerCont.ApplyShadesIfNeeded ();
			PlayHighScoreEvents ();
			highScoreNumber.text = "";
		}
			
		// Move death screen objects into position
		_isInTargetMode = true;
		isFadingInResetMask = true;
		isFadingOutResetMask = false;
		camCont.SetIsInMainMenuMode (true);
		
		// Reset button settings
		highlighter.localPosition = new Vector3 (highlighter.localPosition.x, highlighterPlayAgainYPos, highlighter.localPosition.z);
		buttonSelected = 1;
		playAgainButton.GetComponent <tk2dSprite> ().color = Color.white;
		mainMenuButton.GetComponent <tk2dSprite> ().color = buttonInactiveColor;
		addedScoreNumber.text = "";
		socialTabTitle.text = "share";
		
		// Set a random sloth fact
		SetSlothFacts ();
	}
	
	
	// Initiates a new game from the death screen 
	// Called from OnRoundRestart event trigger
	void PlayAgain ()
	{
		// Clear all death screen stuff
		ClearDeathScreenProcessesUtility ();
		
		audioCont.SetScoreRunnerIsPlaying (false);
		addedScoreNumber.text = "";
	}
	
	
	// Initiates the return to the main menu
	// Called from OnBackToMainMenuFromGame event trigger
	void BeginMenuReturnReceiver ()
	{	
		// Clear all death screen stuff
		ClearDeathScreenProcessesUtility ();
		playerCont.UnApplyShadesFromDeathScreen ();
		
		// Tell the game controller to return to the main menu
		gameCont.BackToMainMenu ();
	}
	
	
	// Utility function to clear death screen processes
	// Called from PlayAgain () and BeginMenuReturnReceiver ()
	private void ClearDeathScreenProcessesUtility ()
	{
		// Move death screen objects out of the way
		isFadingInResetMask = false;
		isFadingOutResetMask = false;
		resetMaskRend.material.color = Color.clear;
		_socialShareFoldoutOpen = false;
		_isInTargetMode = false;
		if (_isHighScore) newHighLabel.EndMove ();
		_isHighScore = false;
		camCont.SetIsInMainMenuMode (false);
		foreach (ParticleSystem p in confettiHighScoreSystems) p.Stop ();
		
		// Stop Calculating the score
		_isCalculatingScore = false;
		StopAllCoroutines ();
		CancelInvoke ();
		runningStarsInt = 0;
	}
	
	#endregion
	
	
	#region Scoring
	
	// Updates the actual score data in the data controller
	// Called from InitiateDeathScreen ()
	void SetActualScoreData ()
	{
		// Calculate the score to be added to the total score
		starsCount = scoreCont.GetStarCount ();
		_starsCountDisplayCopy = starsCount;
		int starsAddNum = (int) (((int) starsCount.z * scoreCont.goldStarValue) + ((int) starsCount.y * scoreCont.silverStarValue) + ((int) starsCount.x * scoreCont.bronzeStarValue));
		dataCont.IncrementGoldStars ((int) starsCount.z);
		dataCont.IncrementSilverStars ((int) starsCount.y);
		dataCont.IncrementBronzeStars ((int) starsCount.x);
		dataCont.IncrementTotalStars ((int) starsCount.z + (int) starsCount.y + (int) starsCount.x);
		
		int scoreToAdd = (int) (scoreCont.GetPlayerScoreInt () + starsAddNum);
		dataCont.IncrementTotalScoreItemUsable (scoreToAdd);
		highScoreNumber.text = "high = " + dataCont.GetHighestScore ().ToString ();
	}
	
	
	// Sets the initial on-screen score, before any star points calculation has taken place
	// Called from InitiateDeathScreen ()
	void SetInitialPlayerScore ()
	{
		// Set the stars count
		goldStarNumText.text = starsCount.z.ToString ();
		silverStarNumText.text = starsCount.y.ToString ();
		bronzeStarNumText.text = starsCount.x.ToString ();
		goldStarNumText.color = Color.white; if (starsCount.z == 0) goldStarNumText.color = Color.gray;
		silverStarNumText.color = Color.white; if (starsCount.y == 0) silverStarNumText.color = Color.gray;
		bronzeStarNumText.color = Color.white; if (starsCount.x == 0) bronzeStarNumText.color = Color.gray;
		
		// Set the player's score before we change it
		yourScoreNumber.text = scoreCont.GetPlayerScore ();
	}
	
	
	// Initiates scoring only after the objects have slid into place
	// Called from InitiateDeathScreen ()
	IEnumerator BeginRollingScoring ()
	{
		// If the score object isn't close enough yet, wait to begin scoring
		while (leftMoveInParent.localPosition.x < (leftMoveXTarget - 0.05f))
			yield return null;
		
		// Snap the scoring object to it's position
		leftMoveInParent.localPosition = _leftMoveTarget;
		
		// Start the scoring process
		InvokeRepeating ("AddStarNumber", 0.3f, 0.45f);
	}
	
	
	// Adds up the number to be added based on the stars collected
	// Called from coroutine BeginRollingScoring ()
	void AddStarNumber ()
	{	
		// If we are done adding stars...
		if (_starsCountDisplayCopy == Vector3.zero)
		{
			audioCont.SetScoreRunnerIsPlaying (true);
			StartCoroutine ("AddStarsNumberToScore");
			CancelInvoke ("AddStarNumber");
		}
		// If we have bronze stars to add...
		if (_starsCountDisplayCopy.x != 0)
		{
			audioCont.PlaySound ("ScoreRunnerStar");
			bronzeStarNumText.fontSize = (int) (bronzeStarNumText.fontSize * 2f);
			bronzeStarNumText.color = Color.gray;
			addedScoreNumber.text = "+" + ((int) starsCount.x * scoreCont.bronzeStarValue).ToString ();
			runningStarsInt = (int) starsCount.x * scoreCont.bronzeStarValue;
			_starsCountDisplayCopy = new Vector3 (0, starsCount.y, starsCount.z);
		}
		// If we have silver stars to add...
		else if (_starsCountDisplayCopy.y != 0)
		{
			silverStarNumText.fontSize = (int) (silverStarNumText.fontSize * 2f);
			silverStarNumText.color = Color.gray;
			audioCont.PlaySound ("ScoreRunnerStar");
			addedScoreNumber.text = "+" + (runningStarsInt + ((int) starsCount.y * scoreCont.silverStarValue)).ToString ();
			runningStarsInt += (int) starsCount.y * scoreCont.silverStarValue;
			_starsCountDisplayCopy = new Vector3 (0, 0, starsCount.z);
		}
		// If we have gold stars to add...
		else if (_starsCountDisplayCopy.z != 0)
		{
			goldStarNumText.fontSize = (int) (goldStarNumText.fontSize * 2f);
			goldStarNumText.color = Color.gray;
			audioCont.PlaySound ("ScoreRunnerStar");
			addedScoreNumber.text = "+" + (runningStarsInt + ((int) starsCount.z * scoreCont.goldStarValue)).ToString ();
			runningStarsInt += (int) starsCount.z * scoreCont.goldStarValue;
			_starsCountDisplayCopy = Vector3.zero;
		}
	}
	
	
	// Gradually adds the star numbers to the total score
	// Called from AddStarNumber ()
	IEnumerator AddStarsNumberToScore ()
	{
		// While the transform parent is still above the target, drop it
		while (addedScoreNumber.text != "" && int.Parse (addedScoreNumber.text) > 0)
		{
			yourScoreNumber.text = (int.Parse (yourScoreNumber.text) + 1).ToString ();
			addedScoreNumber.text = "+" + (int.Parse (addedScoreNumber.text) - 1).ToString ();
			
			yield return null;
		}
		
		// We are done adding to the score
		ScoreRunnerIsDone ();
	}
	
	
	// Allows the player to skip the score calculation process and immediately see the results
	// Called from CheckForInput ()
	void SkipScoreCalc ()
	{
		// Stop the calculating process
		CancelInvoke ("AddStarNumber");
		StopAllCoroutines ();
		
		// Bounce fonts
		goldStarNumText.fontSize = (int) (goldStarNumText.fontSize * 2f);
		silverStarNumText.fontSize = (int) (silverStarNumText.fontSize * 2f);
		bronzeStarNumText.fontSize = (int) (bronzeStarNumText.fontSize * 2f);
		bronzeStarNumText.color = Color.gray;
		silverStarNumText.color = Color.gray;
		goldStarNumText.color = Color.gray;
		
		// 
		ScoreRunnerIsDone ();
	}
	
	
	// Executed when the score is done being added to 
	// Called from SkipScoreCalc () and coroutine AddStarsNumberToScore ()
	private void ScoreRunnerIsDone ()
	{
		//
		audioCont.PlaySound ("ScoreRunnerDone");
		yourScoreNumber.text = scoreCont.GetCurrentTotalScoreWithStars ().ToString ();
		addedScoreNumber.text = "";
		_isCalculatingScore = false;
		yourScoreNumber.fontSize = (int) (yourScoreNumber.fontSize * 1.5f);
		audioCont.SetScoreRunnerIsPlaying (false);
		runningStarsInt = 0;
		scoreGradient.topColor = topGradientPostFinishColor;
		scoreGradient.bottomColor = bottomGradientPostFinishColor;
	}
	
	
	// Starts the sequence of events that play when a new high score is achieved
	// Called from InitiateDeathScreen () and ScoreRunnerIsDone ()
	void PlayHighScoreEvents ()
	{
		// Play the confetti particle systems
		foreach (ParticleSystem p in confettiHighScoreSystems) p.Play ();
		newHighLabel.BeginMove ();
		
		// Play the sound effects
		audioCont.PlaySound ("LandHard");
		audioCont.PlaySound ("Applause");
	}
	
	
	// Tells the death screen to register this as a high score
	// Called from AddPlayerScoreToHighScores () in ScoreController.cs
	public void SetIsHighScore ()
	{
		_isHighScore = true;
	}
	
	
	//
	//
	public void GoHighScoreFlash ()
	{
		/*
		if (_isHighScore)
		{
			playerCont.SetIsWearingShades (true);
			playerCont.ApplyShadesIfNeeded ();
			screenFlash.FlashObject (15, 0.15f, 15);
			audioCont.PlaySound ("Shing");
		}*/
	}
	
	#endregion
	
	
	#region Public
	
	// Sets the sloth fact for this screen iteration
	//
	private void SetSlothFacts ()
	{
		// Extract file text into array of strings
		TextAsset txt = (TextAsset) Resources.Load ("slothFacts", typeof (TextAsset));
		string content = txt.text;
		List <string> factLines = new List <string> (content.Split ('\n'));
		int factNum = Random.Range (1, (factLines.Count / 3) + 1);
		
		// Get strings
		string line1 = factLines [(factNum * 3) - 3];
		dataCont.CheckFactLearned (int.Parse (line1.Split ('|') [0]));
		string factLine1 = line1.Split ('|') [1];
		string factLine2 = factLines [(factNum * 3) - 2];
		if (factLine2.Contains ("-"))
			factLine2 = "";
		
		// Set strings
		dykText1.text = factLine1;
		dykText2.text = factLine2;
	}
	
	#endregion
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundRestart += PlayAgain;
		EventManager.OnRoundEnd += InitiateDeathScreen;
		EventManager.OnBackToMainMenuFromGame += BeginMenuReturnReceiver;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundRestart -= PlayAgain;
		EventManager.OnRoundEnd -= InitiateDeathScreen;
		EventManager.OnBackToMainMenuFromGame -= BeginMenuReturnReceiver;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start () 
	{
		AssignVariables ();
	}
	
	
	// Assigns the local variables
	// Called from Start ()
	private void AssignVariables ()
	{
		inputCont = GetComponent <InputController> ();
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		newHighLabel = GameObject.Find ("*HSLLettersParent").GetComponent <NewHighScoreLabel> ();
		scoreGradient = GameObject.Find ("ScoreNum").GetComponent <Gradient> ();
		dataCont = GetComponent <DataController> ();
		gameCont = GetComponent <GameController> ();
		audioCont = GetComponent <AudioController> ();
		shareCont = GetComponent <SocialShare> ();
		eventManager = GetComponent <EventManager> ();
		camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		resetMaskRend = GameObject.Find ("ScreenMask").renderer;
		screenFlash = GameObject.Find ("ScreenFlasher").GetComponent <Flash> ();
		playerCont = GameObject.Find ("Player").GetComponent <PlayerController> ();
		dykText1.text = "";
		dykText2.text = "";
		setSize = yourScoreNumber.fontSize;
		defaultBronzeStarFontSize = bronzeStarNumText.fontSize;
		defaultSilverStarFontSize = silverStarNumText.fontSize;
		defaultGoldStarFontSize = goldStarNumText.fontSize;
		
		// Set default positions
		_leftMoveDefault = leftMoveInParent.localPosition;
		_rightMoveDefault = rightMoveInParent.localPosition;
		_topMoveDefault = topMoveInParent.localPosition;
		_bottomMoveDefault = bottomMoveInParent.localPosition;
		
		// Set target positions
		_leftMoveTarget = new Vector3 (leftMoveXTarget, _leftMoveDefault.y, _leftMoveDefault.z);
		_rightMoveTarget = new Vector3 (rightMoveXTarget, _rightMoveDefault.y, _rightMoveDefault.z);
		_topMoveTarget = new Vector3 (_topMoveDefault.x, topMoveYTarget, _topMoveDefault.z);
		_bottomMoveTargetClosed = new Vector3 (_bottomMoveDefault.x, bottomMoveYTargetClosed, _bottomMoveDefault.z);
		_bottomMoveTargetOpen = new Vector3 (_bottomMoveDefault.x, bottomMoveYTargetOpen, _bottomMoveDefault.z);
	}
	
	#endregion
}
