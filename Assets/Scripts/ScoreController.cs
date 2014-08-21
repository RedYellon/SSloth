/*
 	ScoreController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	June 18, 2014
 	
 	Controls the score/high scores.
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The score multiplier for each frame
		public float scoreMultiplier = 3.0f;
		// The player's current score
		public float currentScore = 0;
		// The values for the different star types
		public int goldStarValue = 30;
		public int silverStarValue = 10;
		public int bronzeStarValue = 3;
		
		#endregion
	
		#region Scripts
		
		// The player controller
		PlayerController playerCont;
		// The platform manager
		PlatformManager manager;
		// The data controller
		DataController dataCont;
		// The GUI controller
		GuiController guiCont;
		// The leaderboard script
		Leaderboard lb;
	
		#endregion
		
		#region Caches
		
		// The cached text mesh
		private tk2dTextMesh scoreText;
	
		#endregion
	
		#region Private
		
		// If the score should be incrementing
		private bool isIncrementingScore = false;
		// The number of stars currently collected for this run
		private Vector3 starsCollected = Vector3.zero;
		// The star score indicator text
		private tk2dTextMesh starScoreText;
		// The star score indicator transform
		private Transform starScoreTrans;
		//
		private string starIndString;
		// The speed that the star score indicator moves
		private float siMoveSpeed = 15.0f;
		//
		private int currentTotalScore = 0;
	
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically once per frame
	void Update () 
	{ 
		if (isIncrementingScore)
		{
			int intScore = (int) currentScore;
			scoreText.text = intScore.ToString ();
		}
	}
	
	
	// Fixed logic loop
	// Called automatically once per fixed frame
	void FixedUpdate ()
	{
		if (isIncrementingScore) UpdateScore ();
	}
	
	
	// Updates the player's score
	// Called every fixed frame from FixedUpdate ()
	void UpdateScore ()
	{
		currentScore += (Time.deltaTime * scoreMultiplier);
	}
	
	#endregion
	
	
	#region Star Scoring
	
	// Causes the star score indicator above the player to begin blinking
	// Called from StarCollected (int starType)
	void BeginStarIndicatorBlink (int number)
	{
		// Make sure we reset the processes
		StopCoroutine ("StarIndicatorMovement");
		StopCoroutine ("StarIndicatorFade");
		
		// Set the string to be shown
		starIndString = "+" + (number.ToString ());
		starScoreText.text = starIndString;
		starScoreText.color = Color.white;
		starScoreTrans.localPosition = new Vector3 (-0.1153946f, 0, starScoreTrans.localPosition.z);
		siMoveSpeed = 15.0f;
		
		// Begin the movement
		StartCoroutine ("StarIndicatorMovement");
	}
	
	
	// Moves the star value indicator up after a star is collected
	// Called from BeginStarIndicatorBlink (int number)
	IEnumerator StarIndicatorMovement ()
	{
		while (siMoveSpeed > 0)
		{
			starScoreTrans.Translate (Vector3.up * siMoveSpeed * Time.deltaTime, Space.World);
			siMoveSpeed -= Time.deltaTime * 75f;
			
			yield return null;
		}
		
		StartCoroutine ("StarIndicatorFade");
	}
	
	
	// Fades the star indicator
	// Called from BeginStarIndicatorBlink (int number)
	IEnumerator StarIndicatorFade ()
	{
		while (starScoreText.color.a > 0)
		{
			starScoreText.color = Color.Lerp (starScoreText.color, Color.clear, Time.deltaTime * 6f);
			
			yield return null;
		}
	}
	
	#endregion
	
	
	#region End Of Game
	
	// Called when the game has ended
	//
	public void GameEnded ()
	{
		isIncrementingScore = false;
		scoreText.text = "";
	}
	
	
	// Resets the player's score
	//
	public void ResetScore ()
	{
		currentScore = 0;
		currentTotalScore = 0;
		ResetStarsCount ();
	}
	
	
	// Resets the player's star count
	// Called from ResetScore ()
	void ResetStarsCount ()
	{
		if (starsCollected.x > 0 && starsCollected.y > 0 && starsCollected.z > 0)
			lb.GiveAchievement (3);
			
		starsCollected = Vector3.zero;
	}
	
	
	// Adds the score to the saved high score list
	//
	public void AddPlayerScoreToHighScores ()
	{
		// Add the stars to the current score
		int starsAddedScore = ((int) (starsCollected.x * bronzeStarValue)) + ((int) (starsCollected.y * silverStarValue)) + ((int) (starsCollected.z * goldStarValue));
		int totalScore = (int) currentScore + starsAddedScore;
		currentTotalScore = totalScore;
		if (totalScore == 200)
			lb.GiveAchievement (5);
		
		int [] hs = dataCont.GetHighScores ();
		playerCont.SetIsWearingShades (false);
		lb.ReportScore ((int)totalScore);
		dataCont.IncrementTotalScore ((int) totalScore);
		for (int i = 0; i < 10; i++)
		{
			if (totalScore > hs [i])
			{
				List <int> mylist = new List <int> (hs);
				mylist.Insert (i, (int) totalScore);
				hs = mylist.ToArray ();
				dataCont.SaveHighScoreData (hs);
				
				// If the high score was beaten, the sloth gets shades for the next round 8)
				if (i == 0)
				{
					playerCont.SetIsWearingShades (true);
					guiCont.SetMoveGlasses (true);
				}
				
				return;
			}
		}
	}
	
	#endregion
	
	
	#region Getters
	
	public string GetPlayerScore () { return (((int) currentScore).ToString ()); }
	public int GetPlayerScoreInt () { return (int) currentScore; }
	public Vector3 GetStarCount () { return starsCollected; }
	public int GetCurrentTotalScoreWithStars () { return currentTotalScore; }
	
	#endregion


	#region Public
	
	// Begins the scoring for a new round
	//
	public void BeginScoring ()
	{
		isIncrementingScore = true;
		scoreText.renderer.enabled = true;
	}
	
	
	// Adds a collected star to the collection
	// 1 = bronze, 2 = silver, 3 = gold
	public void StarCollected (int starType)
	{
		// Add a star to the running total, depending on the type of star collected
		switch (starType)
		{
			case 1:
				starsCollected = new Vector3 (starsCollected.x + 1, starsCollected.y, starsCollected.z);
				BeginStarIndicatorBlink (bronzeStarValue);
			break;
			case 2:
				starsCollected = new Vector3 (starsCollected.x, starsCollected.y + 1, starsCollected.z);
				BeginStarIndicatorBlink (silverStarValue);
			break;
			case 3:
				starsCollected = new Vector3 (starsCollected.x, starsCollected.y, starsCollected.z + 1);
				BeginStarIndicatorBlink (goldStarValue);
			break;
		}
	}
		
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundBegin += ResetScore;
		EventManager.OnRoundRestart += ResetScore;
		EventManager.OnRoundEnd += AddPlayerScoreToHighScores;
		EventManager.OnRoundEnd += GameEnded;
		EventManager.OnBackToMainMenuFromGame += ResetScore;
		EventManager.OnPlayerLandFirstPlatform += BeginScoring;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundBegin -= ResetScore;
		EventManager.OnRoundRestart -= ResetScore;
		EventManager.OnRoundEnd -= AddPlayerScoreToHighScores;
		EventManager.OnRoundEnd -= GameEnded;
		EventManager.OnBackToMainMenuFromGame -= ResetScore;
		EventManager.OnPlayerLandFirstPlatform -= BeginScoring;
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
		scoreText = gameObject.GetComponent <tk2dTextMesh> ();
		guiCont = GameObject.Find ("&MainController").GetComponent <GuiController> ();
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
		lb = GameObject.Find ("&MainController").GetComponent <Leaderboard> ();
		playerCont = GameObject.Find ("Player").GetComponent <PlayerController> ();
		starScoreTrans = GameObject.Find ("StarAmountText").transform;
		starScoreText = GameObject.Find ("StarAmountText").GetComponent <tk2dTextMesh> ();
	}
	
	#endregion
}
