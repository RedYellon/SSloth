/*
 	BackgroundController.cs
 	Michael Stephens
 	
 	Created:		February 13, 2013
 	Last Edited:	February 13, 2013
 	
 	Coordinates the scrolling of the background.
*/


using UnityEngine;
using System.Collections;


public class BackgroundController : MonoBehaviour 
{
	public bool scrollAtStart = false;
	public float scrollMultiplier = 2.0f;
	private float offset = 0.0f;
	private Renderer rend;
	private bool isScrolling = false;
	
	#region Initialization
	
	// Used for initialization
	// Called automatically at start
	void Start () 
	{
		if (scrollAtStart) SetIsScrolling (true);
		AssignVariables ();
	}
	
	#endregion
	
	// Update is called once per frame
	void Update () 
	{
		if (isScrolling)
		{
			offset += Time.deltaTime * scrollMultiplier;
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (offset, 0.0f));
		}
	}
	
	
	//
	//
	public void SetIsScrolling (bool b)
	{
		isScrolling = b;
	}
	
	
	#region Utility
	
	// Assigns the local variables
	// Called from Start ()
	private void AssignVariables ()
	{
		rend = renderer;
	}
	
	#endregion
}
