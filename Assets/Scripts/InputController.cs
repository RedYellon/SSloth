/*
 	InputController.cs
 	Michael Stephens
 	
 	Created:		September 18, 2013
 	Last Edited:	October 3, 2013
 	
 	Searches for user input.
*/


using UnityEngine;
using System.Collections;


public class InputController : MonoBehaviour 
{
	#region Variables
	
		#region TouchBools
	
		// If the player touched down this frame
		private bool touchDown = false;
		// If the player is holding a touch for this frame
		private bool touchHold = false;
		// If the player released a touch this frame
		private bool touchReleased = false;
		// Whether or not we are touching at all (touchDown OR touchHold)
		private bool isTouching = false;
	
		#endregion
		
		#region TouchInfo
		
		// The position of our most recent touch
		private Vector3 touchPosition = Vector3.zero;
		// The distance our touch traveled from the last frame
		private Vector3 touchPositionDelta = Vector3.zero;
		// The object we are touching (if no object is being touched, we return nothing)
		private Transform touchedObject;
		// The position of the raycast hit in world coordinates
		private Vector3 touchPositionWorld = Vector3.zero;
		
		#endregion
		
		#region Private
		
		// The position of our touch the previous frame
		private Vector3 previousTouchPosition = Vector3.zero;
		
		#endregion
	
	#endregion
	
	
	// Checks for input every frame
	void InputCheck ()
	{
		// We need to reset the touch variables first
		ResetTouchVariables ();
		
		// Check for touch events
		if (Input.GetMouseButtonDown (0)) { touchDown = true; isTouching = true; }
		else if (Input.GetMouseButton (0)) { touchHold = true; isTouching = true; }
		else if (Input.GetMouseButtonUp (0)) { touchReleased = true; }
		
		// If we are touching, we need to get the position of our touch, as well as raycast it
		if (touchDown || touchHold)
		{
			SetTouchPosition ();
			SetTouchPositionWorld ();
			SetTouchPositionDelta ();
			SetTouchRaycastObject ();
		}
	}
	
	
	// Sets the position (Vector3) of the most recent touch
	void SetTouchPosition ()
	{
		touchPosition = Input.mousePosition;	
	}
	
	
	//
	void SetTouchPositionWorld ()
	{
		touchPositionWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	
	
	// Sets the difference in touch position from the previous frame to this frame
	void SetTouchPositionDelta ()
	{
		touchPositionDelta = (touchPosition - previousTouchPosition);
		previousTouchPosition = touchPosition;
	}
	
	
	// Raycasts from the main camera to the touch position
	void SetTouchRaycastObject () 
	{
		Ray ray = Camera.main.ScreenPointToRay (touchPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) { touchedObject = hit.transform; touchPositionWorld = hit.point; } else { touchedObject = null; touchPositionWorld = Vector3.zero; }
	}
	
	
	// Called once per frame
	void Update () 
	{ InputCheck (); }
	
	
	// Resets the touch variables
	void ResetTouchVariables ()
	{
		touchDown = false;
		touchHold = false;
		touchReleased = false;
		isTouching = false;
	}
	
	
	#region Public Outlets
	
	public bool GetTouchDown () { return touchDown; }
	public bool GetTouchHold () { return touchHold; }
	public bool GetTouchReleased () { return touchReleased; }
	public bool GetIsTouching () { return isTouching; }
	public Vector3 GetTouchPosition () { return touchPosition; }
	public Vector3 GetTouchPositionDelta () { return touchPositionDelta; }
	public Transform GetTouchRaycastObject () { return touchedObject; }
	public Vector3 GetTouchPositionWorld () { return touchPositionWorld; }
	
	#endregion
}
