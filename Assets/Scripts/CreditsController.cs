/*
 	CreditsController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 26, 2014
 	Last Edited:	February 26, 2014
 	
 	Controls the credits scene.
*/


using UnityEngine;
using System.Collections;


public class CreditsController : MonoBehaviour 
{
	#region Variables
	
		#region Public
	
		// The list of renderers to turn on/off
		public Renderer [] nameRends;
		public Renderer [] otherRends;
		//
		public Vector3 msHighlighterPos;
		public Vector3 nyHighlighterPos;
		public Vector3 cgHighlighterPos;
		public Vector3 adHighlighterPos;
		public Vector3 tbHighlighterPos;
		//
		public TextMesh jobText;
		public tk2dSprite slothJobSprite;
		public tk2dSprite slothJobSpriteShadow;
	
		#endregion
	
		#region Scripts
	
		// The audio controller
		AudioController audioCont;
		// The scene controller
		SceneController sceneCont;
		// The input controller
		InputController inputCont;
		// The menu transition controller
		MenuBackgroundTransitionController transitionCont;
		// The bounce script for this gameobject
		BounceObject bounce;
	
		#endregion
	
		#region Private
	
		// If we can press the main menu button
		private bool canPressButton = false;
		// The back to main menu button
		private GameObject backToMenuButton;
		//
		private Collider [] nameColliders;
		//
		private Transform highlighter;
		//
		private Vector3 highlighterTargetPosition;
		//
		private Transform transParent;
		//
		private float transitionDropSpeed = 0;
		//
		private bool isTransitioning = true;
	
		#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
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
		if (!isTransitioning)
		{
			if (canPressButton) CheckForInput ();
			MoveHighlighter ();
		}
	}
	
	
	//
	//
	private void CheckForInput ()
	{
		// If we touch down on the screen...
		if (inputCont.GetTouchDown ())
		{
			// If we have touched a button...
			if (inputCont.GetTouchRaycastObject () != null)
			{
				// React
				ButtonTouched (inputCont.GetTouchRaycastObject ().name);
			}
		}
	}
	
	
	// Moves the menu highlighter over the correct (currently actuve) menu option
	//
	void MoveHighlighter ()
	{
		highlighter.position = Vector3.Lerp (highlighter.position, highlighterTargetPosition, Time.deltaTime * 15.0f);
	}
	
	
	//
	//
	private void ButtonTouched (string buttonName)
	{
		switch (buttonName)
		{
			case "BackToMainMenuButton":
				transitionCont.ChangeToScene (0);
				StartCoroutine ("Rise");
			break;
			case "MSLabel":
				highlighterTargetPosition = msHighlighterPos;
				slothJobSprite.SetSprite ("TeamSloth_Code");
				slothJobSpriteShadow.SetSprite ("TeamSloth_Code");
				jobText.text = "(code)";
				audioCont.PlaySound ("MainMenuButton");
			break;
			case "NYLabel":
				highlighterTargetPosition = nyHighlighterPos;
				slothJobSprite.SetSprite ("TeamSloth_Business");
				slothJobSpriteShadow.SetSprite ("TeamSloth_Business");
				jobText.text = "(production)";
				audioCont.PlaySound ("MainMenuButton");
			break;
			case "CGLabel":
				highlighterTargetPosition = cgHighlighterPos;
				slothJobSprite.SetSprite ("TeamSloth_Art1");
				slothJobSpriteShadow.SetSprite ("TeamSloth_Art1");
				jobText.text = "(art)";
				audioCont.PlaySound ("MainMenuButton");
			break;
			case "ADLabel":
				highlighterTargetPosition = adHighlighterPos;
				slothJobSprite.SetSprite ("TeamSloth_Audio");
				slothJobSpriteShadow.SetSprite ("TeamSloth_Audio");
				jobText.text = "(music)";
				audioCont.PlaySound ("MainMenuButton");
			break;
			case "TBLabel":
				highlighterTargetPosition = tbHighlighterPos;
				slothJobSprite.SetSprite ("TeamSloth_Art2");
				slothJobSpriteShadow.SetSprite ("TeamSloth_Art2");
				jobText.text = "(art)";
				audioCont.PlaySound ("MainMenuButton");
			break;
		}
	}
	
	#endregion
	
	
	#region Scene Switching
	
	//
	//
	private void Cleanup ()
	{
		foreach (Renderer r in nameRends)
			r.enabled = false;
		foreach (Renderer r in otherRends)
			r.enabled = false;
		foreach (Collider c in nameColliders)
			c.enabled = false;
			
		backToMenuButton.SetActive (false);
		//audioCont.PlaySound ("Button");
		transitionDropSpeed = 0;
		jobText.renderer.enabled = false;
		
		canPressButton = false;
		sceneCont.ChangeScene (1);
	}
	
	
	//
	//
	public void Setup ()
	{
		isTransitioning = true;
		foreach (Renderer r in nameRends)
			r.enabled = true;
		foreach (Renderer r in otherRends)
			r.enabled = true;
		foreach (Collider c in nameColliders)
			c.enabled = true;
			
		backToMenuButton.SetActive (true);
		//jobText.renderer.enabled = true;	
		
		//
		StartCoroutine ("Drop");
	}
	
	
	//
	//
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
		//transParent.position = Vector3.zero;
		bounce.Bounce ();
		isTransitioning = false;
		canPressButton = true;
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
		Cleanup ();
	}
	
	#endregion
	
	
	#region Utility
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		sceneCont = gameObject.GetComponent <SceneController> ();
		inputCont = gameObject.GetComponent <InputController> ();
		audioCont = gameObject.GetComponent <AudioController> ();
		transitionCont = gameObject.GetComponent <MenuBackgroundTransitionController> ();
		backToMenuButton = GameObject.Find ("BackToMainMenuButton");
		highlighter = GameObject.Find ("HighlighterBar").transform;
		transParent = GameObject.Find ("*CreditsScene").transform;
		bounce = GameObject.Find ("*CreditsScene").GetComponent <BounceObject> ();
		
		backToMenuButton.SetActive (false);
		nameColliders = new Collider [nameRends.Length];
		for (int i = 0; i < nameRends.Length; i++) { nameColliders [i] = nameRends [i].collider; }
		highlighterTargetPosition = msHighlighterPos;
	}
	
	#endregion
}
