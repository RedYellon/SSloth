/*
 	FigureEightMovement.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Code taken from user Tinus at:
 	http://forum.unity3d.com/threads/making-an-object-move-in-a-figure-8-programatically.38007/
 	
 	Created:		June 4, 2014
 	Last Edited:	June 6, 2014
 	
 	Causes the attached gameObject to move in a figure-8 fashion.
*/


using UnityEngine;
using System.Collections;


public class FigureEightMovement : MonoBehaviour 
{
	#region Variables
	
		#region Public
	
		// The speed that the object moves around the figure-8
		public float m_Speed = 1;
		// The X scale of movement
		public float m_XScale = 1;
		// The Y scale of movement
		public float m_YScale = 1;
	
		#endregion
		
		#region Private
	
		// The values use for calculating the figure-8 movement
		private Vector3 m_Pivot;
		private Vector3 m_PivotOffset;
		private float m_Phase;
		private bool m_Invert = false;
		private float m_2PI = Mathf.PI * 2;
		// The cached transform of this object
		private Transform trans;
	
		#endregion
	
	#endregion
	
	
	#region Update
	
	// Main logic loop
	// Called automatically every frame
	void Update () 
	{
		// Update the pivot point and offset
		m_Pivot = trans.parent.position;
		m_PivotOffset = Vector3.up * 2 * m_YScale;
		
		// Update movement math 
		m_Phase += m_Speed * Time.deltaTime;
		if(m_Phase > m_2PI)
		{
			m_Invert = !m_Invert;
			m_Phase -= m_2PI;
		}
		if(m_Phase < 0) m_Phase += m_2PI;
		
		// Move object
		trans.position = m_Pivot + (m_Invert ? m_PivotOffset : Vector3.zero);
		trans.position = new Vector3 (trans.position.x + Mathf.Sin(m_Phase) * m_XScale, trans.position.y + Mathf.Cos(m_Phase) * (m_Invert ? -1 : 1) * m_YScale, trans.position.z);
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
		trans = transform;
		m_Pivot = trans.parent.position;
	}
	
	#endregion
}
