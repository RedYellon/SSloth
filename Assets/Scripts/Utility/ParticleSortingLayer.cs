/*
 	ParticleSortingLayer.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 23, 2014
 	Last Edited:	November 23, 2014
 	
	Utility script to set sorting layer of a particle system.
*/


using UnityEngine;
using System.Collections;


public class ParticleSortingLayer : MonoBehaviour 
{
	#region Variables

	// The sorting layer we want this particle system to belong to
	public string initialSortingLayerName = "Default";

	#endregion


	// Use this for initialization
	void Start () 
	{
		particleSystem.renderer.sortingLayerName = initialSortingLayerName;
	}
}
