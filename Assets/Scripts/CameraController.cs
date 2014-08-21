/*
 	CameraController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 13, 2014
 	Last Edited:	February 14, 2014
 	
 	Controls the camera behavior and movement.
*/


using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The camera limits on the Y axis
		public Vector2 threshold = new Vector2 (0, 0);
		// How quickly the camera follows the player
		public float followSpeed = 2.0f;
		// The camera position for the main menu
		public Vector3 mainMenuPos = Vector3.zero;
		//
		public float shakeMagnitude = 10.0f;
		
		#endregion
		
		#region Scripts
		
		// The data controller
		DataController dataCont;
		
		#endregion
	
		#region Caches
		
		// The transform of the target the camera will follow
		private Transform target;
		// The transform of the camera
		private Transform trans;
		
		#endregion
		
		#region Private
		
		// If the camera is in main menu mode
		private bool isInMainMenuMode = true;
		// The sprite camera
		private tk2dCamera spriteCam;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called once per frame, after the update loop
	void LateUpdate () 
	{
		// Depending on the scene, the camera's behavior changes
		if (isInMainMenuMode) MainMenuSnap ();
		else PlayerFollow ();
	}
	
	
	// Makes the camera follow the player 
	// Called every frame from LateUpdate ()
	void PlayerFollow ()
	{
		// The position the camera will be moving to
		Vector3 followPos = new Vector3 (trans.position.x, target.position.y, trans.position.z);
		
		// Set threshold limits
		if (followPos.y < threshold.x)
			followPos = new Vector3 (trans.position.x, threshold.x, trans.position.z);
		else if (followPos.y > threshold.y)
			followPos = new Vector3 (trans.position.x, threshold.y, trans.position.z);
		
		// Move the camera smoothly
		trans.position = Vector3.Lerp (trans.position, followPos, Time.deltaTime * followSpeed);
	}
	
	
	// Moves the camera to the main menu position
	// Called every frame from LateUpdate ()
	void MainMenuSnap ()
	{
		trans.position = Vector3.Lerp (trans.position, mainMenuPos, Time.deltaTime * 10.0f);
	}
	
	#endregion
	
	
	#region Public
	
	// Sets the camera mode
	//
	public void SetIsInMainMenuMode (bool b)
	{
		isInMainMenuMode = b;
	}
	
	
	// Begins the process of shaking the camera after the sloth has landed particularly hard
	// Called from 
	public void Shake ()
	{
		StartCoroutine ("ShakeCam");
	}
	
	
	// Makes the camera "shake" after the sloth has landed particularly hard
	// Called from Shake ()
	IEnumerator ShakeCam () 
	{
		Vector3 originalCamPos = trans.position;
		float firstYPos = originalCamPos.y - 0.17f;
		float secondYPos = originalCamPos.y + 0.14f;
		bool isShaking = true;
		
		while (isShaking) 
		{
			while (trans.position.y > firstYPos) { trans.Translate (Vector3.down * Time.deltaTime * shakeMagnitude); yield return null;}
			while (trans.position.y < secondYPos) { trans.Translate (Vector3.up * Time.deltaTime * shakeMagnitude); yield return null; }
			while (trans.position.y > originalCamPos.y) { trans.Translate (Vector3.down * Time.deltaTime * shakeMagnitude); yield return null;}
			isShaking = false;
			
			yield return null;
		}
		
		trans.position = originalCamPos;
	}
	
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Awake ()
	{
		//print (SystemInfo.deviceModel);
		
		// Set the target frame rate of the application
		Application.targetFrameRate = 60;
	}
	
	
	// Used for initialization
	// Called automatically at beginning (after Awake ())
	void Start () 
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Set the display mode of the camera
		SetDisplayMode (dataCont.GetCamType ());
	}
	
	 
	// Sets the display mode for the sprote camera
	// Called from Start () and ButtonTouched (string buttonName) in OptionsController.cs
	public void SetDisplayMode (int mode)
	{
		if (mode == 0)
		{
			spriteCam.resolutionOverride [0].autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible;
		}
		else if (mode == 1)
		{
			spriteCam.resolutionOverride [0].autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.StretchToFit;
		}
	}
	
	
	// Assigns variables at the beginning
	// Called from Start ()
	private void AssignVariables ()
	{
		target = GameObject.Find ("Player").transform;
		dataCont = GameObject.Find ("&MainController").GetComponent <DataController> ();
		spriteCam = GetComponent <tk2dCamera> ();
		trans = transform;
	}
	
	#endregion
}
