/*
 	Snowflake.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 23, 2014
 	Last Edited:	November 23, 2014
 	
 	Controls an individual snowflake.
*/


using UnityEngine;
using System.Collections;


public class Snowflake : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The maximum rotation speed for this snowflake
		public float maxRotationSpeed = 5.0f;
		// How quickly the flakes can fall
		public Vector2 fallSpeedRange;
		// The range of sizes this snowflake can be
		public Vector2 sizeRange;
		// The values that the flake will despan at
		public float xDespawnValue;
		public float yDespawnValue;
		// The flake sprite
		public GameObject flakeSprite;
		// The face sprite
		public GameObject faceSprite;

		#endregion

		#region Scripts

		// The platform manager
		PlatformManager _platManager;

		#endregion

		#region Private

		// The cached transform of this snowflake
		private Transform _trans;
		// The rotation speed of this snowflake, calculated when it spawns
		private float _rotationSpeed = 0;
		// The fall speed of this snowflake
		private float _fallSpeed = 0;
		// If the snowflake is currently in use
		private bool _isInUse = false;

		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main update loop
	// Called automatically once per frame
	void Update () 
	{
		// If this snowflake isn't in use, we shouldn't update it
		if (!_isInUse)
			return;

		// Check to see if the snowflake should be despawned
		CheckForDespawn ();

		// Move the snowflake across the screen
		MoveFlake ();

		// Rotate the snowflake
		RotateFlake ();
	}
	
	#endregion


	#region Movement

	// Moves the snowflake across the screen
	// Called every frame from Update ()
	void MoveFlake ()
	{
		// Get the move speed from the platform manager
		float moveSpeed = _platManager.GetPlatformMoveSpeed ();

		// Move the flake
		_trans.Translate (Vector3.left * moveSpeed * Time.deltaTime, Space.World);
		_trans.Translate (Vector3.down * _fallSpeed * Time.deltaTime, Space.World);
	}


	// Rotates the snowflake
	// Called every frame from Update ()
	void RotateFlake ()
	{
		_trans.Rotate (Vector3.forward * _rotationSpeed * Time.deltaTime);
	}

	#endregion


	#region Spawning

	// "Spawns" this snowflake
	// Caled from SpawnSnowflake () in SnowflakeManager.cs
	public void SpawnFlake ()
	{
		// This snowflake should be activated
		_isInUse = true;

		// Set the size of this flake
		bool hasFace = false;
		float sc = 1;
		if (Random.Range (0, 10) >= 7) { hasFace = true; sc= Random.Range (sizeRange.x, sizeRange.y); }
		else { sc= Random.Range (sizeRange.x, sizeRange.y / 3); }
		_trans.localScale = new Vector3 (sc, sc, 1);

		// Set a flake sprite
		int spriteNum = Random.Range (0, 5);
		switch (spriteNum)
		{
			case 0: flakeSprite.GetComponent <tk2dSprite> ().SetSprite ("snowflake_1"); break;
			case 1: flakeSprite.GetComponent <tk2dSprite> ().SetSprite ("snowflake_2"); break;
			case 2: flakeSprite.GetComponent <tk2dSprite> ().SetSprite ("snowflake_3"); break;
			case 3: flakeSprite.GetComponent <tk2dSprite> ().SetSprite ("snowflake_4"); break;
			case 4: flakeSprite.GetComponent <tk2dSprite> ().SetSprite ("snowflake_5"); break;
		}

		// If the flake is big enough, we should also give it a face
		if (sc >= 0.7f && hasFace)
		{
			faceSprite.SetActive (true);
			int faceSpriteNum = Random.Range (0, 5);
			switch (faceSpriteNum)
			{
				case 0: faceSprite.GetComponent <tk2dSprite> ().SetSprite ("snoflakeface_1"); break;
				case 1: faceSprite.GetComponent <tk2dSprite> ().SetSprite ("snoflakeface_2"); break;
				case 2: faceSprite.GetComponent <tk2dSprite> ().SetSprite ("snoflakeface_3"); break;
				case 3: faceSprite.GetComponent <tk2dSprite> ().SetSprite ("snoflakeface_4"); break;
				case 4: faceSprite.GetComponent <tk2dSprite> ().SetSprite ("snoflakeface_5"); break;
			}
		}

		// Get a rotation speed
		_rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);
		_fallSpeed = Random.Range (fallSpeedRange.x, fallSpeedRange.y) + Random.Range (-0.5f, 0.5f);
	}


	// Checks to see if the flake has gone off-screen yet
	// Called every frame from Update ()
	void CheckForDespawn ()
	{
		// Get flake position
		Vector3 pos = _trans.position;

		// Check for horizontal despawn
		if (pos.x <= xDespawnValue || pos.y <= yDespawnValue)
			DespawnFlake ();
	}


	// Despawns the snowflake
	// Called from CheckForDespawn ()
	public void DespawnFlake ()
	{
		// 
		faceSprite.SetActive (false);
		_isInUse = false;
	}

	#endregion


	#region Getters

	public bool GetIsInUse () { return _isInUse; }

	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start ()  
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns initial variables
	// Called initially from Start ()
	private void AssignVariables ()
	{
		_platManager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		_trans = transform;
	}
	
	#endregion
}
