 /*
 	MainMenuController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 14, 2014
 	Last Edited:	November 28, 2014
 	
 	Controls the functions and behavior of the main menu "scene".
*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The button sizes
		public Vector3 buttonSmallScale = new Vector3 (1, 1, 1);
		public Vector3 buttonLargeScale = new Vector3 (1.2f, 1.2f, 1.2f);
		// How quickly the buttons scale when they become active/inactive
		public float buttonGrowSpeed = 10.0f;
		// The button transforms
		public Transform scoresButton;
		public Transform playButton;
		public Transform facebookButton;
		public Transform twitterButton;
		// The menu highlighter
		public Transform highlighter;
		// The positions (Y) for the highlighter on different buttons
		public float startHighlighterYPos;
		public float hsHighlighterYPos;
		public float optionsHighlighterYPos;
		public float creditsHighlighterYPos;
		// The color the buttons turn when they become inactive
		public Color buttonInactiveColor;
		//
		public Transform transitionDropParent;
		//
		public float transitionDropYTarget;
		//
		public RectTransform optionsFoldout;
		public GameObject optionsMasterButton;
		public float optionsFoldoutOnscreenPosition = -211.81f;
		//
		public RectTransform creditsScrollRect;
		public GameObject scrollThing;
		public float csrOnscreenPositionY;
		//
		public Transform buttonNavParent;
		//
		public Transform ospinner1;
		public Transform ospinner2;
		// The christmas ornaments objects
		public GameObject [] christmasOrnaments;
		
		#endregion
	
		#region Scripts
		
		// The data controller
		DataController dataCont;
		// The input controller
		InputController inputCont;
		// The scene controller
		SceneController sceneCont;
		// The camera controller
		CameraController camCont;
		// The audio controller
		AudioController audioCont;
		// The time controller
		TimeController timeCont;
		// The main menu background transition script
		MenuBackgroundTransitionController transitionCont;
		// The event manager
		EventManager eventManager;
	
		#endregion
		
		#region Private
		
		// All of the renderers
		public Renderer [] mainMenuRenderers;
		// The transform of the main title
		private Transform mainTitleTransform;
		// The transform of the super title
		private Transform superTitleTransform;
		// The target position of the main title
		private Vector3 targetTitlePos;
		// The target position of the super title
		private Vector3 superTargetTitlePos;
		// The target position of the main title
		private Vector3 targetTitleStartPos;
		// The target position of the super title
		private Vector3 superTargetTitleStartPos;
		// The current active button
		// 1 = settings, 2 = scores, 3 = play, 4 = slothlopedia, 5 = credits
		private int currentActiveButton = 3;
		// If the titles are moving yet
		private bool titlesAreMoving = false;
		private bool superTitlesAreMoving = false;
		private bool didAnimate = false;
		//
		private Vector3 highlighterTargetPosition;
		//
		private Renderer highlighterRend;
		private Renderer highlighterCapRend;
		// The button sprites
		private tk2dSprite startButtonSprite;
		private tk2dSprite hsButtonSprite;
		//
		private bool sceneIsTransitioning = false;
		//
		private float transitionDropSpeed = 0.0f;
		private float cloudsDropSpeed = 0.0f;
		//
		private Transform cloudsParentTransform;
		private Transform grassParentTransform;
		private BounceObject cloudsBounce;
		//
		private bool isInMainMenu = true;
		float optionsFoldoutOffscreenPosition = 0;
		bool optionsFoldoutIsShowing = false;
		bool creditsScrollerIsShowing = false;
		float csrOffscreenPositionY;
		bool bnpIsOnScreen = false;
		//
		private Vector3 title1ResetPos;
		private Vector3 title2ResetPos;
		// If the camera type has been overridden (to stretch)
		private bool _camOverrideType = false;
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
	
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Update is called once per frame
	void Update () 
	{
		// Check for and respond to input
		if (!sceneIsTransitioning) CheckForInput ();
		
		// Set button sizes
		SetButtonSizes ();
		
		//
		MoveOptionsFoldout ();
		MoveCreditsScroller ();
		MoveButtonNavParent ();
		
		// Move the highlighter
		if (!sceneIsTransitioning) MoveHighlighter ();
		
		// If the titles should be moving, move them
		if (titlesAreMoving) MoveTitle ();
		if (superTitlesAreMoving) MoveSuperTitle ();
	}
	
	#endregion
	
	
	#region Transform Update
	
	// Sets the sizes of the buttons, depending on if they are active or not
	// Called every frame from Update ()
	void SetButtonSizes ()
	{
		// Scores Button
		if (currentActiveButton == 2)
			scoresButton.localScale = Vector3.Lerp (scoresButton.localScale, buttonLargeScale, Time.deltaTime * buttonGrowSpeed);
		else
			scoresButton.localScale = Vector3.Lerp (scoresButton.localScale, buttonSmallScale, Time.deltaTime * buttonGrowSpeed);
			
		// Play Button
		if (currentActiveButton == 3)
			playButton.localScale = Vector3.Lerp (playButton.localScale, buttonLargeScale, Time.deltaTime * buttonGrowSpeed);
		else
			playButton.localScale = Vector3.Lerp (playButton.localScale, buttonSmallScale, Time.deltaTime * buttonGrowSpeed);
	}
	
	
	// Moves the menu highlighter over the correct (currently actuve) menu option
	// Called every frame from Update ()
	void MoveHighlighter ()
	{
		highlighter.localPosition = Vector3.Lerp (highlighter.localPosition, highlighterTargetPosition, Time.deltaTime * 15.0f);
	}
	
	
	// Moves half of the title into position
	// Called every frame from Update ()
	void MoveTitle ()
	{
		mainTitleTransform.position = Vector3.Lerp (mainTitleTransform.position, targetTitlePos, Time.deltaTime * 10.0f);
	}
	
	
	// Moves half of the title into position
	// Called every frame from Update ()
	void MoveSuperTitle ()
	{
		superTitleTransform.position = Vector3.Lerp (superTitleTransform.position, superTargetTitlePos, Time.deltaTime * 10.0f);
	}
	
	#endregion
	
	
	#region Options Foldout
	
	// Switches the camera display settings
	// Called directly from Unity UI
	public void SwitchCameraDisplayType ()
	{
		// Switch the override type
		int type = 0;
		if (!_camOverrideType) type = 1;
		_camOverrideType = !_camOverrideType;
		
		// Tell the camera to switch modes
		camCont.SetDisplayMode (type);
		dataCont.SaveCamOverrideType (type);
		audioCont.PlayButtonPressSound ();
	}
	
	#endregion
	
	
	#region Input
	
	// Collects user input
	// Called every frame from Update ()
	void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// React
				ButtonTouched (inputCont.GetTouchRaycastObject ());
			}
			
			if ((Input.mousePosition.x < (Screen.width / 2)) && creditsScrollerIsShowing)
			{
				creditsScrollerIsShowing = false;
				scrollThing.SetActive (false);
				scrollThing.transform.parent.gameObject.GetComponent <ScrollRect> ().StopMovement ();
				scrollThing.transform.localPosition = new Vector3 (scrollThing.transform.localPosition.x, -15.81f, scrollThing.transform.localPosition.z);
				bnpIsOnScreen = true;
				optionsFoldoutIsShowing = false;
				optionsMasterButton.SetActive (true);
				optionsFoldout.gameObject.SetActive (true);
			}
		}
	}
	
	
	// Responds to a button touch
	// Called from CheckForInput () when a button is touched
	void ButtonTouched (Transform button)
	{
		// Play the button touched sound
		if (isInMainMenu) 
			audioCont.PlayButtonPressSound ();
			
		// React to the button press depending on the button pressed
		switch (button.name)
		{	
			// If we touched the scores button
			case "ScoresButton":
			
				if (currentActiveButton == 2)
				{
					audioCont.PlaySound ("Button");
					optionsFoldoutIsShowing = false;
					bnpIsOnScreen = false;
					BeginTransitionToScene ();
				}
				else
				{
					currentActiveButton = 2;
					SetButtonsToInactiveColor ();
					hsButtonSprite.color = Color.white;
					highlighterTargetPosition = new Vector3 (highlighter.localPosition.x, hsHighlighterYPos, highlighter.localPosition.z);
				}
				
			break;
			
			// If we touched the play button
			case "PlayButton":
			
				if (currentActiveButton == 3)
				{
					audioCont.PlaySound ("Button");
					optionsFoldoutIsShowing = false;
					bnpIsOnScreen = false;
					BeginTransitionToScene ();
				}
				else
				{
					currentActiveButton = 3;
					SetButtonsToInactiveColor ();
					startButtonSprite.color = Color.white;
					highlighterTargetPosition = new Vector3 (highlighter.localPosition.x, startHighlighterYPos, highlighter.localPosition.z);
				}
				
			break;
		}
	}
	public void FBButtonPressed () { Application.OpenURL ("https://www.facebook.com/pages/Super-Sloth/272901039540746"); }
	public void TwitterButtonPressed () { Application.OpenURL ("https://twitter.com/SuperSlothGame"); }
	
	
	// Sets the colors of all of the buttons to the inactive color
	// Called from ButtonTouched (Transform button) and BeginFadeInStartButton () 
	void SetButtonsToInactiveColor ()
	{
		startButtonSprite.color = buttonInactiveColor;
		hsButtonSprite.color = buttonInactiveColor;
	}
	
	
	//
	//
	void MoveButtonNavParent ()
	{
		if (bnpIsOnScreen)
		{
			buttonNavParent.localPosition = Vector3.Lerp (buttonNavParent.localPosition, new Vector3 (0, buttonNavParent.localPosition.y, buttonNavParent.localPosition.z), Time.deltaTime * 10.0f);
		}
		else
		{
			buttonNavParent.localPosition = Vector3.Lerp (buttonNavParent.localPosition, new Vector3 (10f, buttonNavParent.localPosition.y, buttonNavParent.localPosition.z), Time.deltaTime * 10.0f);
		}
	}
	
	
	//
	//
	public void OptionsFoldoutButtonPressed ()
	{
		// Flip the showing bool
		optionsFoldoutIsShowing = !optionsFoldoutIsShowing;
	}
	
	
	//
	//
	public void CreditsScrollButtonPressed ()
	{
		// Flip the showing bool
		creditsScrollerIsShowing = true;
		bnpIsOnScreen = false;
		scrollThing.SetActive (true);
		optionsFoldout.gameObject.SetActive (false);
		optionsMasterButton.SetActive (false);
	}	
	
	
	//
	//
	void MoveOptionsFoldout ()
	{	
		float yV = optionsFoldoutOffscreenPosition; if (optionsFoldoutIsShowing) yV = optionsFoldoutOnscreenPosition;
		Vector3 targ = new Vector3 (optionsFoldout.localPosition.x, yV, optionsFoldout.localPosition.z);
		optionsFoldout.localPosition = Vector3.Lerp (optionsFoldout.localPosition, targ, Time.deltaTime * 15.0f);
		
		//
		if (optionsFoldoutIsShowing)
		{
			ospinner1.Rotate (Vector3.forward * Time.deltaTime * 50);
			ospinner2.Rotate (Vector3.back * Time.deltaTime * 50);
		}
	}
	
	
	//
	//
	void MoveCreditsScroller ()
	{	
		float yV = csrOffscreenPositionY; if (creditsScrollerIsShowing) yV = csrOnscreenPositionY;
		Vector3 targ = new Vector3 (creditsScrollRect.localPosition.x, yV, creditsScrollRect.localPosition.z);
		creditsScrollRect.localPosition = Vector3.Lerp (creditsScrollRect.localPosition, targ, Time.deltaTime * 10.0f);
		
		//
		if (creditsScrollerIsShowing && scrollThing.activeInHierarchy && !Input.GetMouseButton (0))
		{
			scrollThing.transform.Translate (Vector3.up * 0.7f * Time.deltaTime);
		}
	}
	
	#endregion 
	
	
	#region Switch To Main Menu
	
	// Sets up the main menu scene for ACTION
	// Called from other menu scripts
	public void Setup ()
	{
		// If we've animated the movement, we can activate the buttons n shit
		if (didAnimate)
		{
			scoresButton.gameObject.SetActive (true);
			playButton.gameObject.SetActive (true);
			optionsMasterButton.SetActive (true);
			facebookButton.gameObject.SetActive (true);
			twitterButton.gameObject.SetActive (true);
			
			// Start dropping the clouds
			if (cloudsParentTransform.position.y != 0) 
				StartCoroutine ("DropClouds");
		}
		
		// Make sure to reset the time to day
		isInMainMenu = true;
		audioCont.ResetToDay ();
		timeCont.Reset ();
		timeCont.SetIsChanging (false);
		
		// Turn on the renderers
		foreach (Renderer r in mainMenuRenderers)
			r.enabled = true;
		
		// Begin the actual transition
		StartCoroutine ("TransitionFromScene");
		
		// Switch camera mode
		camCont.SetIsInMainMenuMode (true);
	}
	
	
	// Transitions the main menu scene back into the screen
	// Called from Setup ()
	IEnumerator TransitionFromScene ()
	{
		// Tell the rest of the script that we are in the middle of a transition
		sceneIsTransitioning = true;
		cloudsBounce.StopBounce ();
		
		// While the transform parent is still above the target, drop it
		while (transitionDropParent.position.y < -1)
		{
			transitionDropSpeed += Time.deltaTime * 1.1f;
			transitionDropParent.Translate (Vector3.up * transitionDropSpeed);
			if (currentActiveButton != 3) grassParentTransform.Translate (Vector3.up * transitionDropSpeed);
			
			yield return null;
		}
		
		transitionDropSpeed = 0;
		transitionDropParent.position = Vector3.zero;
		grassParentTransform.position = Vector3.zero;
		mainTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = true;
		superTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = true;
		
		// Finish the transition
		Invoke ("SetSceneIsDoneTransitioning", 0.2f);
	}
	
	
	// Finishes transitioning to the main menu scene
	// Called from TransitionFromScene ()
	void SetSceneIsDoneTransitioning ()
	{
		bnpIsOnScreen = true;
		highlighterRend.enabled = true;
		highlighterCapRend.enabled = true;
		sceneIsTransitioning = false;
	}
	
	
	// Drops the clouds from the top of the screen
	// Called from BeginTransitionToScene (), Setup (), and BeginFadeInStartButton ()
	IEnumerator DropClouds ()
	{
		// While the clouds are still above their target position, we should drop them
		while (cloudsParentTransform.position.y > 0)
		{
			cloudsDropSpeed += Time.deltaTime * 1.5f;
			cloudsParentTransform.Translate (Vector3.down * cloudsDropSpeed);
			
			yield return null;
		}
		
		// Hard set the clouds to their target position to make sure their position is exact
		cloudsParentTransform.position = Vector3.zero;
		cloudsBounce.Bounce ();
	}
	
	
	// Switches the active button to the play button
	// Called from 
	public void SetPlayAsActiveButton ()
	{
		currentActiveButton = 3;
		SetButtonsToInactiveColor ();
		startButtonSprite.color = Color.white;
		highlighterTargetPosition = new Vector3 (highlighter.localPosition.x, startHighlighterYPos, highlighter.localPosition.z);
	}
	
	#endregion
	
	
	#region Switch To Other Scene
	
	// Begins the transition to a different menu scene
	// Called from ButtonTouched (Transform button)
	void BeginTransitionToScene ()
	{
		// Begin the background morphing
		transitionCont.ChangeToScene (currentActiveButton);
		
		//
		titlesAreMoving = false;
		superTitlesAreMoving = false;
		sceneIsTransitioning = true;
		
		// Get the clouds out of the way
		StopCoroutine ("DropClouds");
		cloudsParentTransform.position = Vector3.zero;
		
		// Start the transition
		StartCoroutine ("TransitionToScene");
	}
	
	
	// Transitions to a different menu scene
	// Called from BeginTransitionToScene ()
	IEnumerator TransitionToScene ()
	{
		// Stop the clouds from bouncing anymore
		cloudsBounce.StopBounce ();
		
		// While the transform parent is still above the target, drop it
		while (transitionDropParent.position.y > transitionDropYTarget)
		{
			transitionDropSpeed += Time.deltaTime * 1.5f;
			transitionDropParent.Translate (Vector3.down * transitionDropSpeed);
			if (currentActiveButton != 3) grassParentTransform.Translate (Vector3.down * transitionDropSpeed);
			if (currentActiveButton != 3) cloudsParentTransform.Translate (Vector3.up * transitionDropSpeed);
			
			yield return null;
		}
		
		// Now that the transform parent is out of the way, we can cleanup the scene
		Cleanup ();
		
		// Now we transition to the next scene
		switch (currentActiveButton)
		{
			// Options
			case 1: sceneCont.ChangeScene (5); break;
			// High scores
			case 2: sceneCont.ChangeScene (3); break;
			// Play
			case 3: eventManager.SetRoundBegin (); break;
			// Credits
			case 5: sceneCont.ChangeScene (4); break;
		}
	}
	
	
	// Finishes the main menu scene cleanup
	// Called from TransitionToScene ()
	void Cleanup ()
	{
		// Turn movement speed off
		transitionDropSpeed = 0.0f;
		cloudsDropSpeed = 0.0f;
		
		// Reset bools
		isInMainMenu = false;
		sceneIsTransitioning = false;
		scoresButton.gameObject.SetActive (false);
		playButton.gameObject.SetActive (false);
		facebookButton.gameObject.SetActive (false);
		twitterButton.gameObject.SetActive (false);
		optionsMasterButton.SetActive (false);
		highlighterRend.enabled = false;
		highlighterCapRend.enabled = false;
		bnpIsOnScreen = false;
		
		//
		mainTitleTransform.localPosition = title1ResetPos;
		superTitleTransform.localPosition = title2ResetPos;
		mainTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = false;
		superTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = false;
		
		// Turn all of the main menu renderers off
		foreach (Renderer r in mainMenuRenderers)
			r.enabled = false;
	}
	
	#endregion


	#region Public
	
	//
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
		switch (_currentThemeIndex)
		{
			case 1:
				for (int i = 0; i < christmasOrnaments.Length; i++) { christmasOrnaments [i].SetActive (false); }
			break;
			case 2:
				//for (int i = 0; i < christmasOrnaments.Length; i++) { christmasOrnaments [i].SetActive (false); }
			break;
			case 3:
				//for (int i = 0; i < christmasOrnaments.Length; i++) { christmasOrnaments [i].SetActive (true); }
			break;
		}
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start () 
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Begins the intro (cloud drop) animation
		BeginIntroAnimation ();
	}
	
	
	// Begins the animation that happens at game boot
	// Called from Start ()
	void BeginIntroAnimation ()
	{
		// Move titles to their initial positions
		mainTitleTransform.position = targetTitleStartPos;
		superTitleTransform.position = superTargetTitleStartPos;
		
		// Wait, then begin animation
		Invoke ("BeginMovingTitle", 0.5f);
		Invoke ("BeginMovingSuperTitle", 1.0f);
		Invoke ("BeginFadeInStartButton", 1.8f); 
	}
	
	
	// Activates buttons and begins moving dynamic objects
	// Called from BeginIntroAnimation ()
	void BeginMovingTitle () { titlesAreMoving = true; }
	void BeginMovingSuperTitle () { superTitlesAreMoving = true; }
	void BeginFadeInStartButton () 
	{ 
		mainTitleTransform.localPosition = title1ResetPos;
		superTitleTransform.localPosition = title2ResetPos;
		didAnimate = true; 
		StartCoroutine ("DropClouds");
		scoresButton.gameObject.SetActive (true);
		playButton.gameObject.SetActive (true);
		facebookButton.gameObject.SetActive (true);
		twitterButton.gameObject.SetActive (true);
		optionsMasterButton.SetActive (true);
		highlighterRend.enabled = true;
		highlighterCapRend.enabled = true;
		bnpIsOnScreen = true;
		
		SetButtonsToInactiveColor ();
		startButtonSprite.color = Color.white;
		
		// Starts the title gyration
		mainTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = true;
		superTitleTransform.gameObject.GetComponent <TitleGyrate> ().isGo = true;
	}	
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		// Scripts
		dataCont = GetComponent <DataController> ();
		inputCont = GetComponent <InputController> ();
		sceneCont = GetComponent <SceneController> ();
		audioCont = GetComponent <AudioController> ();
		timeCont = GetComponent <TimeController> ();
		transitionCont = GetComponent <MenuBackgroundTransitionController> ();
		eventManager = GetComponent <EventManager> ();
		camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		
		// Private variables
		mainTitleTransform = GameObject.Find ("SLOTH").transform;
		targetTitlePos = mainTitleTransform.position;
		targetTitleStartPos = new Vector3 (targetTitlePos.x, -5.7f, targetTitlePos.z);
		superTitleTransform = GameObject.Find ("SUPER").transform;
		superTargetTitlePos = superTitleTransform.position;
		superTargetTitleStartPos = new Vector3 (superTargetTitlePos.x, 8.3f, superTargetTitlePos.z);
		highlighterRend = highlighter.renderer;
		highlighterCapRend = GameObject.Find ("MenuStripeCap").renderer;
		cloudsParentTransform = GameObject.Find ("Cloudtops").transform;
		grassParentTransform = GameObject.Find ("*GrassParent").transform;
		cloudsBounce = GameObject.Find ("Cloudtops").GetComponent <BounceObject> ();
		highlighterTargetPosition = new Vector3 (highlighter.localPosition.x, startHighlighterYPos, highlighter.localPosition.z);
		optionsFoldoutOffscreenPosition = optionsFoldout.localPosition.y;
		csrOffscreenPositionY = creditsScrollRect.localPosition.y;
		scrollThing.transform.localPosition = new Vector3 (scrollThing.transform.localPosition.x, -15.81f, scrollThing.transform.localPosition.z);
		title1ResetPos = mainTitleTransform.localPosition;
		title2ResetPos = superTitleTransform.localPosition;
		
		// Sprites
		startButtonSprite = playButton.gameObject.GetComponent <tk2dSprite> ();
		hsButtonSprite = scoresButton.gameObject.GetComponent <tk2dSprite> ();
	}
	
	#endregion
}
