using UnityEngine;
using System.Collections;


public class Slothlopedia : MonoBehaviour 
{
	#region Variables
	
		#region Public
	
		// The list of slothlopedia entry prefabs
		public GameObject [] entryPrefabs;
	
		#endregion
	
	#endregion
	
	
	#region Button Response
	
	//
	//
	public void SectionButtonPressed (int buttonNum)
	{
		print (buttonNum);
	}
	
	#endregion
	
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at beginning
	void Start ()
	{
		// Assign the initial private/script/reference variables
		AssignVariables ();
	}
	
	
	// Assigns the initial private/script/reference variables
	// Called from Start ()
	private void AssignVariables ()
	{
		
	}
	
	#endregion
}