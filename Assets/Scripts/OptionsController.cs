/*
 	OptionsController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		June 6, 2014
 	Last Edited:	June 18, 2014
 	
 	Controls the behavior of the options screen.
*/


using UnityEngine;
using System.Collections;


public class OptionsController : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The return to main menu button
		public GameObject returnToMenuButton;
		// The erase data button
		public GameObject eraseDataButton;
		// The sound effect volume knob stuff
		public Transform seVolumeKnobParentTrans;
		public Transform seVolumeKnobSpriteTrans;
		// The music volume knob stuff
		public Transform musicVolumeKnobParentTrans;
		public Transform musicVolumeKnobSpriteTrans;
		// The size range for volume knobs
		public Vector2 knobSizeRange;
		// The x position range for volume knobs
		public Vector2 knobXPosRange;
		// The two X positions for the cam indicator
		public Vector2 camIndicatorXPos;
		// The general object renderers for this scene
		public Renderer [] rends;
		// The camera override indicator
		public Transform camOverrideIndicator;
		// The YES and NO confirmation buttons
		public GameObject yesEraseButton;
		public GameObject noEraseButton;
		
		#endregion
		
		#region Scripts
		
		// The audio controller
		AudioController audioCont;
		// The input controller
		InputController inputCont;
		// The scene controller
		SceneController sceneCont;
		// The menu transition controller
		MenuBackgroundTransitionController transitionCont;
		// The bounce script for this gameobject
		BounceObject bounce;
		// The data controller
		DataController dataCont;
		// The camera controller
		CameraController camCont;
		
		#endregion
		
		#region Private
		
		//
		private Transform transParent;
		//
		private float transitionDropSpeed = 0;
		// If we are currently transitioning between menus
		private bool isTransitioning = false;
		// If we are currently adjusting the sound effects volume
		private bool isAdjustingSEVolume = false;
		// If we are currently adjusting the music volume
		private bool isAdjustingMusicVolume = false;
		// The current volume levels
		private float seVolume;
		private float musicVolume;
		// The camera overrdie type
		private int camOverrideType = 0;
		// The number of times the user has confirmed they want to erase the data
		private int eraseConfCount = 0;
		// The erase data confirmation label
		private Renderer eraseConfLabelRend;
		//
		private bool isConfirmingEraseProcessActive = false;
		// The reset mask object renderer
		private Renderer resetMaskRend;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		// If we aren't in the middle of a transition, we should be detecting input
		if (!isTransitioning)
		{
			CheckForInput ();
		}
		
		// If the sound effect volume is being adjusted, we should adjust it
		if (isAdjustingSEVolume)
		{
			AdjustSEVolume ();
		}
		
		// If the music volume is being adjusted, we should adjust it
		if (isAdjustingMusicVolume)
		{
			AdjustMusicVolume ();
		}
		
		// Move the cam override type indicator
		MoveCamOverrideIndicator ();
	}
	
	
	// Adjusts the sound effect volume
	// Called every frame from Update ()
	void AdjustSEVolume ()
	{
		// The X position of the touch, in world coordinates
		float touchXPos = inputCont.GetTouchPositionWorld ().x;
		
		// Limit the X range of the knobs
		if (touchXPos > knobXPosRange.y)
			touchXPos = knobXPosRange.y;
		else if (touchXPos < knobXPosRange.x)
			touchXPos = knobXPosRange.x;
		
		// Move the SE knob to the proper position
		seVolumeKnobParentTrans.position = new Vector3 (touchXPos, seVolumeKnobParentTrans.position.y, seVolumeKnobParentTrans.position.z);
		
		// Calculate the current SE volume
		seVolume = (1 / Mathf.Abs (knobXPosRange.x)) * (seVolumeKnobParentTrans.position.x + Mathf.Abs (knobXPosRange.x));
		seVolume = Mathf.Min (seVolume, 1);
		audioCont.SetSEVolume (seVolume);
	}
	
	
	// Adjusts the music volume
	// Called every frame from Update ()
	void AdjustMusicVolume ()
	{
		// The X position of the touch, in world coordinates
		float touchXPos = inputCont.GetTouchPositionWorld ().x;
		
		// Limit the X range of the knobs
		if (touchXPos > knobXPosRange.y)
			touchXPos = knobXPosRange.y;
		else if (touchXPos < knobXPosRange.x)
			touchXPos = knobXPosRange.x;
		
		// Move the music knob to the proper position
		musicVolumeKnobParentTrans.position = new Vector3 (touchXPos, musicVolumeKnobParentTrans.position.y, musicVolumeKnobParentTrans.position.z);
		
		// Calculate the current music volume
		musicVolume = (1 / Mathf.Abs (knobXPosRange.x)) * (musicVolumeKnobParentTrans.position.x + Mathf.Abs (knobXPosRange.x));
		musicVolume = Mathf.Min (musicVolume, 1);
		audioCont.SetMusicVolume (musicVolume);
	}
	
	
	// Moves the cam override indicator to the correct spot
	// Called every frame from Update ()
	void MoveCamOverrideIndicator ()
	{
		// The position we will be moving to
		Vector3 pos = Vector3.zero;
		
		// If we are in fit all mode...
		if (camOverrideType == 0)
		{
			pos = new Vector3 (camIndicatorXPos.x, camOverrideIndicator.localPosition.y, camOverrideIndicator.localPosition.z);
		}
		// If we are in stretch mode...
		else if (camOverrideType == 1)
		{
			pos = new Vector3 (camIndicatorXPos.y, camOverrideIndicator.localPosition.y, camOverrideIndicator.localPosition.z);
		}
		
		// Move the indicator
		camOverrideIndicator.localPosition = Vector3.Lerp (camOverrideIndicator.localPosition, pos, Time.deltaTime + 10.0f);
	}
	
	#endregion
	
	
	#region Input
	
	// Checks for and responds to user input
	// Called every frame from Update ()
	private void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// If we have touched on a volume knob...
				if (inputCont.GetTouchRaycastObject ().name == "SEVolumeKnob" && !isConfirmingEraseProcessActive)
				{
					isAdjustingSEVolume = true;
				}
				else if (inputCont.GetTouchRaycastObject ().name == "MusicVolumeKnob" && !isConfirmingEraseProcessActive)
				{
					isAdjustingMusicVolume = true;
				}
				else
				{
					// React
					ButtonTouched (inputCont.GetTouchRaycastObject ().name);
				}
			}
		}
		
		// If we release on the screen...
		if (inputCont.GetTouchReleased ())
		{
			// We are no longer adjusting any volume
			isAdjustingSEVolume = false;
			isAdjustingMusicVolume = false;
		}
	}
	
	
	// Reacts to a specific button being touched
	// Called from CheckForInput ()
	private void ButtonTouched (string buttonName)
	{
		// If we are confirming erase data, we don't want to be able to touch other buttons
		if (isConfirmingEraseProcessActive)
		{
			switch (buttonName)
			{
				case "EraseDataConfirmationYesButt":
					audioCont.PlayButtonPressSound ();
					YesConfirmEraseButtonPressed ();
				break;
				case "EraseDataConfirmationNoButt":
					audioCont.PlayButtonPressSound ();
					CancelEraseData ();
				break;
			}
			return;
		}
		
		switch (buttonName)
		{
			case "BackToMainMenuButtonOp":
				transitionCont.ChangeToScene (0);
				audioCont.PlayButtonPressSound ();
				StartCoroutine ("Rise");
			break;
			case "DisplayFitContentButton":
				camCont.SetDisplayMode (0);
				audioCont.PlayButtonPressSound ();
				camOverrideType = 0;
				dataCont.SaveCamOverrideType (0);
			break;
			case "DisplayStretchScreenButton":
				camCont.SetDisplayMode (1);
				audioCont.PlayButtonPressSound ();
				camOverrideType = 1;
				dataCont.SaveCamOverrideType (1);
			break;
			case "EraseDataButton":
				audioCont.PlayButtonPressSound ();
				EraseDataButtonPressed ();
			break;
		}
	}
	
	#endregion
	
	
	#region Erase Data
	
	// Called when the erase data button is pressed for the first time
	// Called from ButtonTouched (string buttonName)
	void EraseDataButtonPressed ()
	{
		// Activate confirmation stuff
		yesEraseButton.SetActive (true);
		noEraseButton.SetActive (true);
		eraseConfLabelRend.enabled = true;
		isConfirmingEraseProcessActive = true;
		resetMaskRend.material.color = new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.85f);
	}
	
	
	// Called when the user confirms that they want to erase their data
	//
	void YesConfirmEraseButtonPressed ()
	{
		eraseConfCount ++;
		switch (eraseConfCount)
		{
			case 1:
				eraseConfLabelRend.gameObject.GetComponent <TextMesh> ().text = "are you REALLY sure? You can't undo this action";
			break;
			case 2:
				eraseConfLabelRend.gameObject.GetComponent <TextMesh> ().text = "okay for real it's gonna happen, but are you SURE?";
			break;
			case 3:
				eraseConfLabelRend.gameObject.GetComponent <TextMesh> ().text = "erasing data...";
				yesEraseButton.SetActive (false);
				noEraseButton.SetActive (false);
				EraseData ();
				Invoke ("CancelEraseData", 2.0f);
			break;
		}
	}
	
	
	// Cancels the erase data process
	//
	void CancelEraseData ()
	{
		yesEraseButton.SetActive (false);
		noEraseButton.SetActive (false);
		eraseConfLabelRend.enabled = false;
		isConfirmingEraseProcessActive = false;
		eraseConfCount = 0;
		eraseConfLabelRend.gameObject.GetComponent <TextMesh> ().text = "are you SURE you want to erase your data?";
		resetMaskRend.material.color = new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f);
	}
	
	
	// Actually erases the data
	//
	void EraseData ()
	{
		dataCont.EraseData ();
	}
	
	#endregion
	
	
	#region Scene Switching
	
	// Called when the scene is setting up
	//
	public void Setup ()
	{
		// Set the position of the volume knobs based on saved volume info
		SetLoadedKnobPositions ();
		
		// Set the position of the cam override indicator
		SetCamIndicatorPosition ();
		
		// Activate the renderers
		foreach (Renderer r in rends) { r.enabled = true; }
		
		// Activate the buttons
		returnToMenuButton.SetActive (true);
		eraseDataButton.SetActive (true);
		
		// Begin the transition
		StartCoroutine ("Drop");
	}
	
	
	//
	// Called from Setup ()
	IEnumerator Drop ()
	{
		// While the transform parent is still above the target, drop it
		while (transParent.position.y > 0.5f)
		{
			transitionDropSpeed += Time.deltaTime * 2f;
			transParent.Translate (Vector3.down * transitionDropSpeed);
			
			yield return null;
		}
		
		// Now that the transform parent is out of the way, we can cleanup the scene
		transitionDropSpeed = 0;
		bounce.Bounce ();
		isTransitioning = false;
	}

	
	//
	//
	IEnumerator Rise ()
	{
		// While the transform parent is still above the target, drop it
		audioCont.PlaySound ("Button");
		isTransitioning = true;
		while (transParent.position.y < 11.63509f)
		{
			transitionDropSpeed += Time.deltaTime * 1.5f;
			transParent.Translate (Vector3.up * transitionDropSpeed);
			
			yield return null;
		}
		
		// Now that the transform parent is out of the way, we can cleanup the scene
		transitionDropSpeed = 0;
		transParent.position = new Vector3 (0, 12.63509f, 0);
		
		// Cleanup the scene objects
		Cleanup ();
	}
	
	
	// Called when the scene is cleaning up
	// Called from Rise ()
	private void Cleanup ()
	{
		//
		foreach (Renderer r in rends) { r.enabled = false; }
		
		//
		returnToMenuButton.SetActive (false);
		eraseDataButton.SetActive (false);
		isAdjustingSEVolume = false;
		isAdjustingMusicVolume = false;
		transitionDropSpeed = 0;
		dataCont.SaveSEVolume (seVolume);
		dataCont.SaveMusicVolume (musicVolume);
		eraseConfCount = 0;
		yesEraseButton.SetActive (false);
		noEraseButton.SetActive (false);
		eraseConfLabelRend.enabled = false;
		isConfirmingEraseProcessActive = false;
		resetMaskRend.material.color = new Color (resetMaskRend.material.color.r, resetMaskRend.material.color.g, resetMaskRend.material.color.b, 0.0f);
		
		// Change back to the main menu scene
		sceneCont.ChangeScene (1);
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Set the position of the volume knobs based on saved volume info
		SetLoadedKnobPositions ();
		
		// Set the position of the cam override indicator
		SetCamIndicatorPosition ();
	}
	
	
	// Sets the position of the volume knobs based on saved volume info
	// Called from Start () and Setup ()
	void SetLoadedKnobPositions ()
	{
		// Make sure the volume info matches the saved data
		seVolume = dataCont.GetSEVolume ();
		musicVolume = dataCont.GetMusicVolume ();
		
		float seXPos = knobXPosRange.x + (dataCont.GetSEVolume () * Mathf.Abs (knobXPosRange.x));
		seVolumeKnobParentTrans.position = new Vector3 (seXPos, seVolumeKnobParentTrans.position.y, seVolumeKnobParentTrans.position.z);
		float musicXPos = knobXPosRange.x + (dataCont.GetMusicVolume () * Mathf.Abs (knobXPosRange.x));
		musicVolumeKnobParentTrans.position = new Vector3 (musicXPos, musicVolumeKnobParentTrans.position.y, musicVolumeKnobParentTrans.position.z);
	}
	
	
	// Sets the position of the cam resolution indicator
	// Called from Start () and Setup ()
	void SetCamIndicatorPosition ()
	{
		// Get the type
		camOverrideType = dataCont.GetCamType ();
		
		// Snap the indicator
		if (camOverrideType == 0)
		{
			camOverrideIndicator.localPosition = new Vector3 (camIndicatorXPos.x, camOverrideIndicator.localPosition.y, camOverrideIndicator.localPosition.z);
		}
		else if (camOverrideType == 1)
		{
			camOverrideIndicator.localPosition = new Vector3 (camIndicatorXPos.y, camOverrideIndicator.localPosition.y, camOverrideIndicator.localPosition.z);
		}
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		sceneCont = gameObject.GetComponent <SceneController> ();
		inputCont = gameObject.GetComponent <InputController> ();
		audioCont = gameObject.GetComponent <AudioController> ();
		dataCont = gameObject.GetComponent <DataController> ();
		transitionCont = gameObject.GetComponent <MenuBackgroundTransitionController> ();
		bounce = GameObject.Find ("*OptionsScene").GetComponent <BounceObject> ();
		transParent = GameObject.Find ("*OptionsScene").transform;
		camCont = GameObject.Find ("Main Camera").GetComponent <CameraController> ();
		eraseConfLabelRend = GameObject.Find ("EraseDataConfirmationText").renderer;
		resetMaskRend = GameObject.Find ("ScreenMask").renderer;
	}
	
	#endregion
}
