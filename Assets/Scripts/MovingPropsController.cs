/*
 	MovingPropsController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		August 13, 2014
 	Last Edited:	August 13, 2014
 	
 	Controls the movements of the foreground props, such
 	as the llama, giraffe, and ostrich.
*/


using UnityEngine;
using System.Collections;


public class MovingPropsController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The x positions where props despawn and respawn, respectively
		public Vector2 despawnAndRespawnGates;
		// The speed that the props move across the screen, relative to the platform move speed
		public float moveSpeed = 0.17f;
		// The range of seconds to wait between spawning a new balloon
		public Vector2 spawnWaitTimeRange;
		// The renderers
		public Renderer [] propRenderers;
		
		#endregion
		
		#region Scripts
		
		// The platform manager
		PlatformManager manager;
		
		#endregion
		
		#region Private
		
		// 
		private Transform llama;
		// 
		private Transform giraffe;
		// 
		private Transform ostrich;
		// If the props are moving
		private bool isMoving = false;
		//
		private float offset;
		private float step;
		//
		private float ls;
		private float gs;
		private float os;
		
		
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
			llama.position = new Vector3 (llama.position.x, Mathf.Sin (step) + offset, llama.position.z);
			giraffe.position = new Vector3 (giraffe.position.x, Mathf.Sin (step) + offset, giraffe.position.z);
			ostrich.position = new Vector3 (ostrich.position.x, Mathf.Sin (step) + offset, ostrich.position.z);
		}
	}
	
	
	// Moves the balloons across the screen
	// Called every frame from Update (), assuming isMoving is true
	void MoveTextures ()
	{
		// Move the balloons
		llama.Translate (Vector3.right * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		giraffe.Translate (Vector3.right * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		ostrich.Translate (Vector3.right * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
	}
	
	
	// Checks to see if the props should be reset
	// Called every frame from Update (), assuming isMoving is true
	void CheckForReset ()
	{
		if (llama.position.x >= despawnAndRespawnGates.x)
		{
			Reset ();
		}
	}
	
	#endregion
	
	
	#region Spawning
	
	// Spawns a prop (aka makes it start moving)
	// Called from Start ()
	void SpawnProp ()
	{
		// If we are already moving, let's get outta here
		if (isMoving)
			return;
		
		// Get a random prop
		int r = Random.Range (0, 9);
		int b = 1;
		if (r > 6)
			b = 2;
		else if (r > 3)
			b = 3;
		
		// Make that prop visible
		if (b == 1)
		{
			propRenderers [0].enabled = true;
			offset = llama.position.y + 0.5f;
		}
		else if (b == 2)
		{
			propRenderers [1].enabled = true;
			offset = giraffe.position.y + giraffe.localScale.y;
		}
		else if (b == 3)
		{
			propRenderers [2].enabled = true;
			offset = ostrich.position.y + ostrich.localScale.y;
		}
		
		// Make sure the prop is moving!
		isMoving = true;
		
		// We will spawn another prop a random time from now
		float randTime = Random.Range (spawnWaitTimeRange.x, spawnWaitTimeRange.y);
		randTime += 60;
		Invoke ("SpawnProp", randTime);
	}
	
	#endregion
	
	
	#region Resetting
	
	// Resets the props after they have passed through the despawn gate
	// Called from CheckForReset ()
	void Reset ()
	{
		// We are no longer moving
		isMoving = false;
		
		// Turn off all renderers
		foreach (Renderer r in propRenderers) { r.enabled = false; }
		
		// Set a random height
		llama.localPosition = new Vector3 (despawnAndRespawnGates.y, ls, llama.position.z);
		giraffe.localPosition = new Vector3 (despawnAndRespawnGates.y, gs, giraffe.position.z);
		ostrich.localPosition = new Vector3 (despawnAndRespawnGates.y, os, ostrich.position.z);
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Begin spawning props
		float randTime = Random.Range (spawnWaitTimeRange.x, spawnWaitTimeRange.y);
		Invoke ("SpawnProp", randTime);
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		llama = GameObject.Find ("LlamaSprite").transform;
		giraffe = GameObject.Find ("GiraffeSprite").transform;
		ostrich = GameObject.Find ("OstrichSprite").transform;
		ls = llama.position.y;
		os = ostrich.position.y;
		gs = giraffe.position.y;
	}
	
	#endregion
}
