/*
 	ColorsController.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		November 28, 2014
 	Last Edited:	November 28, 2014
 	
 	Stores colors.
*/


using UnityEngine;
using System.Collections;


public class ColorsController : MonoBehaviour 
{
	#region Colors

	[Header ("Background Normal Colors")]
	public Color backgroundColor;
	public Color stripesColor;

	[Header ("Background Winter Colors")]
	public Color backgroundColorWinter;
	public Color stripesColorWinter;

	[Header ("Stats Background Normal Colors")]
	public Color statsBackgroundColor;
	public Color statsStripesColor;
	
	[Header ("Stats Background Winter Colors")]
	public Color statsBackgroundColorWinter;
	public Color statsStripesColorWinter;

	[Header ("Tinter Normal Colors")]
	public Color tinterDayColor;
	public Color tinterEveningColor;
	public Color tinterNightColor;
	public Color tinterMorningColor;
	
	[Header ("Tinter Winter Colors")]
	public Color tinterDayColorWinter;
	public Color tinterEveningColorWinter;
	public Color tinterNightColorWinter;
	public Color tinterMorningColorWinter;
	
	[Header ("Object Normal Colors")]
	public Color objectDayColor;
	public Color objectEveningColor;
	public Color objectNightColor;
	public Color objectMorningColor;
	
	[Header ("Object Winter Colors")]
	public Color objectDayColorWinter;
	public Color objectEveningColorWinter;
	public Color objectNightColorWinter;
	public Color objectMorningColorWinter;
	
	[Header ("Grass Normal Colors")]
	public Color grassDayColor;
	public Color grassEveningColor;
	public Color grassNightColor;
	public Color grassMorningColor;
	
	[Header ("Grass Winter Colors")]
	public Color grassDayColorWinter;
	public Color grassEveningColorWinter;
	public Color grassNightColorWinter;
	public Color grassMorningColorWinter;

	#endregion
}
