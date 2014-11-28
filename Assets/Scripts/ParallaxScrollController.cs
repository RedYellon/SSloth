/*
 	ParallaxScrollController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		March 28, 2014
 	Last Edited:	June 7, 2014
 	
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
		// The array of grass ornaments
		public Transform [] grassOrnaments;
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
		// The butterfly controller
		ButterflyBehavior butterfly;
		// The firefly controller
		FireflyBehavior firefly;
		// The data controller
		DataController dataCont;
		// The time controller
		TimeController _timeCont;
		
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
		private Vector3 [] ornamentBeginningPositions;
		//
		private bool ornamentCanSpawn = false;
		// 1 = normal, 2 = winter, 3 = christmas
		private int _currentThemeIndex = 1;
		
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
		activeOrnament.position = ornamentBeginningPositions [activeOrnamentIndex];
		isMovingOrnament = false;
		
		// Reset butterfly/firefly if necessary
		if (activeOrnament == butterflyParent)
		{
			butterfly.ResetSetActiveCheck ();
		}
		else if (activeOrnament == fireflyParent)
		{
			firefly.ResetSetActiveCheck ();
		}
		
		Invoke ("SpawnOrnament", Random.Range (ornamentSpawnTimeRange.x, ornamentSpawnTimeRange.y));
	}
	
	
	//
	//
	void ResetAll ()
	{
		for (int i = 0; i < textures.Length; i++)
		{
			textures [i].position = startPos [i];
		}
		for (int i = 0; i < grassOrnaments.Length; i++) { grassOrnaments [i].position = ornamentBeginningPositions [i]; }
		isMovingOrnament = false;
		
		//
		butterfly.ResetSetActiveCheck ();
		firefly.ResetSetActiveCheck ();
	}
	
	
	//
	void SpawnOrnament ()
	{
		if (!ornamentCanSpawn) return;
		
		int ornMun = Random.Range (0, grassOrnaments.Length);
		activeOrnamentIndex = ornMun;
		activeOrnament = grassOrnaments [ornMun];
		isMovingOrnament = true;
		
		//
		if (activeOrnament == butterflyParent)
		{
			butterfly.SetIsInMotion (true);
			dataCont.IncrementButterfliesSeen (1);
		}
		else if (activeOrnament == fireflyParent)
		{
			firefly.SetIsInMotion (true);
			if (firefly.gameObject.renderer.enabled)
				dataCont.IncrementFirefliesSeen (1);
		}
	}
	
	
	//
	//
	public void ResetAllOrnaments ()
	{
		ResetAll ();
		//for (int i = 0; i < grassOrnaments.Length; i++) { grassOrnaments [i].position = ornamentBeginningPositions [i]; }
	}
	
	#endregion
	
	
	#region Public
	
	// Activates/Deactivates the moving of the grass
	//
	public void SetForegroundMove (bool b)
	{
		isMoving = b;
		if (b)
		{
			ornamentCanSpawn = true;
			CancelInvoke ("SpawnOrnament");
			Invoke ("SpawnOrnament", Random.Range (ornamentSpawnTimeRange.x, ornamentSpawnTimeRange.y));
			//ResetAllOrnaments ();
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
	
	
	//
	//
	public void SetForegroundVisible (bool b)
	{
		foreach (Transform t in textures)
		{
			if (b) t.renderer.enabled = b;
		}
		if (!b)
		{
			for (int i = 0; i < grassOrnaments.Length; i++) { grassOrnaments [i].position = ornamentBeginningPositions [i]; }
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
				GameObject.Find ("GrassOrnament1").GetComponent <tk2dSprite> ().enabled = true;
				GameObject.Find ("GrassOrnament3").GetComponent <tk2dSprite> ().SetSprite ("weed");
				GameObject.Find ("GrassOrnament4").GetComponent <tk2dSprite> ().SetSprite ("rock");
				GameObject.Find ("Butterfly").GetComponent <tk2dSprite> ().enabled = true;
				GameObject.Find ("GrassOrnament5").GetComponent <tk2dSprite> ().SetSprite ("rock_1_fixed");

				// Change the color of the grass
				col = _timeCont.grassDayColor;
				for (int i = 0; i < textures.Length; i++)
					textures [i].GetComponent <tk2dSprite> ().color = col;
			break;
			case 2:
				GameObject.Find ("GrassOrnament1").GetComponent <tk2dSprite> ().enabled = false;
				GameObject.Find ("GrassOrnament3").GetComponent <tk2dSprite> ().SetSprite ("snow_weed");
				GameObject.Find ("GrassOrnament4").GetComponent <tk2dSprite> ().SetSprite ("Snow_rock");
				GameObject.Find ("Butterfly").GetComponent <tk2dSprite> ().enabled = false;
				GameObject.Find ("GrassOrnament5").GetComponent <tk2dSprite> ().SetSprite ("snow_weed");

				// Change the color of the grass
				col = _timeCont.grassDayColorWinter;
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
		_timeCont = GameObject.Find ("&MainController").GetComponent <TimeController> ();
		if (startOn) isMoving = true;
		startPos = new Vector3 [textures.Length];
		for (int i = 0; i < textures.Length; i++)
			startPos [i] = textures [i].position;
		
		// Assign ornaments
		ornamentBeginningPositions = new Vector3 [grassOrnaments.Length];
		for (int i = 0; i < grassOrnaments.Length; i++)
		{
			ornamentBeginningPositions [i] = grassOrnaments [i].position;
		}
		butterfly = GameObject.Find ("Butterfly").GetComponent <ButterflyBehavior> ();
		firefly = GameObject.Find ("Firefly").GetComponent <FireflyBehavior> ();
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
	}
	
	#endregion
}
