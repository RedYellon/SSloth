


using UnityEngine;
using System.Collections;


public class TitleAnimation : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The array of materials tp switch between
		public Material [] frames;
		// The speed that the frames advance
		public float animationSpeed = 0.1f;
	
		#endregion
		
		#region Caches
		
		// The cached renderer
		private Renderer rend;
	
		#endregion
	
		#region Private
		
		// The current frame
		private int currentFrame = 0;
	
		#endregion
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start () 
	{
		AssignVariables ();
		InvokeRepeating ("Animate", animationSpeed, animationSpeed);
	}
	
	#endregion
	
	
	#region Animation
	
	// Advances to the next frame
	//
	void Animate ()
	{
		// Advance the frame
		currentFrame++;
		if (currentFrame > 7)
			currentFrame = 0;
			
		// Change the material
		rend.material = frames [currentFrame];
	}
	
	#endregion
	
	
	#region Utility
	
	// Assigns the variables at the beginning
	//
	private void AssignVariables ()
	{
		rend = renderer;
	}
	
	#endregion
}
