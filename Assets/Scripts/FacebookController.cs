using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public sealed class FacebookController : MonoBehaviour 
{
	
	#region Facebook
	
	private static bool IsUserInfoLoaded = false;
	private static bool IsFrindsInfoLoaded = false;
	private static bool IsAuntifivated = false;
	
	#endregion
	
	
	//
	//
	public void ShareToFacebook (int score)
	{
		//string bstring = "I just scored " + score + " in #supersloth!";
		/*if (Application.platform == RuntimePlatform.IPhonePlayer)
			bstring = "I just scored " + score + " in Super Sloth for iOS!";
		else if (Application.platform == RuntimePlatform.Android)
			bstring = "I just scored " + score + " in Super Sloth for Android!";
		
		Application.OpenURL (FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID +
		                     "&link=" + WWW.EscapeURL("http://www.superslothgame.com") +
		                     "&name=" + WWW.EscapeURL(bstring) +
		                     "&caption=" + WWW.EscapeURL("I am the sloth master") + 
		                     "&description=" + WWW.EscapeURL("I will name my \nfirstborn sloth after the first person who can get a higher score than me.") + 
		                     "&picture=" + WWW.EscapeURL("http://www.superslothgame.com/uploads/1/2/8/1/12813510/2555159.png?235") + 
		                     "&redirect_uri=" + WWW.EscapeURL("http://www.facebook.com/"));*/
		
		Texture2D tex = GetScreenshot ();
		SPShareUtility.FacebookShare ("I just scored " + score + " in #supersloth!", tex);
		Destroy (tex);
	}
	
	
	//
	//
	public void Tweet (int score)
	{
		string displaystring = "Hey Twitter! I just scored " + score.ToString () + " in #supersloth!  @SuperSlothGame";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			displaystring = "Hey Twitter! I just scored " + score.ToString () + " in #supersloth for iOS!  @SuperSlothGame";
		else if (Application.platform == RuntimePlatform.Android) 
			displaystring = "Hey Twitter! I just scored " + score.ToString () + " in #supersloth for Android!  @SuperSlothGame";
		 
		
		Texture2D tex = GetScreenshot ();
		SPShareUtility.TwitterShare (displaystring, tex);
		Destroy (tex);
	}
	
	
	// Returns a texture screenshot
	Texture2D GetScreenshot ()
	{
		//
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();
		
		// Return the texture
		return tex;
	}
	
}
