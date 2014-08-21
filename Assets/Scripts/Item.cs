/*
 	Item.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		March 16, 2014
 	Last Edited:	March 16, 2014
 	
 	Controls the behavior of this specific item.
*/


using UnityEngine;
using System.Collections;


public class Item : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// The display name of this item
		public string displayName = "Item Name";
		// The standalone material of this item
		public Material itemMaterial;
		// The description of this item
		public string itemDescription = "Item Description";
		
		#endregion
		
		#region Scripts
		
		
		
		#endregion
		
		#region References
		
		
		
		#endregion
		
		#region Private
		
		
		
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
		
	}
	
	#endregion
	
	
	#region Utility
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		
	}
	
	#endregion
}
