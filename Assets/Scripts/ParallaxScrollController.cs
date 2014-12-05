/*
 	ParallaxScrollController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		March 28, 2014
 	Last Edited:	November 28, 2014
 	
 	Controls the behavior of any parallax textures.
*/


using UnityEngine;
using System.Collections;


public class ParallaxScrollController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// If the parallax should start on
		public bool startOn = false;
		// The array of grass transforms
		public Transform [] textures;
		// The array of grass ornaments for the normal theme
		public Transform [] grassOrnamentsNormal;
		// The array of grass ornaments for the winter theme
		public Transform [] grassOrnamentsWinter;
		// The speed that the textures move across the screen
		public float moveSpeed = 5.0f;
		// The X position where the grass texture resets itself
		public float resetPosition;
		// The X position where the grass texture respawns
		public float respawnPosition;
		// The range of times that it takes ornaments to spawn
		public Vector2 ornamentSpawnTimeRange;
		// The transform that has the firefly child
		public Transform fireflyParent;
		// The transform that has the butterfly child
		public Transform butterflyParent;
		
		#endregion
		
		#region Scripts
		
		// The platform manager
		PlatformManager manager;
		// The butterfly controllers
		ButterflyBehavior [] _butterflies;
		// The firefly controllers
		FireflyBehavior [] _fireflies;
		// The data controller
		DataController dataCont;
		// The colors controller
		ColorsController _colors;
		
		#endregion

		#region Private
		
		// If the parallax is moving
		private bool isMoving = false;
		// The array of start positions for the textures
		private Vector3 [] startPos;
		//
		private Transform activeOrnament;
		private int activeOrnamentIndex;
		//
		private bool isMovingOrnament = false;
		//
		private bool ornamentCanSpawn = false;
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
		// The currently active array of ornaments
		private Transform [] _ornaments;
		//
		private Vector3 [] _normalBeginningPos;
		private Vector3 [] _winterBeginningPos;
		private Vector3 [] _ornamentBeginningPositions;
		
		#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Awake ()
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
		if (isMoving)
		{
			MoveTextures ();
			CheckForReset ();
			
			//
			if (isMovingOrnament) 
			{
				MoveOrnament ();
				CheckForOrnamentReset ();
			}
		}
	}
	
	
	// Moves the grass across the screen
	//
	void MoveTextures ()
	{
		foreach (Transform t in textures)
		{
			t.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		}
	}
	
	
	//
	//
	void MoveOrnament ()
	{
		activeOrnament.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
	}
	
	
	//
	//
	void CheckForReset ()
	{
		foreach (Transform t in textures)
		{
			if (t.position.x <= resetPosition)
			{
				Reset (t);
			}
		}
	}
	
	
	//
	//
	void CheckForOrnamentReset ()
	{
		if (activeOrnament.position.x <= resetPosition)
		{
			ResetOrnament ();
		}
	}
	
	
	//
	//
	void Reset (Transform t)
	{
		t.position = new Vector3 (respawnPosition, t.position.y, t.position.z);
	}
	
	
	//
	//
	void ResetOrnament ()
	{
		activeOrnament.position = _ornamentBeginningPositions [activeOrnamentIndex];
		isMovingOrnament = false;

		foreach (ButterflyBehavior b in _butterflies) { b.ResetSetActiveCheck (); }
		foreach (FireflyBehavior f in _fireflies) { f.ResetSetActiveCheck (); }
		Invoke ("SpawnOrnament", Random.Range (ornamentSpawnTimeRange.x, ornamentSpawnTimeRange.y));
	}
	
	
	//
	//
	void ResetAll ()
	{
		for (int i = 0; i < textures.Length; i++) textures [i].position = startPos [i];
		for (int i = 0; i < grassOrnamentsNormal.Length; i++) { grassOrnamentsNormal [i].position = _normalBeginningPos [i]; }
		for (int i = 0; i < grassOrnamentsWinter.Length; i++) { grassOrnamentsWinter [i].position = _winterBeginningPos [i]; }
		isMovingOrnament = false;
		
		//
		foreach (ButterflyBehavior b in _butterflies) { b.ResetSetActiveCheck (); }
		foreach (FireflyBehavior f in _fireflies) { f.ResetSetActiveCheck (); }
	}
	
	
	// Spawns a new ornament
	void SpawnOrnament ()
	{
		if (!ornamentCanSpawn) return;
		
		int ornMun = Random.Range (0, _ornaments.Length);
		activeOrnamentIndex = ornMun;
		activeOrnament = _ornaments [ornMun];
		isMovingOrnament = true;

		// If the ornament has a butterfly ir firefly...
		bool hasButterfly = false;
		bool hasFirefly = false;
		if (activeOrnament.childCount > 0) { if (activeOrnament.GetChild (0).GetComponent <ButterflyBehavior> () != null) hasButterfly = true; } 
		if (activeOrnament.childCount > 0) { if (activeOrnament.GetChild (0).GetComponent <FireflyBehavior> () != null) hasFirefly = true; }
		
		//
		if (hasButterfly)
		{
			activeOrnament.GetChild (0).GetComponent <ButterflyBehavior> ().SetIsInMotion (true);
			dataCont.IncrementButterfliesSeen (1);
		}
		else if (hasFirefly)
		{
			FireflyBehavior ff = activeOrnament.GetChild (0).GetComponent <FireflyBehavior> ();
			ff.SetIsInMotion (true);
			if (ff.gameObject.renderer.enabled)
				dataCont.IncrementFirefliesSeen (1);
		}
	}
	
	
	//
	//
	public void ResetAllOrnaments ()
	{
		ResetAll ();
	}
	
	#endregion
	
	
	#region Public
	
	// Activates/Deactivates the moving of the grass
	public void SetForegroundMove (bool b)
	{
		isMoving = b;
		if (b)
		{
			ornamentCanSpawn = true;
			CancelInvoke ("SpawnOrnament");
			Invoke ("SpawnOrnament", Random.Range (ornamentSpawnTimeRange.x, ornamentSpawnTimeRange.y));
			isMovingOrnament = false;
		}
		else
			ornamentCanSpawn = false;
	}
	void StopForegroundMove ()
	{
		isMoving = false;
		ornamentCanSpawn = false;
	}
	
	
	// Makes the foreground visible
	public void SetForegroundVisible (bool b)
	{
		foreach (Transform t in textures)
		{
			if (b) t.renderer.enabled = b;
		}
		if (!b)
		{
			for (int i = 0; i < grassOrnamentsNormal.Length; i++) { grassOrnamentsNormal [i].position = _normalBeginningPos [i]; }
			for (int i = 0; i < grassOrnamentsWinter.Length; i++) { grassOrnamentsWinter [i].position = _winterBeginningPos [i]; }
			isMovingOrnament = false;
			ornamentCanSpawn = false;
			CancelInvoke ("SpawnOrnament");
			ResetAll ();
		}
	}


	// Changes the theme, and thus the grass colors/decorations
	// Called from ThemeController.cs
	public void ChangeCurrentTheme (int index)
	{
		_currentThemeIndex = index;
		Color col = Color.white;
		switch (_currentThemeIndex)
		{
			case 1:
				//
				_ornaments = grassOrnamentsNormal;
				_ornamentBeginningPositions = _normalBeginningPos;
				grassOrnamentsWinter [0].parent.gameObject.SetActive (false);
				grassOrnamentsNormal [0].parent.gameObject.SetActive (true);

				// Change the color of the grass
				col = _colors.grassDayColor;
				for (int i = 0; i < textures.Length; i++)
					textures [i].GetComponent <tk2dSprite> ().color = col;
			break;
			case 2:
				//
				_ornaments = grassOrnamentsWinter;
				_ornamentBeginningPositions = _winterBeginningPos;
				grassOrnamentsNormal [0].parent.gameObject.SetActive (false);
				grassOrnamentsWinter [0].parent.gameObject.SetActive (true);

				// Change the color of the grass
				col = _colors.grassDayColorWinter;
				for (int i = 0; i < textures.Length; i++)
					textures [i].GetComponent <tk2dSprite> ().color = col;
			break;
		}
	}
	
	#endregion
	
	
	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundEnd += ResetAllOrnaments;
		EventManager.OnRoundEnd += StopForegroundMove;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundEnd -= ResetAllOrnaments;
		EventManager.OnRoundEnd -= StopForegroundMove;
	}
	
	#endregion
	
	
	#region Utility
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		_colors = GameObject.Find ("_ColorsController").GetComponent <ColorsController> ();
		if (startOn) isMoving = true;
		startPos = new Vector3 [textures.Length];
		for (int i = 0; i < textures.Length; i++)
			startPos [i] = textures [i].position;
		
		// Assign ornaments
		_normalBeginningPos = new Vector3 [grassOrnamentsNormal.Length];
		_winterBeginningPos = new Vector3 [grassOrnamentsWinter.Length];
		for (int i = 0; i < grassOrnamentsNormal.Length; i++) { _normalBeginningPos [i] = grassOrnamentsNormal [i].position; }
		for (int i = 0; i < grassOrnamentsWinter.Length; i++) { _winterBeginningPos [i] = grassOrnamentsWinter [i].position; }

		//
		_ornaments = grassOrnamentsNormal;
		_ornamentBeginningPositions = _normalBeginningPos;

		//
		_butterflies = FindObjectsOfType (typeof (ButterflyBehavior)) as ButterflyBehavior [];
		_fireflies = FindObjectsOfType (typeof (FireflyBehavior)) as FireflyBehavior [];
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
	}
	
	#endregion
}
