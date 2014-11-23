/*
 	SnowflakeManager.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 23, 2014
 	Last Edited:	November 23, 2014
 	
 	Controls the spawning and coordination of snowflakes
 	in the winter theme.
*/


using UnityEngine;
using System.Collections;


public class SnowflakeManager : MonoBehaviour 
{
	#region Variables

		#region Public

		// The snowflake prefab
		public GameObject snowflakePrefab;
		// The Y value at which the snowflakes spawn
		public float ySpawnValue;
		// The range of X values the flakes can spawn at
		public Vector2 xSpawnValueRange;
		// The size of the flake pool created at the beginning
		public int flakePoolSize = 20;
		// The rate that snowflakes spawn at
		public Vector2 spawnRateRange;

		#endregion

		#region Private

		// The pool of snowflake objects
		private Snowflake [] _snowflakes;
		// The snowflake parent object
		private Transform _flakeParent;

		#endregion

	#endregion


	#region Spawning

	// Spawns a new snowflake
	//
	void SpawnSnowflake ()
	{
		// Get the next available snowflake
		for (int i = 0; i < _snowflakes.Length; i++)
		{
			if (!_snowflakes [i].GetIsInUse ())
			{
				// Move the flake to it's spawning position
				float xpos = Random.Range (xSpawnValueRange.x, xSpawnValueRange.y);
				_snowflakes [i].transform.position = new Vector3 (xpos, ySpawnValue, _snowflakes [i].transform.position.z);

				// Activate and send the flake on its way
				_snowflakes [i].SpawnFlake ();
				Invoke ("SpawnSnowflake", Random.Range (spawnRateRange.x, spawnRateRange.y));

				return;
			}
		}
		Invoke ("SpawnSnowflake", Random.Range (spawnRateRange.x, spawnRateRange.y));
	}


	// Turns off snowflakes and spawning
	//
	public void TurnOffSnowflakes ()
	{
		// Stop spawning flakes
		CancelInvoke ();

		// Return all flakes to their proper spot
		for (int i = 0; i < _snowflakes.Length; i++)
		{
			_snowflakes [i].DespawnFlake ();
			_snowflakes [i].transform.position = new Vector3 (100, 100, _snowflakes [i].transform.position.z);
			_snowflakes [i].gameObject.SetActive (false);
		}
	}


	// Begins spawning snowflakes
	//
	public void TurnOnSnowflakes ()
	{
		// Activate snowflakes
		for (int i = 0; i < _snowflakes.Length; i++)
			_snowflakes [i].gameObject.SetActive (true);

		// Begin spawning snowflakes
		Invoke ("SpawnSnowflake", Random.Range (spawnRateRange.x, spawnRateRange.y));
	}

	#endregion


	#region Layer Order

	// Sends the snowflakes to the front
	void SendToFront ()
	{
		for (int i = 0; i < _snowflakes.Length; i++)
		{
			_snowflakes [i].faceSprite.renderer.sortingLayerName = "ForegroundScreenEffects";
			_snowflakes [i].flakeSprite.renderer.sortingLayerName = "ForegroundScreenEffects";
		}
	}


	// Sends the snowflakes to the back
	void SendToBack ()
	{
		for (int i = 0; i < _snowflakes.Length; i++)
		{
			_snowflakes [i].faceSprite.renderer.sortingLayerName = "BackgroundScreenEffects";
			_snowflakes [i].flakeSprite.renderer.sortingLayerName = "BackgroundScreenEffects";
		}
	}

	#endregion


	#region EventManager
	
	// Subscribes to events
	// Called automatically when this script is enabled
	void OnEnable ()
	{
		EventManager.OnRoundRestart += SendToFront;
		EventManager.OnRoundEnd += SendToBack;
		EventManager.OnBackToMainMenuFromGame += SendToFront;
	}
	
	
	// Unsubscribes to events
	// Called automatically when this script is disabled
	void OnDisable ()
	{
		EventManager.OnRoundRestart -= SendToFront;
		EventManager.OnRoundEnd -= SendToBack;
		EventManager.OnBackToMainMenuFromGame -= SendToFront;
	}
	
	#endregion


	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Awake ()  
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();

		// Create the pool of snowflakes to be used
		CreateSnowflakePool ();
	}


	// Creates an object pool of snowflakes
	// Called from Start ()
	void CreateSnowflakePool ()
	{
		// Create the pool
		_snowflakes = new Snowflake [flakePoolSize];
		for (int i = 0; i < flakePoolSize; i++)
		{
			Snowflake s = ((GameObject) Instantiate (snowflakePrefab, Vector3.zero, Quaternion.identity)).GetComponent <Snowflake> ();
			_snowflakes [i] = s;
			_snowflakes [i].transform.parent = _flakeParent;
			_snowflakes [i].transform.position = new Vector3 (100, 100, _snowflakes [i].transform.position.z);
			_snowflakes [i].gameObject.SetActive (false);
		}
	}
	
	
	// Assigns initial variables
	// Called initially from Start ()
	private void AssignVariables ()
	{
		_flakeParent = GameObject.Find ("*SnowflakeHolder").transform;
	}
	
	#endregion
}
