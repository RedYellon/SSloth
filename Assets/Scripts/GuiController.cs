/*
 	GuiController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	June 18, 2014
 	
 	Coordinates the end-of-game screen and info.
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;	// Used for lists


public class GuiController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The text that holds your score for this round
		public TextMesh yourScoreNumber;
		// The text that holds your total cumulative score
		public TextMesh totalScoreNumber;
		// The text that shows the score added from the stars
		public TextMesh addedScoreNumber;
		// The text that shows the player's highest score
		public TextMesh highScoreNumber;
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
		public TextMesh goldStarNumText;
		public TextMesh silverStarNumText;
		public TextMesh bronzeStarNumText;
		//
		public Transform highlighter;
		//
		public Transform leftMoveInParent;
		public Transform rightMoveInParent;
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
		// The facebook controller
		FacebookController fbCont;
		//
		EventManager eventManager;
	
		#endregion
	
		#region Private
		
		//
		private bool canTouchButtons = true;
		// The reset mask object renderer
		private Renderer resetMaskRend;
		//
		private bool isFadingInResetMask = false;
		private bool isFadingOutResetMask = false;
		// The did you know text meshes
		private TextMesh dykText1;
		private TextMesh dykText2;
		//
		private Transform glassesTrans;
		private Vector3 glassesEndPos;
		private Vector3 glassesStartPos;
		private bool moveGlasses = false;
		//
		private Renderer newLabel;
		//
		private bool isTransitioning = false;
		//
		private float transitionSlideSpeed = 15.0f;
		// 1 = playAgain, 2 = main menu
		private int buttonSelected = 1;
		//
		private int setSize = 1;
		//
		private Vector3 starsCount = Vector3.zero;
		private int runningStarsInt = 0;
		private bool isAddingStars = false;
		//
		private int defaultBronzeStarFontSize;
		private int defaultSilverStarFontSize;
		private int defaultGoldStarFontSize;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main update loop
	// Called automatically once per frame
	void Update () 
	{ 
		//
		CheckForInput ();
		LerpFontSizes ();
		
		if (isFadingInResetMask) FadeInResetMask ();
		if (isFadingOutResetMask) FadeOutResetMask ();
		
		//
		if (moveGlasses) MoveGlasses ();
		
		if (!isTransitioning)
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
	
	
	// Fades in the reset mask at the time
	// Called from Setup ()
	void FadeInResetMaskStart ()
	{
		isFadingOutResetMask = false;
		isFadingInResetMask = true;
	}
	
	
	// Moves the high score glasses down from the top of the screen
	// Called every frame from Update ()
	void MoveGlasses ()
	{	
		if (glassesTrans.position.y > glassesEndPos.y)
		{
			glassesTrans.Translate (Vector3.down * Time.deltaTime * 4.0f);
		}
		else
		{
			SetMoveGlasses (false);
			glassesTrans.position = glassesStartPos;
			glassesTrans.renderer.enabled = false;
			
			//
			audioCont.PlaySound ("Cymbal");
			playerCont.ApplyShadesIfNeeded ();
			audioCont.StopDrumroll ();
			newLabel.enabled = true;
			
			screenFlash.FlashObject (20.0f, 0.01f, 20.0f);
		}
	}
	
	#endregion
	
	
	#region Input
	
	// Responds to user input
	// Called every frame from Update ()
	void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown () && canTouchButtons)
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// React to button touched
				ButtonTouched (inputCont.GetTouchRaycastObject ().name);
			}
			else if (addedScoreNumber.renderer.enabled)
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
					eventManager.SetRoundRestart ();
				}
			break;
			case "FacebookButtonEndGame":
				fbCont.ShareToFacebook (scoreCont.GetCurrentTotalScoreWithStars ());
			break;
			case "TwitterButtonEndGame":
				fbCont.Tweet (scoreCont.GetCurrentTotalScoreWithStars ());
			break;
		}
	}
	
	#endregion
	
	
	#region Scene Setup
	
	// Begins the slide in transition of the screen
	//
	void BeginSlideTransition ()
	{
		isTransitioning = true;
		canTouchButtons = false;
		Setup ();
		StartCoroutine ("SlideIn");
	}
	
	
	// Sets up the end of game screen
	// Called from BeginSlideTransition ()
	void Setup ()
	{
		// Calculate the score for this round as well as the new total score
		SetActualScoreData ();
		
		// We need to delay allowing the player to restart by a fraction of a second to avoid jamming the screen mask
		isTransitioning = false;
		
		Invoke ("FadeInResetMaskStart", fadeInResetMaskDelayTime);
		mainMenuButton.SetActive (true);
		playAgainButton.SetActive (true);
		
		// Turn on all of the assorted renderers
		foreach (Renderer r in rends)
		{
			r.enabled = true;
		}
		
		// Set a random sloth fact
		SetSlothFacts ();
		
		// Set the stars count
		addedScoreNumber.text = "";
		Vector3 stars = scoreCont.GetStarCount ();
		goldStarNumText.text = stars.z.ToString ();
		silverStarNumText.text = stars.y.ToString ();
		bronzeStarNumText.text = stars.x.ToString ();
		
		// Set the player's score before we change it
		yourScoreNumber.text = scoreCont.GetPlayerScore ();
		yourScoreNumber.gameObject.renderer.enabled = true;
		
		// Set the cumulative score before we change it
		totalScoreNumber.text = dataCont.GetTotalScoreItemUsable ().ToString ();
	}
	
	
	// Slides the content in from the sides
	// Called from BeginSlideTransition ()
	IEnumerator SlideIn ()
	{
		// While the transform parent is still far enough away from the target, move it
		while (leftMoveInParent.localPosition.x < -0.1f)
		{
			leftMoveInParent.localPosition = Vector3.Lerp (leftMoveInParent.localPosition, Vector3.zero, Time.deltaTime * transitionSlideSpeed);
			rightMoveInParent.localPosition = Vector3.Lerp (rightMoveInParent.localPosition, Vector3.zero, Time.deltaTime * transitionSlideSpeed);
			
			yield return null;
		}
		
		//
		leftMoveInParent.localPosition = Vector3.zero;
		rightMoveInParent.localPosition = Vector3.zero;
		
		//
		canTouchButtons = true;
		
		// Start the scoring process
		starsCount = scoreCont.GetStarCount ();
		InvokeRepeating ("AddStarNumber", 0.35f, 0.5f);
	}
	
	#endregion
	
	
	#region Scene Cleanup
	
	// Slides the content out away from the center of the screen
	//
	IEnumerator SlideOut ()
	{
		// While the transform parent is still near the target, move it
		while (leftMoveInParent.localPosition.x > -14.8f)
		{
			leftMoveInParent.localPosition = Vector3.Lerp (leftMoveInParent.localPosition, new Vector3 (-15, 0, 0), Time.deltaTime * (transitionSlideSpeed * 0.5f));
			rightMoveInParent.localPosition = Vector3.Lerp (rightMoveInParent.localPosition, new Vector3 (15, 0, 0), Time.deltaTime * (transitionSlideSpeed * 0.5f));
			
			yield return null;
		}
		
		//
		leftMoveInParent.localPosition = new Vector3 (-15, 0, 0);
		rightMoveInParent.localPosition = new Vector3 (15, 0, 0);
		
		// If we started another game, start it
		if (buttonSelected == 1)
			GameStarted ();
		// If we were returning to the main menu, return to it
		else
			gameCont.BackToMainMenu ();
	}
	
	
	//
	//
	void BeginMenuReturnReceiver ()
	{
		isFadingInResetMask = false;
		isFadingOutResetMask = true;
		
		//
		StopCoroutine ("SlideIn");
		StartCoroutine ("SlideOut");
		audioCont.PlaySound ("Button");
	}
	
	
	// Cleans up the end of game screen
	// Called from Cleanup () in GameController.cs
	public void Cleanup ()
	{
		GameStarted ();
		isFadingOutResetMask = false;
		resetMaskRend.material.color = new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f);
	}
	
	
	//
	//
	void RestartRoundEventReceiver ()
	{
		// Fade out the reset mask
		isFadingInResetMask = false;
		isFadingOutResetMask = true;
		audioCont.SetScoreRunnerIsPlaying (false);
		CancelInvoke ();
		StopCoroutine ("SlideIn");
		StartCoroutine ("SlideOut");
		
		audioCont.PlaySound ("Button");
		
		//
		audioCont.StopDrumroll ();
		SetMoveGlasses (false);
		glassesTrans.position = glassesStartPos;
		glassesTrans.renderer.enabled = false;
	}
	
	
	// Called when a new game has been started
	// Called from SlideOut () and Cleanup ()
	void GameStarted ()
	{
		canTouchButtons = true;
		SetMoveGlasses (false);
		glassesTrans.position = glassesStartPos;
		audioCont.SetScoreRunnerIsPlaying (false);
		runningStarsInt = 0;
		glassesTrans.position = glassesStartPos;
		addedScoreNumber.text = "";
		addedScoreNumber.gameObject.renderer.enabled = false;
		isAddingStars = false;
		newLabel.enabled = false;
		isTransitioning = false;
		mainMenuButton.SetActive (false);
		playAgainButton.SetActive (false);
		buttonSelected = 1;
		playAgainButton.GetComponent <tk2dSprite> ().color = Color.white;
		mainMenuButton.GetComponent <tk2dSprite> ().color = buttonInactiveColor;
		glassesTrans.renderer.enabled = false;
		
		// Reset stars
		goldStarNumText.text = "-";
		silverStarNumText.text = "-";
		bronzeStarNumText.text = "-";
		
		// Set all the proper text pieces to be disabled
		foreach (Renderer r in rends)
		{
			r.enabled = false;
		}
		//highScoreNumber.gameObject.renderer.enabled = false;
		yourScoreNumber.gameObject.renderer.enabled = false;
		
		// Remove the did you know text
		dykText1.text = "";
		dykText2.text = "";
		
		// Make sure the parent transforms have moved all the way to their original positions
		leftMoveInParent.localPosition = new Vector3 (-15, 0, 0);
		rightMoveInParent.localPosition = new Vector3 (15, 0, 0);
		
		// Cancel any outstanding invokes
		CancelInvoke ();
	}
	
	#endregion
	
	
	#region Scoring
	
	// Updates the actual score data in the data controller
	// Called from Setup ()
	void SetActualScoreData ()
	{
		// Calculate the score to be added to the total score
		Vector3 stars = scoreCont.GetStarCount ();
		int starsAddNum = (int) (((int) stars.z * scoreCont.goldStarValue) + ((int) stars.y * scoreCont.silverStarValue) + ((int) stars.x * scoreCont.bronzeStarValue));
		if (starsAddNum != 0)
		{
			addedScoreNumber.gameObject.renderer.enabled = true;
		}
		int scoreToAdd = (int) (scoreCont.GetPlayerScoreInt () + starsAddNum);
		dataCont.IncrementTotalScoreItemUsable (scoreToAdd);
		totalScoreNumber.text = dataCont.GetTotalScoreItemUsable ().ToString ();
		highScoreNumber.text = dataCont.GetHighestScore ().ToString ();
	}
	
	
	// Adds up the number to be added based on the stars collected
	// Called from SlideIn ()
	void AddStarNumber ()
	{	
		// If there are no stars, we know we are done
		if (starsCount == Vector3.zero)
		{
			if (isAddingStars)
			{
				isAddingStars = false;
				audioCont.SetScoreRunnerIsPlaying (true);
				CancelInvoke ("AddStarNumber");
				StartCoroutine ("AddStarsNumberToScore");
			}
			else
			{
				CancelInvoke ("AddStarNumber");
				return;
			}
		}
		else if (starsCount.x != 0)
		{
			isAddingStars = true;
			audioCont.PlaySound ("ScoreRunnerStar");
			bronzeStarNumText.fontSize = (int) (bronzeStarNumText.fontSize * 2f);
			addedScoreNumber.text = "+" + ((int) starsCount.x * scoreCont.bronzeStarValue).ToString ();
			runningStarsInt = (int) starsCount.x * scoreCont.bronzeStarValue;
			starsCount = new Vector3 (0, starsCount.y, starsCount.z);
		}
		else if (starsCount.y != 0)
		{
			isAddingStars = true;
			silverStarNumText.fontSize = (int) (silverStarNumText.fontSize * 2f);
			audioCont.PlaySound ("ScoreRunnerStar");
			addedScoreNumber.text = "+" + (runningStarsInt + ((int) starsCount.y * scoreCont.silverStarValue)).ToString ();
			runningStarsInt += (int) starsCount.y * scoreCont.silverStarValue;
			starsCount = new Vector3 (0, 0, starsCount.z);
		}
		else if (starsCount.z != 0)
		{
			isAddingStars = true;
			goldStarNumText.fontSize = (int) (goldStarNumText.fontSize * 2f);
			audioCont.PlaySound ("ScoreRunnerStar");
			addedScoreNumber.text = "+" + (runningStarsInt + ((int) starsCount.z * scoreCont.goldStarValue)).ToString ();
			runningStarsInt += (int) starsCount.z * scoreCont.goldStarValue;
			starsCount = Vector3.zero;
		}
	}
	
	
	// Gradually adds the star numbers to the total score
	// Called from AddStarNumber ()
	IEnumerator AddStarsNumberToScore ()
	{
		// While the transform parent is still above the target, drop it
		while (!isTransitioning && addedScoreNumber.text != "" && int.Parse (addedScoreNumber.text) > 0)
		{
			yourScoreNumber.text = (int.Parse (yourScoreNumber.text) + 1).ToString ();
			addedScoreNumber.text = (int.Parse (addedScoreNumber.text) - 1).ToString ();
			
			yield return null;
		}
		
		addedScoreNumber.text = "";
		yourScoreNumber.fontSize = (int) (yourScoreNumber.fontSize * 1.75f);
		audioCont.SetScoreRunnerIsPlaying (false);
		if (goldStarNumText.text != "-") audioCont.PlaySound ("ScoreRunnerDone");
		addedScoreNumber.renderer.enabled = false;
		runningStarsInt = 0;
	}
	
	
	//
	//
	void SkipScoreCalc ()
	{
		CancelInvoke ("AddStarNumber");
		StopCoroutine ("AddStarsNumberToScore");
		addedScoreNumber.renderer.enabled = false;
		audioCont.PlaySound ("ScoreRunnerDone");
		addedScoreNumber.text = "";
		yourScoreNumber.fontSize = (int) (yourScoreNumber.fontSize * 1.75f);
		yourScoreNumber.text = scoreCont.GetCurrentTotalScoreWithStars ().ToString ();
		audioCont.SetScoreRunnerIsPlaying (false);
		runningStarsInt = 0;
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
		string factLine1 = factLines [(factNum * 3) - 3];
		string factLine2 = factLines [(factNum * 3) - 2];
		if (factLine2.Contains ("-"))
			factLine2 = "";
		
		// Set strings
		dykText1.text = factLine1;
		dykText2.text = factLine2;
	}
	
	
	// Sets whether or not the high-score glasses should be descending from the sky
	//
	public void SetMoveGlasses (bool b)
	{
		moveGlasses = b;
		if (b)
		{
			audioCont.PlayDrumroll ();
			glassesTrans.renderer.enabled = true;
		}
	}
	
	#endregion
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundRestart += RestartRoundEventReceiver;
		EventManager.OnRoundEnd += BeginSlideTransition;
		EventManager.OnBackToMainMenuFromGame += BeginMenuReturnReceiver;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundRestart -= RestartRoundEventReceiver;
		EventManager.OnRoundEnd -= BeginSlideTransition;
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
		dataCont = GetComponent <DataController> ();
		gameCont = GetComponent <GameController> ();
		audioCont = GetComponent <AudioController> ();
		fbCont = GetComponent <FacebookController> ();
		eventManager = GetComponent <EventManager> ();
		resetMaskRend = GameObject.Find ("ScreenMask").renderer;
		dykText1 = GameObject.Find ("DYKTextLine1").GetComponent <TextMesh> ();
		dykText2 = GameObject.Find ("DYKTextLine2").GetComponent <TextMesh> ();
		screenFlash = GameObject.Find ("ScreenFlasher").GetComponent <Flash> ();
		newLabel = GameObject.Find ("newLabel").renderer;
		glassesTrans = GameObject.Find ("SlothGlasses").transform;
		playerCont = GameObject.Find ("Player").GetComponent <PlayerController> ();
		glassesEndPos = glassesTrans.position;
		glassesStartPos = new Vector3 (glassesTrans.position.x, 7.175864f, glassesTrans.position.z);
		glassesTrans.position = glassesStartPos;
		dykText1.text = "";
		dykText2.text = "";
		yourScoreNumber.gameObject.renderer.enabled = false;
		setSize = yourScoreNumber.fontSize;
		defaultBronzeStarFontSize = bronzeStarNumText.fontSize;
		defaultSilverStarFontSize = silverStarNumText.fontSize;
		defaultGoldStarFontSize = goldStarNumText.fontSize;
	}
	
	#endregion
}
