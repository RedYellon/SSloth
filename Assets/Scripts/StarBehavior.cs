/*
 	StarBehavior.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		March 14, 2014
 	Last Edited:	June 18, 2014
 	
 	Controls the behavior of this star.
*/


using UnityEngine;
using System.Collections;


public class StarBehavior : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// 1 = bronze, 2 = silver, 3 = gold
		public int starType = 3;
		// How close the player must get to collect this star
		public float collectDistance = 0.65f;
		// How quickly this star rotates
		public float defaultRotationSpeed = 2.0f;
		// How quickly the star rotates after it has been collected by the player
		public float collectedRotationSpeed = 5.0f;
		// How far the star bounces up when collected
		public float moveUpDistance = 1.0f;
		// The X position that this star despawns at
		public float despawnXPos = -20f;
		// The x offset once the star is collected
		public float xOffsetOnceCollected = -0.2f;
		
		#endregion
		
		#region Scripts
		
		// The score controller
		ScoreController scoreCont;
		// The audio controller
		AudioController audioCont;
		// The platform manager
		PlatformManager manager;
		
		#endregion
		
		#region References
		
		// The transform of this star
		private Transform trans;
		// The transform of the player
		private Transform playerTrans;
		//
		private Transform parentTrans;
		
		#endregion
		
		#region Private
		
		// Whether or not the star is moving across the screen
		private bool isMoving = true;
		// The current rotation speed of this star
		private float currentRotationSpeed = 2.0f;
		// The move speed of the star after it has been collected
		private float collectedMoveSpeed = 20.0f;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		// If the star should be moving, then move it and check for collection/despawn
		if (isMoving) 
		{
			Move ();
			CheckForDespawn ();
			CheckForPlayerCollision ();
		}
		
		// The star should alwyas be rotating during it's life
		Rot ();
	}
	
	#endregion
	
	
	#region Movement
	
	// Sets whether this star should be moving across the screen
	// Called from GameEnded () in PlatformManager.cs
	public void SetIsMoving (bool b)
	{
		isMoving = b;
	}
	
	
	// Moves the star across the screen
	// Called every frame from Update ()
	void Move ()
	{
		trans.Translate (Vector3.left * manager.GetPlatformMoveSpeed () * Time.deltaTime, Space.World);
	}
	
	
	// Continuously rotates the star
	// Called every frame from Update ()
	void Rot ()
	{
		trans.Rotate (0, 0, currentRotationSpeed);
	}
	
	
	// Moves the star up and then down into the player after it has been collected
	// Called from Collected ()
	IEnumerator CollectedMovement ()
	{
		while (trans.localPosition.y > 0)
		{
			trans.Translate (Vector3.up * collectedMoveSpeed * Time.deltaTime, Space.World);
			collectedMoveSpeed -= Time.deltaTime * 100f;
			
			yield return null;
		}
		
		// Destroy the star
		End ();
	}
	
	#endregion
	
	
	#region Proximity Check
	
	// Determines if the player is close enough to this star to pick it up (no physics needed!)
	// Called every frame from Update ()
	void CheckForPlayerCollision ()
	{
		if (Vector3.Distance (trans.position, playerTrans.position) <= collectDistance)
			Collected ();
	}
	
	
	// Determines if this star is far enough to the left to despawn
	// Called every frame from Update ()
	void CheckForDespawn ()
	{
		if (trans.position.x < despawnXPos)
			Despawn ();
	}
	
	#endregion
	
	
	#region End Of Life
	
	// Called when the star has been collected by the player
	// Called from CheckForPlayerCollision ()
	void Collected ()
	{
		// Play the correct Audio sound
		switch (starType)
		{
			case 1: audioCont.PlaySound ("StarBronze"); break;
			case 2: audioCont.PlaySound ("StarSilver");	break;
			case 3: audioCont.PlaySound ("StarGold"); break;
		}
		
		// Increase the rotation speed and begin the collected animation
		currentRotationSpeed = collectedRotationSpeed;
		trans.parent = parentTrans;
		trans.localPosition = new Vector3 (xOffsetOnceCollected, 0.1f, 0.1f);
		isMoving = false;
		
		// Begin collected animation coroutine
		StartCoroutine ("CollectedMovement");
	}
	
	
	// Destroys the star after it has been collected by the player
	// Called from CollectedMovement ()
	void End ()
	{
		audioCont.PlaySound ("StarGet");
		scoreCont.StarCollected (starType);
		manager.RemoveStarFromList (this);
		Destroy (gameObject);
	}
	
	
	// Despawns this star after it has passed beyond the depsawn point
	// Called from CheckForDespawn ()
	public void Despawn ()
	{
		manager.RemoveStarFromList (this);
		Destroy (gameObject);
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		scoreCont = GameObject.Find ("Score").GetComponent <ScoreController> ();
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		
		trans = transform;
		currentRotationSpeed = defaultRotationSpeed;
		playerTrans = GameObject.Find ("SlothSprite").transform;
		parentTrans = GameObject.Find ("Player").transform;
	}
	
	#endregion
}
