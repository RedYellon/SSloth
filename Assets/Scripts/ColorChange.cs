using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		//
		public float changeRate = 10.0f;
		
		#endregion
		
		#region Scripts
		
		//
		AudioController audioCont;
		
		#endregion
		
		#region Private
		
		// The sprite of this object
		private tk2dSprite sprite;
		//
		private Color targetColor;
		//
		private bool isChanging = false;
		
		#endregion
	
	#endregion
	
	
	// Use this for initialization
	void Start () 
	{
		AssignVariables ();
	}
	
	
	public void ChangeColors (Color c)
	{
		// Set the target color
		targetColor = c;
		
		//
		audioCont.PlaySound ("Color Bar");
		isChanging = true;
		Invoke ("EndColorChange", 5.0f);
		StartCoroutine ("ColorShift");
	}
	
	
	//
	//
	IEnumerator ColorShift ()
	{
		// 
		sprite.color = Color.white;
		while (isChanging)
		{
			sprite.color = Color.Lerp (sprite.color, targetColor, Time.deltaTime * changeRate);
			yield return null;
		}
	}
	
	
	//
	//
	void EndColorChange ()
	{
		isChanging = false;
		sprite.color = targetColor;
	}
	
	
	//
	//
	void AssignVariables ()
	{
		sprite = GetComponent <tk2dSprite> ();
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
	}
}
