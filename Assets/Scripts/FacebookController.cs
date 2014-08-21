using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public sealed class FacebookController : MonoBehaviour 
{
	
	private const string FACEBOOK_APP_ID = "656397034456972";
	private const string FACEBOOK_URL = "http://www.facebook.com/dialog/feed";
	
	
	
	public void ShareToFacebook (int score)
	{
		string bstring = "I just scored " + score + " in #supersloth!";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			bstring = "I just scored " + score + " in Super Sloth for iOS!";
		else if (Application.platform == RuntimePlatform.Android)
			bstring = "I just scored " + score + " in Super Sloth for Android!";
		
		Application.OpenURL (FACEBOOK_URL + "?app_id=" + FACEBOOK_APP_ID +
		                     "&link=" + WWW.EscapeURL("http://www.superslothgame.com") +
		                     "&name=" + WWW.EscapeURL(bstring) +
		                     "&caption=" + WWW.EscapeURL("I am the sloth master") + 
		                     "&description=" + WWW.EscapeURL("I will name my \nfirstborn sloth after the first person who can get a higher score than me.") + 
		                     "&picture=" + WWW.EscapeURL("http://www.superslothgame.com/uploads/1/2/8/1/12813510/2555159.png?235") + 
		                     "&redirect_uri=" + WWW.EscapeURL("http://www.facebook.com/"));
	}
	
	
	
	//#region Initialization
	/*
	// Use this for initialization
	void Start () 
	{
		CallFBInit ();
		CallFBLogin ();
	}
	
	private bool isInit = false;
	
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		isInit = true;
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	#endregion
	
	#region Login
	
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", LoginCallback);
	}
	
	void LoginCallback(FBResult result)
	{
		//if (result.Error != null)
		//	lastResponse = "Error Response:\n" + result.Error;
		//else if (!FB.IsLoggedIn)
		//{
			//lastResponse = "Login cancelled by Player";
		//}
		//else
		//{
			//lastResponse = "Login was successful!";
		//}
	}
	
	private void CallFBLogout()
	{
		FB.Logout();
	}
	
	#endregion
	*/
	public void Tweet (int s)
	{
		string st = "I just scored " + s.ToString () + " in #supersloth!  @SuperSlothGame";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			st = "I just scored " + s.ToString () + " in #supersloth for iOS!  @SuperSlothGame";
		else if (Application.platform == RuntimePlatform.Android) 
			st = "I just scored " + s.ToString () + " in #supersloth for Android!  @SuperSlothGame";
		 
		Application.OpenURL ("http://twitter.com/intent/tweet" + "?text=" + WWW.EscapeURL (st) + "&amp;lang=" + WWW.EscapeURL ("en"));
	}
	/*
	public void FBBrag (int score)                                                                                                 
	{                        
		//
		if (!FB.IsLoggedIn)
		{
			CallFBLogin ();
			//return;
		}
		            
		string bstring = "I just scored " + score + " in #supersloth!";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			bstring = "I just scored " + score + " in Super Sloth for iOS!";
		else if (Application.platform == RuntimePlatform.Android)
			bstring = "I just scored " + score + " in Super Sloth for Android!";
		                                                                                  
		FB.Feed(                                                                                                                 
		        linkCaption: bstring,               
		        picture: "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-xpa1/t1.0-1/p160x160/1604399_272908212873362_66469287_n.png",                                                     
		        linkName: "Look at how slothy I am right now!",                                                                 
		        link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")       
		        );                                                                                                               
	}  */
}
