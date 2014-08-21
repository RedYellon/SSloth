/*
 	Flash.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		February 28, 2014
 	Last Edited:	February 28, 2014
 	
 	Makes the renderer of the attached object flash momentarily.
*/


using UnityEngine;
using System.Collections;


public class Flash : MonoBehaviour 
{
	#region Variables
	
		#region Private
	
		// Whether or not the object is currently flashing
		private bool isFlashing = false;
		// If the object is currently fading in
		private bool isFadingIn = false;
		// If the object is currently waiting to fade out
		private bool isWaitingToFadeOut = false;
		// If the object is currently fading out
		private bool isFadingOut = false;
		// The speed the object will fade in with
		private float fadeInS;
		// The speed the object will fade out with
		private float fadeOutS;
		// The time the object lasts before fading out
		private float durationS;
		// The target opacity before the object begins to hold/fadeOut
		private float targetOpacity;
		// The cached sprite
		private tk2dSprite sprite;
	
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
		if (isFlashing)
		{
			if (isFadingIn)
			{
				FadeIn ();
			}
			else if (isFadingOut)
			{
				FadeOut ();
			}
		}
	}
	
	
	// Makes the renderer fade in
	// Called every frame from Update ()
	private void FadeIn ()
	{
		if (sprite.color.a < (targetOpacity - 0.01f))
		{
			Color fadeToColor = new Color (sprite.color.r, sprite.color.g, sprite.color.b, targetOpacity);
			sprite.color = Color.Lerp (sprite.color, fadeToColor, Time.deltaTime * fadeInS);
		}
		else
		{
			isFadingIn = false;
			isFadingOut = true;
		}
	}
	
	
	//
	//
	private void FadeOut ()
	{
		if (sprite.color.a > 0)
		{
			Color fadeToColor = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 0.0f);
			sprite.color = Color.Lerp (sprite.color, fadeToColor, Time.deltaTime * fadeOutS);
		}
		else
		{
			isFadingOut = false;
			isFlashing = false;
		}
	}
	
	#endregion
	
	
	#region Public
	
	// Makes the object flash
	// Called from whatever
	public void FlashObject (float fadeInSpeed, float duration, float fadeOutSpeed)
	{
		// Set fade perameters
		fadeInS = fadeInSpeed;
		durationS = duration;
		fadeOutS = fadeOutSpeed;
		targetOpacity = 1.0f;
		
		// Begin flashing
		isFadingIn = true;
		isFlashing = true;
	}
	
	
	// Returns whether or not this object is currently flashing
	// Called from whatever
	public bool GetIsFlashing ()
	{
		return isFlashing;
	}
	
	#endregion
	
	
	#region Utility
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		sprite = GetComponent <tk2dSprite> ();
	}
	
	#endregion
}
