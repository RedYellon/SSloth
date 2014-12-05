/*
 	FireflyBehavior.cs
 	Michael Stephens
 	
 	Created:		May 27, 2014
 	Last Edited:	November 28, 2014
 	
 	Controls the firefly glowing/moving.
*/


using UnityEngine;
using System.Collections;


public class FireflyBehavior : MonoBehaviour 
{
	#region Variables

		#region Public
		
		// How quickly the firefly glows
		public float glowRate = 5.0f;
		// How quickly the firefly switches between fading in/out
		public float switchTime = 1.0f;
		public float switchTimeMove = 1.0f;
		// The glow colors for the firefly
		public Color glowColor1;
		public Color glowColor2;
		// The min and max glow alpha vars
		public Vector2 glowAlphaMinMax;
		// The glowing gameobject
		public GameObject glowChild;
		
		#endregion
		
		#region Public
			
		// The cached sprite
		private tk2dSprite sprite;
		// Bool used to fade firefly in/out
		private bool b = false;		
		// Whether or not the firefly is currently moving across the screen
		private bool isInMotion = false;
		// Determines if the firefly will be acivating/deactivating/doing nothing when it resets
		private int resetAction = 0;
		// The cached rends
		private Renderer rend;
		private Renderer glowRend;
		
		#endregion

	#endregion
	
	
	#region Glow
	
	// Makes the firefly glow
	// Called from Start ()
	IEnumerator GlowFly ()
	{
		// While this gameobject exists...
		while (gameObject)
		{
			// If the firefly should be fading in...
			if (!b)
			{
				sprite.color = Color.Lerp (sprite.color, glowColor1, Time.deltaTime * glowRate);
			}
			else
			{
				sprite.color = Color.Lerp (sprite.color, glowColor2, Time.deltaTime * glowRate);
			}
			
			yield return null;
		}
	}
	
	
	// Switches the firefly between fading in/out
	// Called from Start ()
	void SwitchBool ()
	{
		b = !b;
	}
	
	#endregion
	
	
	#region Activation
	
	// Sets this firefly's active state
	// Called from CheckForTimeChange () and Reset () in TimeController.cs
	public void SetIsActive (bool b)
	{
		// If the firefly is moving across the screen, we need to wait to activate/deactivate it until it resets
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
	
	
	// Turns the firefly on
	//
	void TurnOn ()
	{
		rend.enabled = true;
		glowRend.enabled = true;
	}
	
	
	// Turns the firefly off
	//
	void TurnOff ()
	{
		rend.enabled = false;
		glowRend.enabled = false;
	}
	
	#endregion
	
	
	#region Public
	
	// Sets the firefly's motion state
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
		// Assign initial variables
		AssignVariables ();
		
		// Begin the glowing
		StartCoroutine ("GlowFly");
		
		// Begin periodical switching between fading in/out
		InvokeRepeating ("SwitchBool", 0, switchTime);
	}
	
	
	// Assigns the local variables
	// Called from Start ()
	private void AssignVariables ()
	{
		sprite = glowChild.GetComponent <tk2dSprite> ();
		rend = renderer;
		glowRend = glowChild.renderer;
	}
	
	#endregion
}
