/*
 	PlatformManager.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	November 22, 2014
 	
 	Coordinates the platforms.
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlatformManager : MonoBehaviour 
{
	#region Variables
	
	#region Public
	
	// The array of platform colors, in order
	public Color [] platformColors;
	// The color of platforms with stars on them
	public Color starPlatformColor;
	// The two X positions of the beginning platforms
	public float beginningPlatXPos1 = 1.903751f;
	public float beginningPlatXPos2 = 7.149028f;
	// The size of the platform object pool
	public int platformPoolSize = 8;
	// The platform gameObject
	public GameObject platformPrefab;
	// The star prefabs
	public GameObject goldStarPrefab;
	public GameObject silverStarPrefab;
	public GameObject bronzeStarPrefab;
	// Platforms will despawn to the left of this point
	public float platformDespawnXPos;
	// The current move speed of the platforms
	public float beginningPlatformMoveSpeed = 5.0f;
	//
	public Vector2 platformRegSpawnRateRange = new Vector2 (300, 450);
	public Vector2 platformSpecSpawnRateRange = new Vector2 (450, 2000);
	// How quickly the platform move speed increases
	public float platformMoveSpeedIncreaseRate = 0.0001f;
	// The chance (%) of spawning a special platform
	public int specialPlatformSpawnChance = 30;
	// The chance (%) of spawning a star platform
	public int starPlatformSpawnChance = 33;
	
	#region Spawn
	
	// The X position that platforms spawn at
	public float spawnXPos = 11.4f;
	// The maximum and minimum Y values that platforms can spawn between
	public Vector2 maxSpawnRange = Vector2.zero;
	
	#endregion
	
	#endregion
	
	#region Private
	
	// The list of platforms on the screen
	public PlatformController [] platforms; 
	public List <StarBehavior> stars = new List <StarBehavior> ();
	// The position that platforms wait at before they are deployed
	private Vector3 platformStagingPosition;
	// The current color index for the next spawned platform
	private int platColorIndex = 0;
	// If the platforms are increasing in speed
	private bool isIncreasingSpeed = false;
	// The previous platform that was spawned
	private Vector3 previousSpawnPos = Vector3.zero;
	// If platforms can be spawned
	private bool canSpawnPlatforms = false;
	// If the game has started
	private bool gameIsStarted = false;
	
	// The current move speed of the platforms
	private float platformMoveSpeed = 0.0f;
	// The current number of ticks to spawn a regular platform
	private int currentTicks = 0;
	// The current number of ticks to spawn a random platform
	private int currentRandomTicks = 0;
	// The number of ticks needed to pass for the next regular platform to spawn
	private int ticksToNextSpawn = 50;
	// The number of ticks needed to pass for the next random platform to spawn
	private int ticksToNextRandomSpawn = 200;
	// The current number of ticks to decrement the regular platform spawn time
	private int decrementTicks = 0;
	// The maximum number of ticks between individual platform spawns
	private float spawnRateRegDifference = 150;
	private float spawnRateSpecDifference = 1500;
	// The platform holder gameobject
	private Transform platformHolder;
	// The temporary platform move speed percentage (for when we only want to temporarily change move speed)
	private float temporaryMoveSpeedVariable = 1;
	//
	private Vector3 platParentOffscreenPos = new Vector3 (0, -9, 0);
	//
	private Vector3 platParentTargPos;
	//
	private bool goPlatParentMove = false;
	// If the platforms should have the chance of icicles
	private bool _platformsAreIcy = false;
	
	#endregion
	
	#endregion
	
	
	#region Spawning
	
	// Spawns a platform
	//
	void SpawnPlatform ()
	{	
		// If we can't spawn a platform for whatever reason, we should exit
		if (!canSpawnPlatforms)
			return;
		
		// Declare the Y position of the soon to be spawned platform
		float ySpawnPos = Random.Range (maxSpawnRange.x, maxSpawnRange.y);
		
		// Adjust to help minimize impossible jumps
		if (Mathf.Abs (ySpawnPos - previousSpawnPos.y) > 4.8f)
		{
			if (ySpawnPos < previousSpawnPos.y)
				ySpawnPos += 1.5f;
			else
				ySpawnPos -= 1.5f;
		}
		
		// We need to make sure we haven't spawned a platform on top of another one
		if (previousSpawnPos != Vector3.zero)
			if (Mathf.Abs (ySpawnPos - previousSpawnPos.y) < 1.0f)
		{
			if (ySpawnPos > previousSpawnPos.y)
				ySpawnPos = (Mathf.Min (ySpawnPos + 1.5f, maxSpawnRange.y));
			else
				ySpawnPos = (Mathf.Max (ySpawnPos - 1.5f, maxSpawnRange.x));
		}
		
		// Cycle through and spawn the next available platform
		Vector3 spawnPos = new Vector3 (spawnXPos, ySpawnPos, Random.Range (0.3f, 0f));
		PlatformController p = null;
		for (int i = 0; i < platforms.Length; i++)
		{
			// If this platform is not currently in use...
			if (!platforms [i].GetIsCurrentlyInUse ())
			{
				p = platforms [i];
				SetPlatformColor (p);
				p.transform.localPosition = spawnPos;
				p.ActivatePlatform ();
				p.StartMovingPlatform ();
				p.gameObject.GetComponent<Renderer>().enabled = true;
				break;
			}
		}
		if (p == null) 
			return;
		
		// If chance hit, spawn a special platform
		if (Random.Range (0, 100) <= specialPlatformSpawnChance)
		{
			// We need to get a special platform to spawn
			float r = Random.Range (0, 10);
			
			// If we should spawn a star platform...
			if (r <= 3)
			{
				GameObject st = Instantiate (goldStarPrefab, new Vector3 (spawnPos.x - 0.1f, spawnPos.y + 1.3f, 0.0f), Quaternion.identity) as GameObject;
				stars.Add (st.gameObject.GetComponent <StarBehavior> ());
				p.SetFaceType (1);
			}
			// If we should spawn an angry platform...
			else if (r <= 6)
			{
				p.SetFaceType (2);
			}
			// If we should spawn a sad platform...
			else if (r <= 8)
			{
				p.SetFaceType (4);
			}
			// If we should spawn a dirty platform...
			else if (r <= 10)
			{
				p.SetFaceType (3);
			}
			
			// Turn the renderer for the base platform off
			p.gameObject.GetComponent <Renderer> ().enabled = false;
		}
		// Now we need to check for random star spawn
		else if (Random.Range (0, 100) <= starPlatformSpawnChance)
		{
			// Silver stars
			if (Random.Range (0, 10) <= 3)
			{
				GameObject st = Instantiate (silverStarPrefab, new Vector3 (spawnPos.x - 0.1f, spawnPos.y + 1.3f, 0.0f), Quaternion.identity) as GameObject;
				stars.Add (st.gameObject.GetComponent <StarBehavior> ());
			}
			// Bronze stars
			else
			{
				GameObject st = Instantiate (bronzeStarPrefab, new Vector3 (spawnPos.x - 0.1f, spawnPos.y + 1.3f, 0.0f), Quaternion.identity) as GameObject;
				stars.Add (st.gameObject.GetComponent <StarBehavior> ());
			}
		}
		
		// If we can, we should check for a random icicle spawn
		if (_platformsAreIcy)
		{
			int r = Random.Range (0, 10);
			if (r <= 3)
				p.icicleSprite.SetActive (true);
		}
		
		// Set the previous platform position
		previousSpawnPos = spawnPos;
	}
	
	
	// Sets the initial platforms in their correct positions
	// Called from GameStarted ()
	void SpawnBeginningPlatforms ()
	{
		platforms [0].transform.localPosition = new Vector3 (beginningPlatXPos1, -0.871768f, 0f);
		platforms [1].transform.localPosition = new Vector3 (beginningPlatXPos2, -0.871768f, 0f);
		platforms [0].ActivatePlatform ();
		platforms [1].ActivatePlatform ();
		platforms [0].gameObject.GetComponent<Renderer>().enabled = true;
		platforms [1].gameObject.GetComponent<Renderer>().enabled = true;
		SetPlatformColor (platforms [0]);
		SetPlatformColor (platforms [1]);
	}
	
	
	// Ends all platform spawning processes
	// Called from GameEnded () and Cleanup ()
	void EndSpawningPlatforms ()
	{
		gameIsStarted = false;
		canSpawnPlatforms = false;
	}
	
	
	// Sets the color of the platform
	// Called from SpawnPlatform ()
	void SetPlatformColor (PlatformController p)
	{
		// Reset the color index if we have gotten to the end of the array
		if (platColorIndex >= platformColors.Length)
		{
			platColorIndex = 0;
		}
		
		// Set the color of the platform
		p.gameObject.GetComponent <tk2dSprite> ().color = platformColors [platColorIndex];
		//p.transform.GetChild (0).GetComponent <tk2dSprite> ().color = platformColors [platColorIndex];
		p.SetDefaultColor (platformColors [platColorIndex]);
		
		// Step to next color in array
		platColorIndex++;
	}
	
	#endregion
	
	
	#region Update
	
	//
	//
	void Update ()
	{
		if (goPlatParentMove)
		{
			platformHolder.position = Vector3.Lerp (platformHolder.position, platParentTargPos, Time.deltaTime * 10.0f);
		}
	}
	
	
	// Used for framerate-independent stuff
	// Called automatically every fixed frame
	void FixedUpdate ()
	{
		// If we can spawn platforms, we need to start the tick cycle
		if (canSpawnPlatforms)
		{
			currentTicks++;
			decrementTicks++;
			currentRandomTicks++;
			
			// If we've waited long enough, we should spawn a regular platform
			if (currentTicks >= ticksToNextSpawn)
			{
				SpawnPlatform ();
				ticksToNextSpawn = Random.Range ((int) (platformRegSpawnRateRange.x / GetPlatformMoveSpeed ()), (int) ((platformRegSpawnRateRange.x / GetPlatformMoveSpeed ()) + spawnRateRegDifference));
				currentTicks = 0;
			}
			
			// If we've waited long enough, we should spawn a random platform
			if (currentRandomTicks >= ticksToNextRandomSpawn)
			{
				SpawnPlatform ();
				ticksToNextRandomSpawn = Random.Range ((int) (platformSpecSpawnRateRange.x / GetPlatformMoveSpeed ()), (int) ((platformSpecSpawnRateRange.x / GetPlatformMoveSpeed ()) + spawnRateSpecDifference));
				currentRandomTicks = 0;
			}
		}
		
		// If we are increasing the speed of the platforms
		if (isIncreasingSpeed) 
			IncreasePlatformSpeed ();
	}
	
	
	// Increases the movement speed of the platforms over time
	// Called every frame from Update ()
	void IncreasePlatformSpeed ()
	{
		platformMoveSpeed += platformMoveSpeedIncreaseRate;
	}
	
	
	// Cuts the move speed of the platforms
	// Called when a player lands on the dirty platform
	public void CutMoveSpeed ()
	{
		platformMoveSpeed *= 0.95f;
		if (platformMoveSpeed < beginningPlatformMoveSpeed)
			platformMoveSpeed = beginningPlatformMoveSpeed;
	}
	
	#endregion
	
	
	#region Themes
	
	// Sets whether snow is active
	// Called from EventManager.cs
	public void SetSnowIsActive (bool b)
	{
		// Activate the snow sprites on the platforms
		for (int i = 0; i < platforms.Length; i++)
		{
			platforms [i].snowSprite.SetActive (b);
		}
		
		// Ice sprites will be activated randomly at spawn
		_platformsAreIcy = b;
	}
	
	#endregion
	
	
	#region Public
	
	// Removes the given star from the master star list
	// 
	public void RemoveStarFromList (StarBehavior s) { stars.Remove (s); }
	public void ActivateDirtySlowdown () { temporaryMoveSpeedVariable = 0.7f; }
	public void DeactivateDirtySlowdown () { temporaryMoveSpeedVariable = 1; }
	
	
	// Ends the game and cleans up various thingies
	// Called from triggered event OnRoundEnd
	void GameEnded ()
	{
		isIncreasingSpeed = false;
		platformMoveSpeed = beginningPlatformMoveSpeed;
		temporaryMoveSpeedVariable = 1;
		platformMoveSpeed = 0.0f;
		canSpawnPlatforms = false;
		ResetTickCounts ();
		
		// Stop platforms from moving
		foreach (PlatformController p in platforms)
			p.SetIsMoving (false);
		
		// Stop stars from moving
		foreach (StarBehavior st in stars)
			st.SetIsMoving (false);
		
		// Stop the platform spawning 
		EndSpawningPlatforms ();
	}
	
	
	// Resets the spawn ticks for a new round
	// Called from GameEnded ()
	void ResetTickCounts ()
	{
		currentTicks = 0;
		decrementTicks = 0;
		ticksToNextSpawn = 50;
		currentRandomTicks = 0;
	}
	
	
	// Called as soon as a new game is started
	// Called on triggered events OnRoundBegin and OnRoundStarted
	void GameStarted ()
	{
		// If this method is accidentally called twice, escape to prevent bugs
		if (gameIsStarted) return;
		else gameIsStarted = true;
		
		// Clear the screen
		ResetScreenObjects ();
		
		// Spawn the beginning platforms
		SpawnBeginningPlatforms ();
	}
	
	
	// Called when the user exits the game scene
	// Called on triggered event OnBackToMainMenuFromGame
	void Cleanup ()
	{
		// Clear the screen
		ResetScreenObjects ();
		
		// Stop the platform spawning 
		EndSpawningPlatforms ();
	}
	
	#endregion
	
	
	#region Getters
	
	// Returns a list of all the current platforms
	//
	public List <PlatformController> GetPlatforms ()
	{
		List <PlatformController> ps = new List <PlatformController> ();
		for (int i = 0; i < platforms.Length; i++)
		{
			if (platforms [i].GetIsCurrentlyInUse ())
				ps.Add (platforms [i]);
		}
		return ps;
	}
	
	public float GetPlatformMoveSpeed () { return (platformMoveSpeed * temporaryMoveSpeedVariable); }
	
	#endregion
	
	
	// Begins moving the platforms
	// Called on triggered event OnPlayerLandFirstPlatform
	void BeginMovingPlatforms ()
	{
		// Reset all platforms
		platforms [0].StartMovingPlatform ();
		platforms [1].StartMovingPlatform ();
		
		// Reset spawning variables
		platformMoveSpeed = beginningPlatformMoveSpeed;
		temporaryMoveSpeedVariable = 1;
		isIncreasingSpeed = true;
		canSpawnPlatforms = true;
	}
	
	
	#region Utility
	
	// Resets the platforms and stars
	// Called from GameStarted () and Cleanup ()
	void ResetScreenObjects ()
	{
		// Reset all platforms
		for (int i = 0; i < platforms.Length; i++)
			platforms [i].DeactivatePlatform ();
		
		// Remove all stars
		for (int i = 0; i < stars.Count; i++)
			Destroy (stars [i].gameObject);
		
		// Clear any lingering stars
		stars.Clear ();
	}	
	
	
	//
	//
	void RaisePlatforms ()
	{
		platParentTargPos = Vector3.zero;
	}
	
	
	//
	//
	void LowerPlatforms ()
	{
		platParentTargPos = platParentOffscreenPos;
	}
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundBegin += GameStarted;
		EventManager.OnRoundBegin += RaisePlatforms;
		EventManager.OnRoundRestart += GameStarted;
		EventManager.OnRoundRestart += RaisePlatforms;
		EventManager.OnRoundEnd += GameEnded;
		EventManager.OnRoundEnd += LowerPlatforms;
		EventManager.OnBackToMainMenuFromGame += Cleanup;
		EventManager.OnPlayerLandFirstPlatform += BeginMovingPlatforms;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundBegin -= GameStarted;
		EventManager.OnRoundRestart -= GameStarted;
		EventManager.OnRoundBegin -= RaisePlatforms;
		EventManager.OnRoundRestart -= RaisePlatforms;
		EventManager.OnRoundEnd -= GameEnded;
		EventManager.OnRoundEnd -= LowerPlatforms;
		EventManager.OnBackToMainMenuFromGame -= Cleanup;
		EventManager.OnPlayerLandFirstPlatform -= BeginMovingPlatforms;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Awake ()  
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Instantiate all of the platforms and pool them for dynamic use
		CreatePlatformPool ();
	}
	
	
	// Creates the pool of platforms from which platforms will be "spawned"
	// Called initially from Awake ()
	void CreatePlatformPool ()
	{
		// Instantiate however many platforms
		platforms = new PlatformController [platformPoolSize];
		for (int i = 0; i < platforms.Length; i++)
		{
			GameObject plat = Instantiate (platformPrefab, platformStagingPosition, Quaternion.identity) as GameObject;
			plat.transform.parent = platformHolder;
			platforms [i] = plat.GetComponent <PlatformController> ();
			goPlatParentMove = true;
		}
		platParentTargPos = platParentOffscreenPos;
	}
	
	
	// Assigns initial variables
	// Called initially from Awake ()
	private void AssignVariables ()
	{
		platColorIndex = Random.Range (0, platformColors.Length);
		platformStagingPosition = new Vector3 (spawnXPos, 0f, 0f);
		platformHolder = GameObject.Find ("*PlatformHolder").transform;
		spawnRateRegDifference = (platformRegSpawnRateRange.y / beginningPlatformMoveSpeed) - (platformRegSpawnRateRange.x / beginningPlatformMoveSpeed);
		spawnRateSpecDifference = (platformSpecSpawnRateRange.y / beginningPlatformMoveSpeed) - (platformSpecSpawnRateRange.x / beginningPlatformMoveSpeed);
	}
	
	#endregion
}
