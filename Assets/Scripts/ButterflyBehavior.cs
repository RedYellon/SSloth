/*
 	ButterflyBehavior.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		June 6, 2014
 	Last Edited:	June 6, 2014
 	
 	Controls the behavior of the decorative butterfly.
*/


using UnityEngine;
using System.Collections;


public class ButterflyBehavior : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// How quickly the butterfly changes animation frames
		public float frameChangeRate = 0.1f;
		// How quickly the butterfly changes directions
		public float directionFlipRate = 0.35f;
		
		#endregion
		
		#region References
		
		// The butterfly transform
		private Transform trans;
		// The butterfly sprite
		private tk2dSprite sprite;
		// The butterfly renderer
		private Renderer rend;
		//
		private int resetAction = 0;
		
		#endregion
		
		#region Private
		
		// Used to determine what frame the butterfly should change to
		private bool frameChangeBool = false;
		// Used to determine what direction the butterfly should flip to
		private bool directionFlipBool = false;
		//
		private bool isInMotion = false;
		
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update ()
	{
		
	}
	
	#endregion
	
	
	#region Animation
	
	// Changes the frame of the butterfly
	// Called from Start ()
	void ChangeFrame ()
	{
		// Change the animation frame
		if (frameChangeBool)
			sprite.SetSprite ("butterfly_1");
		else
			sprite.SetSprite ("butterfly_2");
			
		// Flip the bool so the frame changes next time
		frameChangeBool = !frameChangeBool;
	}
	
	
	// Changes the direction of the butterfly
	// Called from Start ()
	void FlipDirection ()
	{
		// Flip the butterfly direction
		if (directionFlipBool)
			trans.localScale = new Vector3 (-1, 1, 1);
		else
			trans.localScale = new Vector3 (1, 1, 1);
		
		// Flip the bool so the direction changes next time
		directionFlipBool = !directionFlipBool;
	}
	
	#endregion
	
	
	#region Activation
	
	// Sets this butterfly's active state
	// Called from CheckForTimeChange () and Reset () in TimeController.cs
	public void SetIsActive (bool b)
	{
		// If the butterfly is moving across the screen, we need to wait to activate/deactivate it unitil it resets
		if (isInMotion)
		{
			if (b)
				resetAction = 1;
			else
				resetAction = 2;
			
			// Now we can leave the function
			return;
		}
		
		// If we are turning the firefly off...
		if (!b)
		{
			// Make the firefly renderers invisible
			TurnOff ();
		}
		// If we are turning the firefly on...
		else
		{
			// Make the firefly renderers visible
			TurnOn ();
		}
	}
	
	
	//
	//
	public void ResetSetActiveCheck ()
	{
		// 
		switch (resetAction)
		{
			// Turn on
			case 1:
				TurnOn ();
			break;
			// Turn off
			case 2:
				TurnOff ();
			break;
		}
		
		// Reset the reset action
		resetAction = 0;
		isInMotion = false;
	}
	
	
	// Turns the butterfly on
	//
	void TurnOn ()
	{
		rend.enabled = true;
	}
	
	
	// Turns the butterfly off
	//
	void TurnOff ()
	{
		rend.enabled = false;
	}
	
	#endregion
	
	
	#region Public
	
	// Sets the butterfly's motion state
	// Called from
	public void SetIsInMotion (bool b)
	{
		isInMotion = b;
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
		
		// Begin the animations
		InvokeRepeating ("ChangeFrame", frameChangeRate, frameChangeRate);
		InvokeRepeating ("FlipDirection", directionFlipRate, directionFlipRate);
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		trans = transform;
		sprite = GetComponent <tk2dSprite> ();
		rend = renderer;
	}
	
	#endregion
}
