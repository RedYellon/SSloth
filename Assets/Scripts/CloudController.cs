/*
 	CloudController.cs
 	Michael Stephens
 	
 	Created:		May 8, 2014
 	Last Edited:	May 19, 2014
 	
 	Controls the spawning of clouds.
*/


using UnityEngine;
using System.Collections;


public class CloudController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The x positions where clouds despawn and respawn, respectively
		public Vector2 despawnAndRespawnGates;
		// The possible range of Y positions for clouds
		public Vector2 minAndMaxYSpawnPosGates;
		// If the parallax should start on
		public bool startOn = false;
		// The array of clouds (parent objects)
		public GameObject [] clouds;
		// The cloud sprite transforms
		public Transform [] cloudSpriteTransforms;
		// The speed that the textures move across the screen
		public float moveSpeed = 5.0f;
	
		#endregion
	
		#region Scripts
	
		// The platform manager
		PlatformManager manager;
	
		#endregion
	
		#region Private
	
		// If the parallax is moving
		private bool isMoving = false;
		// The cloud transforms
		private Transform [] cloudTrans;
		
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
	
	
	// Moves the clouds across the screen
	// Called every frame from Update (), assuming isMoving is true
	void MoveTextures ()
	{
		// Move the clouds
		foreach (Transform t in cloudTrans)
		{
			t.Translate (Vector3.left * Time.deltaTime * (moveSpeed * manager.GetPlatformMoveSpeed ()), Space.World);
		}
	}
	
	
	// Checks to see if any of the clouds should be reset
	// Called every frame from Update (), assuming isMoving is true
	void CheckForReset ()
	{
		for (int i = 0; i < clouds.Length; i++)
		{
			if (cloudTrans [i].position.x <= despawnAndRespawnGates.x)
			{
				Reset (i);
			}
		}
	}
	
	
	#endregion
	
	
	#region Resetting
	
	// Resets the cloud after it has passed through the despawn gate
	// Called from CheckForReset ()
	void Reset (int index)
	{
		// Set a random height
		cloudTrans [index].localPosition = new Vector3 (despawnAndRespawnGates.y, Random.Range (minAndMaxYSpawnPosGates.x, minAndMaxYSpawnPosGates.y), cloudTrans [index].position.z);
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
		manager = GameObject.Find ("&MainController").GetComponent <PlatformManager> ();
		cloudTrans = new Transform [clouds.Length];
		for (int i = 0; i < clouds.Length; i++) { cloudTrans [i] = clouds [i].transform; }
		if (startOn) isMoving = true;
	}
	
	#endregion
}
