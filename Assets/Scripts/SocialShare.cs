/*
 	SocialShare.cs
 	
 	Michael Stephens
 	www.michaeljohnstephens.com
 	
 	Created:		September 12, 2014
 	Last Edited:	September 12, 2014
 	
 	Controls the social media sharing funtions of the game.
*/


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public sealed class SocialShare : MonoBehaviour 
{
	#region Variables
	
		#region Public
		
		// Twitter photo reqs
		public int twitterWidth = 220;
		public int twitterHeight = 220;
		// Instagram photo reqs
		public int igWidth = 612;
		public int igHeight = 612;
		
		#endregion
	
	// The saved screenshot
	Texture2D screenshot;
	// The saved score
	int playerScore = 0;
	
	#endregion
	
	
	#region Public
	
	// Takes the user to a page where they can rate the app
	//
	public void RateAppPopup ()
	{
		MobileNativeRateUs ratePopUp = new MobileNativeRateUs ("Feeling slothy?", "Rate Super Sloth and support future updates!");
		ratePopUp.SetAppleId ("830746781");
		ratePopUp.SetAndroidAppUrl ("https://play.google.com/store/apps/details?id=com.ougda.supersloth");
		//ratePopUp.addEventListener (BaseEvent.COMPLETE, OnRatePopUpClose);
		
		ratePopUp.Start ();
	}
	
	
	// Shares the player's score to facebook
	// Called from ButtonTouched (string buttonName) in GuiController.cs
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
		                     "&description=" + WWW.EscapeURL("I will name my firstborn sloth after the first person who can get a higher score than me.") + 
		                     "&picture=" + WWW.EscapeURL("http://www.superslothgame.com/uploads/1/2/8/1/12813510/2555159.png?235") + 
		                     "&redirect_uri=" + WWW.EscapeURL("http://www.facebook.com/"));*/
		
		playerScore = score;
		StartCoroutine (GetScreenshot (2));
	}
	
	
	// Tweets the player's score
	// Called from ButtonTouched (string buttonName) in GuiController.cs
	public void Tweet (int score)
	{
		//
		playerScore = score;
		StartCoroutine (GetScreenshot (1));
	}
	
	
	// Posts the picture of the player's score to instagram
	//
	public void PostToInstagram (int score)
	{
		//
		playerScore = score;
		StartCoroutine (GetScreenshot (3));
	}
	
	#endregion
	
	
	#region Post
	
	//
	//
	private void PostTweet ()
	{
		// Extract tweet file text into array of strings
		TextAsset txt = (TextAsset) Resources.Load ("twitterSloth", typeof (TextAsset));
		string content = txt.text;
		List <string> tweetLines = new List <string> (content.Split ('\n'));
		
		// Get a random line and fill in score/platform
		string tweetTemp = tweetLines [UnityEngine.Random.Range (0, tweetLines.Count)];
		if (tweetTemp.Contains ("|score|"))
			tweetTemp = tweetTemp.Replace ("|score|", playerScore.ToString ());
		if (tweetTemp.Contains ("|plat|"))
			tweetTemp = tweetTemp.Replace ("|plat|", GetPlatform ());
		
		// Activate the twitter share popup, fill it in with share text and screenshot
		UM_ShareUtility.TwitterShare (tweetTemp, screenshot);
	}
	
	
	//
	//
	private void PostToFB ()
	{
		// Extract fb file text into array of strings
		TextAsset txt = (TextAsset) Resources.Load ("facebookSloth", typeof (TextAsset));
		string content = txt.text;
		List <string> fbLines = new List <string> (content.Split ('\n'));
		
		// Get a random line and fill in score/platform
		string fbTemp = fbLines [UnityEngine.Random.Range (0, fbLines.Count)];
		if (fbTemp.Contains ("|score|"))
			fbTemp = fbTemp.Replace ("|score|", playerScore.ToString ());
		if (fbTemp.Contains ("|plat|"))
			fbTemp = fbTemp.Replace ("|plat|", GetPlatform ());
		
		// Activate facebook share popup, fill it in with share text and screenshot
		UM_ShareUtility.FacebookShare (fbTemp, screenshot);
	}
	
	
	//
	//
	private void PostToInstagram ()
	{
		// Extract fb file text into array of strings
		TextAsset txt = (TextAsset) Resources.Load ("instagramSloth", typeof (TextAsset));
		string content = txt.text;
		List <string> igLines = new List <string> (content.Split ('\n'));
		
		// Get a random line and fill in score/platform
		string igTemp = igLines [UnityEngine.Random.Range (0, igLines.Count)];
		if (igTemp.Contains ("|score|"))
			igTemp = igTemp.Replace ("|score|", playerScore.ToString ());
		if (igTemp.Contains ("|plat|"))
			igTemp = igTemp.Replace ("|plat|", GetPlatform ());
			
		//
		//UM_ShareUtility.ShareMedia (screenshot, igTemp);
	}
	
	#endregion
	
	
	#region Utility
	
	// Returns a texture screenshot of the current screen
	// Called from ShareToFacebook (int score) and Tweet (int score)
	IEnumerator GetScreenshot (int type)
	{
		// Wait for drawing buffer
		yield return new WaitForEndOfFrame ();
		
		// Create a new blank texture
		int width = 0;
		int height = 0;
		switch (type)
		{
			// Twitter
			case 1:
				width = Screen.width;
				height =  Screen.height;
			break;
			// Facebook
			case 2:
				width = Screen.width;
				height = Screen.height;
			break;
			// Instagram
			case 3:
				width = igWidth;
				height = igHeight;
			break;
		}
		screenshot = new Texture2D (width, height, TextureFormat.RGB24, false);
		screenshot.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		screenshot.Apply ();
		
		//
		switch (type)
		{
			// Twitter
			case 1: PostTweet (); break;
			// Facebook
			case 2: PostToFB (); break;
			// Instagram
			case 3: PostToInstagram (); break;
		}
		
		//
		Destroy (screenshot);
	}
	
	
	// Returns a descriptive string of the current platform
	// Called from Tweet (int score)
	string GetPlatform ()
	{
		string plat = "real";
		switch (Application.platform)
		{
			case RuntimePlatform.IPhonePlayer: plat = "iOS"; break;
			case RuntimePlatform.Android: plat = "Android"; break;
			case RuntimePlatform.WP8Player: plat = "Windows Phone 8"; break;
			case RuntimePlatform.OSXPlayer: plat = "Mac"; break;
			case RuntimePlatform.WindowsPlayer: plat = "Windows"; break;
		}
		return plat;
	}
	
	#endregion
	
	
	#region Facebook Setup
	
	//
	//
	void Awake ()
	{
		// Subscribe to fb events
	//	SPFacebook.instance.addEventListener (FacebookEvents.FACEBOOK_INITED, OnInit);
	//	SPFacebook.instance.addEventListener (FacebookEvents.AUTHENTICATION_SUCCEEDED, OnAuth);
		
		// Initiate FB
	//	SPFacebook.instance.Init ();
	}
	
	
	//
	//
	public void PostToFBDeepLink () 
	{
		// Get the correct link based on platform
		/*string platLink = "http://www.superslothgame.com";
		switch (Application.platform)
		{
			case RuntimePlatform.IPhonePlayer: platLink = "https://itunes.apple.com/us/app/super-sloth/id830746781?mt=8&ign-mpt=uo%3D4"; break;
			case RuntimePlatform.Android: platLink = "https://play.google.com/store/apps/details?id=com.ougda.supersloth"; break;
		}
		
		// Actually post to facebook
		SPFacebook.instance.Post 
		(
			link: platLink,
			linkName: "This is the link name",
			linkCaption: "This is the link caption",
			linkDescription: "This is the link description",
			picture: "http://www.superslothgame.com/uploads/1/2/8/1/12813510/8931332.png"
		);*/
	}
	
	
	private void OnInit () 
	{
		/*if (SPFacebook.instance.IsLoggedIn) 
		{
			OnAuth ();
		} 
		else 
		{
			SPFacebook.instance.Login ();
		}*/
	}
	
	
	private void OnAuth () 
	{
	}
	
	#endregion
	
	
	#region Rating Callbacks
	/*
	//
	//
	private void OnRatePopUpClose (CEvent e) 
	{
		//removing listner
		e.dispatcher.removeEventListener (BaseEvent.COMPLETE, OnRatePopUpClose);
		
		//parsing result
		switch ((MNDialogResult) e.data) 
		{
			case MNDialogResult.RATED:
				//Debug.Log ("Rate Option pickied");
			break;
			case MNDialogResult.REMIND:
				//Debug.Log ("Remind Option pickied");
			break;
			case MNDialogResult.DECLINED:
				//Debug.Log ("Declined Option pickied");
			break;
		}
		
		//string result = e.data.ToString ();
		//new MobileNativeMessage ("Result", result + " button pressed");
	}*/
	
	#endregion
}
