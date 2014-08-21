/*
 	BalloonController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		June 8, 2014
 	Last Edited:	August 13, 2014
 	
 	Controls the movements of the balloons that float 
 	across the sky periodically.
*/


using UnityEngine;
using System.Collections;


public class BalloonController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The x positions where balloons despawn and respawn, respectively
		public Vector2 despawnAndRespawnGates;
		// The possible range of Y positions for balloons
		public Vector2 minAndMaxYSpawnPosGates;
		// The speed that the textures move across the screen
		public float moveSpeed = 0.17f;
		// The range of seconds to wait between spawning a new balloon
		public Vector2 spawnWaitTimeRange;
		// The renderers for the regular balloon
		public Renderer [] regularBalloonRends;
		// The renderers for the sloth balloon
		public Renderer [] slothBalloonRends;
		// The renderers for the manned balloon
		public Renderer [] mannedBalloonRends;
	
		#endregion
		
		#region Scripts
		
		// The platform manager
		PlatformManager manager;
		
		#endregion
		
		#region References
		
		
		
		#endregion
		
		#region Private
		
		// The regular balloon
		private Transform regularBalloon;
		// The sloth balloon
		private Transform slothBalloon;
		// The manned balloon
		private Transform mannedBalloon;
		// If the ballons are moving
		private bool isMoving = false;
		//
		private float offset;
		private float step;
		
		#endregion
	
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
		}
	}
	
	
	// Fixed logic loop
	// Called automatically every fixed frame
	void FixedUpdate () 
	{
		if (isMoving)
		{
			//																				\\
			// Up & down movement logic taken from user o_oll at:							\\
			// http://answers.unity3d.com/questions/346008/hover-object-up-and-down.html	\\
			//																				\\
			
			// Increment steps
			step += 0.01f;
			if (step > 999999) { step = 1; }
			
			// Float up and down along the y axis
			regularBalloon.position = new Vector3 (regularBalloon.position.x, Mathf.Sin (step) + offset, regularBalloon.position.z);
			slothBalloon.position = new Vector3 (slothBalloon.position.x, Mathf.Sin (step) + offset, slothBalloon.position.z);
			mannedBalloon.position = new Vector3 (mannedBalloon.position.x, Mathf.Sin (step) + offset, mannedBalloon.position.z);
		}
	}
	
	
	// Moves the balloons across the screen
	// Called every frame from Update (), assuming isMoving is true
	void MoveTextures ()
	{
		// Move the balloons
		slothBalloon.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		regularBalloon.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		mannedBalloon.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
	}
	
	
	// Checks to see if the balloons should be reset
	// Called every frame from Update (), assuming isMoving is true
	void CheckForReset ()
	{
		if (regularBalloon.position.x <= despawnAndRespawnGates.x)
		{
			Reset ();
		}
	}
	
	#endregion
	
	
	#region Spawning
	
	// Spawns a balloon (aka makes it start moving)
	// Called from Start ()
	void SpawnBalloon ()
	{
		// If we are already moving, let's get outta here
		if (isMoving)
			return;
		
		// Get a random balloon
		int r = Random.Range (0, 12);
		int b = 1;
		if (r > 10)
			b = 2;
		else if (r > 7)
			b = 3;
			
		// Make that balloon visible
		if (b == 1)
		{
			foreach (Renderer re in regularBalloonRends) { re.enabled = true; }
			offset = regularBalloon.position.y + regularBalloon.localScale.y;
		}
		else if (b == 2)
		{
			foreach (Renderer re in slothBalloonRends) { re.enabled = true; }
			offset = slothBalloon.position.y + slothBalloon.localScale.y;
		}
		else if (b == 3)
		{
			foreach (Renderer re in mannedBalloonRends) { re.enabled = true; }
			offset = mannedBalloon.position.y + mannedBalloon.localScale.y;
		}
		
		// Make sure the balloon is moving!
		isMoving = true;
		
		// We will spawn another ballon a random time from now
		float randTime = Random.Range (spawnWaitTimeRange.x, spawnWaitTimeRange.y);
		randTime += 60;
		Invoke ("SpawnBalloon", randTime);
	}
	
	#endregion
	
	
	#region Resetting
	
	// Resets the ballons after they have passed through the despawn gate
	// Called from CheckForReset ()
	void Reset ()
	{
		// We are no longer moving
		isMoving = false;
		
		// Turn off all renderers
		foreach (Renderer r in regularBalloonRends) { r.enabled = false; }
		foreach (Renderer r in slothBalloonRends) { r.enabled = false; }
		foreach (Renderer r in mannedBalloonRends) { r.enabled = false; }
		
		// Set a random height
		regularBalloon.localPosition = new Vector3 (despawnAndRespawnGates.y, Random.Range (minAndMaxYSpawnPosGates.x, minAndMaxYSpawnPosGates.y), regularBalloon.position.z);
		slothBalloon.localPosition = new Vector3 (despawnAndRespawnGates.y, Random.Range (minAndMaxYSpawnPosGates.x, minAndMaxYSpawnPosGates.y), slothBalloon.position.z);
		mannedBalloon.localPosition = new Vector3 (despawnAndRespawnGates.y, Random.Range (minAndMaxYSpawnPosGates.x, minAndMaxYSpawnPosGates.y), mannedBalloon.position.z);
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Begin spawning balloons
		float randTime = Random.Range (spawnWaitTimeRange.x, spawnWaitTimeRange.y);
		Invoke ("SpawnBalloon", randTime);
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		regularBalloon = GameObject.Find ("RegularBalloon").transform;
		slothBalloon = GameObject.Find ("SlothBalloon").transform;
		mannedBalloon = GameObject.Find ("MannedBalloon").transform;
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
	}
	
	#endregion
}
