/*
 	PlayerController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 12, 2014
 	Last Edited:	June 8, 2014
 	
 	Controls the player.
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The walk animation speed
		public float walkAnimSpeed = 0.2f;
		// The jump height of the sloth
		public float jumpHeight = 600;
		// The launch height of the sloth
		public float launchHeight = 1400;
		// The jump height of the sloth
		public float doubleJumpHeight = 600;
		// When the player falls below this level, they lose
		public float deathYLevel = -3.74f;
		// How fast the player must be falling to register a "hard" hit
		public float landHardVelocityGate = 28f;
		// How quickly the sloth sprite rotates during a double jump
		public float doubleJumpRotationSpeed = 800;
		//
		public Transform guiTransParent;
		public Vector3 guiLocalPos;
		
		#endregion
		
		#region Scripts
		
		// The input controller
		InputController inputCont;
		// The platform manager
		PlatformManager platformManager;
		// The audio controller
		AudioController audioCont;
		// The game controller
		GameController gameCont;
		// The data controller
		DataController dataCont;
		// The controller that scrolls the grass
		ParallaxScrollController grassCont;
		// The camera controller
		CameraController camCont;
		// The score controller
		ScoreController scoreCont;
		// The item controller
		ItemController itemCont;
		// The leaderboard
		Leaderboard lb;
		// The event controller
		EventManager eventManager;
		
		#endregion
		
		#region Caches
		
		// The cached transform of the player parent
		Transform trans;
		// The cached transform of the sloth sprite
		Transform spriteTrans;
		// The cached rigidbody of the sloth
		Rigidbody rb;
		// The mud splatter particle system
		ParticleSystem mudSplatter;
		// The hard land smoke particle system
		ParticleSystem hardLandSmoke;
		// The double jump effect particle system
		ParticleSystem doubleJumpEffect;
	
		#endregion
		
		#region Stat Collection
		
		int sessionPlayTime = 0;
		int angryPlatsHit = 0;
		int sadPlatsHit = 0;
		int dirtyPlatsHit = 0;
		int timesJumped = 0;
		int timesDoubleJumped = 0;
		int timesLanded = 0;
		
		#endregion
		
		#region Private
		
		//
		tk2dSprite sprite;
		// The beginning position of the sloth
		private Vector3 beginningPosition;
		// Used to determine landing 
		private List <PlatformController> plats = new List <PlatformController> ();
		private List <PlatformController> horizontallyValidPlats = new List <PlatformController> ();
		private List <PlatformController> landPlats = new List <PlatformController> ();
		// The platform the sloth is currently on
		private PlatformController currentOnPlatform;
		// If the player can jump
		private bool canJump = false;
		// If the sloth is grounded
		private bool isGrounded = false;
		// If the sloth is on his way down from a jump and can land on a platform
		private bool canLand = false;
		// If the sloth is in mid-jump and on the way up
		private bool isOnTheWayUp = false;
		// Bool used to switch between frames of walk animation
		private bool walkAnimBool = false;
		// If the sloth is currently playing the walking animation
		private bool isWalkingAnim = false;
		// If the player can double-jumo
		private bool canDoubleJump = false;
		// If the sloth is spinning from a double-jump
		private bool isSlothSpinning = false;
		// If the sloth should be wearing shades!
		private bool isWearingShades = false;
		//
		private bool isLaunchingUp = false;
		// Used to rotate the sloth when the player hsa lost
		private bool isDead = false;
		//
		private bool slothIsBeginningFalling = false;
		//
		private bool isHardLand = false;
		//
		private Transform fallingWithPlat = null;
		private PlatformController fp;
		private float fallingWithPlatHeight;
		//
		private Vector3 startGamePos;
		//
		private bool guiSlothAnimateBool = false;
		//
		private bool didPrevLandOnBouncePlat = false;
		//
		private Vector3 mudSplatterLocalPosition;
		private Vector3 hardLandSmokeLocalPosition;
		private Vector3 doubleJumpEffectLocalPosition;
		//
		private int playTimeAtStart = 0;
	
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main update loop
	// Called automatically once per frame
	void Update () 
	{
		// Update the platform list
		UpdatePlatformList ();
		UpdateHorizontallyValidPlatforms ();
		
		//
		if (fallingWithPlat != null) MatchFallingPlatYPos ();
		
		// Check for user input
		if (canJump) CheckForInput ();
		
		// If the sloth is on a platform, we need to check to see if we have walked off of it yet
		if (isGrounded) { CheckForFall (); }
		
		// If the sloth is on the way up during a jump, we need to know when he's going down
		if (isOnTheWayUp) CheckForGoingDown ();
		
		// If the sloth is going down, we need to check for collisions with a platform
		if (canLand) 
		{
			CheckForPlatformCollision ();
			UpdateCanLandPlatforms ();
		}
		
		// We need to see if we should die 
		if (!isDead) CheckForDeath ();
	}
	
	
	//
	//
	void FixedUpdate ()
	{
		if (isSlothSpinning) SpinSloth ();
	}
	
	
	// Checks every frame for user input
	// Called from Update ()
	void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			if (canJump && isGrounded)
			{
				// If we haven't touched a button...
				if (inputCont.GetTouchRaycastObject () == null || inputCont.GetTouchRaycastObject () == trans)
				{
					// Determine what jump to do
					if (!canDoubleJump)
					{
						// Make the sloth jump
						Jump ();
					}
					else
					{
						// Make the sloth do his second jump
						DoubleJump ();
					}
				}
			}
			else if (canDoubleJump && (inputCont.GetTouchRaycastObject () == null || inputCont.GetTouchRaycastObject () == trans))
				DoubleJump ();
		}
	}
	
	#endregion
	
	
	#region Collision Check
	
	// Checks to see if the sloth should be falling
	//
	void CheckForFall ()
	{
		// If the sloth is off of the current platform, they should fall
		if ((currentOnPlatform.transform.position.x < -currentOnPlatform.platformWidth) || (currentOnPlatform.transform.position.x > currentOnPlatform.platformWidth + 0.5f))
		{
			isGrounded = false;
			platformManager.DeactivateDirtySlowdown ();
			canLand = true;
			rb.isKinematic = false;
			rb.useGravity = true;
			canDoubleJump = true;
			fallingWithPlat = null;
			
			// Stop the walk animation
			CancelInvoke ("AnimateWalking");
			isWalkingAnim = false;
		}
	}
	
	
	// Checks to see if the sloth is on the way down during a jump
	//
	void CheckForGoingDown ()
	{
		// If the Y velocity is negative, then by definition the sloth is going down
		if (rb.velocity.y < 0f)
		{
			canLand = true;
			isOnTheWayUp = false;
			
			if (isLaunchingUp)
			{
				isLaunchingUp = false;
				canDoubleJump = true;
			}
		}
	}
	
	
	// Checks to see if the sloth should "collide" with a platform
	//
	void CheckForPlatformCollision ()
	{	
		// For each platform that is below us...
		foreach (PlatformController p in landPlats)
		{
			if (p == null || p == fp)
				break;
				
			// If we are lower than this platform AND lined up with it...
			if (trans.position.y <= (p.transform.position.y + p.platformHeight))
			{
				Land (p);
			}
		}
	}
	
	
	// Checks to see if the sloth should die
	// 
	void CheckForDeath ()
	{
		if (trans.position.y <= deathYLevel)
			eventManager.SetRoundEnd ();
	}
	
	
	//
	//
	void MatchFallingPlatYPos ()
	{
		trans.position = new Vector3 (trans.position.x, fallingWithPlat.transform.position.y + fallingWithPlatHeight, trans.position.z);
	}
	
	#endregion
	
	
	#region Platform Updates
	
	// Updates the list of all platforms on the screen
	// Called every frame from Update ()
	void UpdatePlatformList ()
	{
		plats = platformManager.GetPlatforms ();
	}
	
	
	//
	//
	void UpdateHorizontallyValidPlatforms ()
	{
		horizontallyValidPlats.Clear ();
		foreach (PlatformController p in plats)
		{
			if ((p.transform.position.x > -p.platformWidth) && (p.transform.position.x < p.platformWidth + 0.5f))
			{
				horizontallyValidPlats.Add (p);
			}
		}
	}
	
	
	//
	//
	void UpdateCanLandPlatforms ()
	{
		landPlats.Clear ();
		foreach (PlatformController p in horizontallyValidPlats)
		{
			if (trans.position.y >= (p.transform.position.y + p.platformHeight) && p != fp)
			{
				landPlats.Add (p);
			}
		}
	}
	
	#endregion
	
	
	#region Movement
	
	// Called when the player lands on a platform
	//
	void Land (PlatformController p)
	{
		// Check to see if we've landed on a sad platform
		bool previousWasFalling = false;
		if (fallingWithPlat != null) previousWasFalling = true;
		fallingWithPlat = null;
		fp = null;
		bool didLandOnSad = false;
		if (p.GetFaceType () == 4)
		{
			fp = p;
			fallingWithPlat = p.transform;
			fallingWithPlatHeight = p.platformHeight;
			didLandOnSad = true;
			sadPlatsHit ++;
		}
		
		//
		timesLanded ++;
		spriteTrans.rotation = Quaternion.Euler (Vector3.zero);
		dataCont.IncrementNumberOfPlatsHit ();
		bool ihl = false; if (isHardLand || rb.velocity.y <= -landHardVelocityGate) ihl = true;
		p.PlayerDidLand (ihl);
		if (!rb.isKinematic) rb.velocity = Vector3.zero;
		rb.useGravity = false;
		rb.isKinematic = true;
		if (!didLandOnSad) canLand = false;
		trans.position = new Vector3 (trans.position.x, (p.transform.position.y + p.platformHeight) + 0.01f, trans.position.z);
		currentOnPlatform = p;
		isGrounded = true;
		canJump = true;
		canDoubleJump = false;
		isSlothSpinning = false;
		
		//
		if (slothIsBeginningFalling)
			eventManager.SetPlayerLandFirstPlatform ();
			
		// If the player landed hard, respond appropriately
		if (ihl)
			HardLand (p);
			
		// If we hit an angry platform, the sloth should go flying
		if (p.GetIsLauncher ())
		{
			//
			if (didPrevLandOnBouncePlat)
			{
				lb.GiveAchievement (2);
				didPrevLandOnBouncePlat = false;
			}
			else
				didPrevLandOnBouncePlat = true;
				
			p.PlayerLaunched ();
			Launch ();
		}
		else
			didPrevLandOnBouncePlat = false;
			
		//
		if (p.GetFaceType () == 3)
		{
			mudSplatter.transform.parent = trans;
			mudSplatter.transform.localPosition = mudSplatterLocalPosition;
			mudSplatter.Play ();
			mudSplatter.transform.parent = p.transform;
			dirtyPlatsHit ++;
		}
		
		//
		if (isLaunchingUp || previousWasFalling)
			return;
		
		// Make the sloth land
		sprite.SetSprite ("slothLand");
		itemCont.SetFrame (9);
		
		// If we aren't walking, let's start the walking animation
		if (!isWalkingAnim)
		{ 
			isWalkingAnim = true;
			InvokeRepeating ("AnimateWalking", walkAnimSpeed, walkAnimSpeed);
		}
		return;
	}
	
	
	// Called when the player lands "hard" on a platform
	// Called from Land (PlatformController p)
	void HardLand (PlatformController p)
	{
		isHardLand = false;
		camCont.Shake ();
		p.SetIsCracked ();
		
		//
		hardLandSmoke.transform.parent = trans;
		hardLandSmoke.transform.localPosition = hardLandSmokeLocalPosition;
		hardLandSmoke.Play ();
		hardLandSmoke.transform.parent = p.transform;
	}
	
	
	// Makes the sloth jump
	// Called from CheckForInput ()
	void Jump ()
	{
		// Play the jump sound effect
		audioCont.PlaySound ("Jump");
		timesJumped ++;
		//doubleJumpEffect.transform.parent = trans;
		///doubleJumpEffect.transform.localPosition = doubleJumpEffectLocalPosition;
		//doubleJumpEffect.Play ();
		//doubleJumpEffect.transform.parent = null;
		
		// The sloth is now in the air
		fallingWithPlat = null;
		fp = null;
		rb.isKinematic = false;
		rb.AddForce (Vector3.up * jumpHeight);
		rb.useGravity = true;
		isGrounded = false;
		isOnTheWayUp = true;
		canDoubleJump = true;
		isHardLand = false;
		platformManager.DeactivateDirtySlowdown ();
		
		// We want to stop the walking animation
		CancelInvoke ("AnimateWalking");
		isWalkingAnim = false;
		
		// Set the sloth to a random jump animation
		int randNum = Random.Range (0, 6);
		SetJumpSprite (randNum);
	}
	
	
	// Makes the player "launch" in the air
	//
	void Launch ()
	{
		// Launch the player
		rb.isKinematic = false;
		fp = null;
		fallingWithPlat = null;
		rb.velocity = Vector3.zero;
		rb.AddForce (Vector3.up * launchHeight);
		rb.useGravity = true;
		isGrounded = false;
		isOnTheWayUp = true;
		isLaunchingUp = true;
		angryPlatsHit ++;
		
		// We want to stop the walking animation
		CancelInvoke ("AnimateWalking");
		isWalkingAnim = false;
		
		// Set the sloth to a random jump animation
		int randNum = Random.Range (0, 6);
		SetJumpSprite (randNum);
	}
	
	
	// Makes the sloth do a double-jump
	//
	void DoubleJump ()
	{
		// Play the jump sound effect
		audioCont.PlaySound ("Double Jump");
		timesDoubleJumped ++;
		
		//doubleJumpEffect.transform.parent = trans;
		//doubleJumpEffect.transform.localPosition = doubleJumpEffectLocalPosition;
		//doubleJumpEffect.Play ();
		//doubleJumpEffect.transform.parent = null;
		
		// 
		if (!isOnTheWayUp)
		{
			isOnTheWayUp = true;
			canLand = false;
		}
		
		// Apply the force
		if (!isLaunchingUp) rb.velocity = Vector3.zero;
		rb.AddForce (Vector3.up * doubleJumpHeight);
		canDoubleJump = false;
		isHardLand = false;
		
		// Set the sprite to a random jump animation
		isSlothSpinning = true;
		int randNum = Random.Range (0, 6);
		
		// Set the sloth to a random jump animation
		SetJumpSprite (randNum);
	}

	#endregion
	
	
	#region Animation
	
	// Switches between walk frames to give the appearance of animation
	// Called from Land (PlatformController p)
	void AnimateWalking ()
	{
		// Set the material to the correct frame
		if (walkAnimBool)
		{
			sprite.SetSprite ("slothRun1");
			itemCont.SetFrame (10);
		}
		else
		{
			sprite.SetSprite ("slothRun2");
			itemCont.SetFrame (11);
		}
			
		// Switch the frame
		walkAnimBool = !walkAnimBool;
	}
	
	
	// Makes the sloth do his idle animation at the end of game screen
	// Called from GameEnded ()
	void GuiSlothAnimate ()
	{
		trans.localPosition = guiLocalPos;
		if (guiSlothAnimateBool)
		{
			sprite.SetSprite ("slothIdle1"); 
			itemCont.SetFrame (1);
		}
		else
		{
			sprite.SetSprite ("slothIdle2"); 
			itemCont.SetFrame (2);
		}
		
		guiSlothAnimateBool = !guiSlothAnimateBool;
	}
	
	
	// Sets the sloth sprite to a jump frame
	//
	void SetJumpSprite (int frameNum)
	{
		switch (frameNum)
		{
			case 0:
				sprite.SetSprite ("slothJump1");
				itemCont.SetFrame (3);
			break;
			case 1:
				sprite.SetSprite ("slothJump2");
				itemCont.SetFrame (4);
			break;
			case 2:
				sprite.SetSprite ("slothJump3");
				itemCont.SetFrame (5);
			break;
			case 3: 
				sprite.SetSprite ("slothJump4");
				itemCont.SetFrame (6);
			break;
			case 4:
				sprite.SetSprite ("slothJump5");
				itemCont.SetFrame (7);
			break;
			case 5:
				sprite.SetSprite ("slothJump6");
				itemCont.SetFrame (8);
			break;
		}
	}
	
	
	// Sets if the player is wearing high-score shades
	//
	public void SetIsWearingShades (bool b)
	{
		isWearingShades = b;
		if (!b) 
			itemCont.SetHeadSpriteActive (false);
	}
	
	
	// Makes the sloth rotate during a double jump
	// Called every fixed frame from FixedUpdate ()
	void SpinSloth ()
	{
		spriteTrans.Rotate (-Vector3.forward * Time.deltaTime * doubleJumpRotationSpeed);
	}
	
	#endregion
	
	
	#region Setup
	
	// Begins a new game
	//
	void GameStarted ()
	{
		// Reset the transform values of the player
		trans.parent = null;
		trans.position = startGamePos;
		spriteTrans.rotation = Quaternion.Euler (Vector3.zero);
		trans.position = beginningPosition;
		playTimeAtStart = (int) Time.time;
		
		// If the player had a high score, apply the shades 8)
		ApplyShadesIfNeeded ();
		
		// Un-duck the music
		audioCont.DuckMusic (false);
		
		// Reset the animation/sprite properties
		CancelInvoke ("GuiSlothAnimate");
		sprite.SetSprite ("slothJump1");
		sprite.renderer.enabled = true;
		itemCont.SetFrame (3);
		
		// Set the variables correctly
		canJump = true;
		isGrounded = false;
		canLand = true;
		isOnTheWayUp = false;
		isDead = false;
		canDoubleJump = false;
		rb.isKinematic = false;
		rb.useGravity = true;
		isHardLand = true;
		
		// Begin the sloth falling again
		slothIsBeginningFalling = true;
		Invoke ("PlayBeginningWhooshSound", 0.2f);
	}
	
	
	// Plays the "whoosh" sound at the beginning of the game after a small amount of time
	// Called from GameStarted ()
	void PlayBeginningWhooshSound ()
	{
		audioCont.PlaySound ("BeginningFall");
	}
	
	
	// Begins the game proper after the player has landed on the first platform
	// Tripped from event PlayerHasLandedOnFirstPlatform
	void PlayerHasLandedOnFirstPlatform ()
	{
		slothIsBeginningFalling = false;
		grassCont.SetForegroundMove (true);
		camCont.SetIsInMainMenuMode (false);
	}
	
	
	// Applies high score shades to the sloth if needed
	// Called from GameStarted () and MoveGlasses () in GuiController.cs
	public void ApplyShadesIfNeeded ()
	{
		if (isWearingShades)
		{
			itemCont.SetHeadSpriteActive (true);
			itemCont.SetHeadSprite ("aviators");
		}
	}
	
	#endregion
	
	
	#region Cleanup
	
	// Ends the current game
	//
	void GameEnded ()
	{
		// Set proper transform values
		spriteTrans.rotation = Quaternion.Euler (Vector3.zero);
		trans.parent = guiTransParent;
		trans.localPosition = guiLocalPos;
		trans.position = new Vector3 (trans.position.x, deathYLevel, trans.position.y);
		SubmitStats ();
		
		// Play the game over sound effect and duck the music
		audioCont.DuckMusic (true);
		audioCont.PlaySound ("GameOver");
		
		// Set the variables to reflect that the game is over
		didPrevLandOnBouncePlat = false;
		isSlothSpinning = false;
		isDead = true;
		canJump = false;
		isGrounded = false;
		isOnTheWayUp = false;
		canLand = false;
		rb.isKinematic = true;
		rb.useGravity = false;
		fallingWithPlat = null;
		fp = null;
		isWalkingAnim = false;
		slothIsBeginningFalling = false;
		
		// Reset animations
		CancelInvoke ("AnimateWalking");
		sprite.SetSprite ("slothJump1");
		itemCont.SetFrame (3);
		InvokeRepeating ("GuiSlothAnimate", 0.0f, 0.4f);
	}
	
	
	// Sends the stats to the data controller
	//
	void SubmitStats ()
	{
		// Calc play time
		sessionPlayTime = (int) Time.time - playTimeAtStart;
		dataCont.AddToSecondsPlayed (sessionPlayTime);
		
		// Other stats
		dataCont.IncrementJumps (timesJumped);
		dataCont.IncrementDoubleJumps (timesDoubleJumped);
		dataCont.IncrementLandings (timesLanded);
		dataCont.IncrementAngryPlatsHit (angryPlatsHit);
		dataCont.IncrementDirtyPlatsHit (dirtyPlatsHit);
		dataCont.IncrementSadPlatsHit (sadPlatsHit);
	}
	
	
	// Resets the per-round stats
	//
	void ResetStats ()
	{
		sessionPlayTime = 0;
		angryPlatsHit = 0;
		sadPlatsHit = 0;
		dirtyPlatsHit = 0;
		timesJumped = 0;
		timesDoubleJumped = 0;
		timesLanded = 0;
	}
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundBegin += GameStarted;
		EventManager.OnRoundRestart += GameStarted;
		EventManager.OnBackToMainMenuFromGame += ResetStats;
		EventManager.OnRoundRestart += ResetStats;
		EventManager.OnRoundEnd += GameEnded;
		EventManager.OnPlayerLandFirstPlatform += PlayerHasLandedOnFirstPlatform;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundBegin -= GameStarted;
		EventManager.OnRoundRestart -= GameStarted;
		EventManager.OnBackToMainMenuFromGame -= ResetStats;
		EventManager.OnRoundRestart -= ResetStats;
		EventManager.OnRoundEnd -= GameEnded;
		EventManager.OnPlayerLandFirstPlatform -= PlayerHasLandedOnFirstPlatform;
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
		// Sprite stuff
		GameObject s = GameObject.Find ("SlothSprite");
		sprite = s.GetComponent <tk2dSprite> ();
		spriteTrans = s.transform;
		itemCont = s.GetComponent <ItemController> ();
		
		// Main controller scripts
		GameObject mc = GameObject.Find ("&MainController");
		inputCont = mc.GetComponent <InputController> ();
		platformManager = mc.GetComponent <PlatformManager> ();
		audioCont = mc.GetComponent <AudioController> ();
		gameCont = mc.GetComponent <GameController> ();
		dataCont = mc.GetComponent <DataController> ();
		lb = mc.GetComponent <Leaderboard> ();
		eventManager = mc.GetComponent <EventManager> ();
		
		// Other local variables
		grassCont = GameObject.Find ("Foreground Grass").GetComponent <ParallaxScrollController> ();
		camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		
		// Local caches
		trans = transform;
		rb = rigidbody;
		beginningPosition = new Vector3 (trans.position.x, trans.position.y + 0.15f, trans.position.z);
		rb.isKinematic = true;
		startGamePos = trans.position;
		mudSplatter = GameObject.Find ("MudSplatterEffect").particleSystem;
		hardLandSmoke = GameObject.Find ("HardLandSmokeEffect").particleSystem;
		doubleJumpEffect = GameObject.Find ("DoubleJumpEffect").particleSystem;
		mudSplatterLocalPosition = mudSplatter.transform.localPosition;
		hardLandSmokeLocalPosition = hardLandSmoke.transform.localPosition;
		doubleJumpEffectLocalPosition = doubleJumpEffect.transform.localPosition;
	}
	
	#endregion
}
