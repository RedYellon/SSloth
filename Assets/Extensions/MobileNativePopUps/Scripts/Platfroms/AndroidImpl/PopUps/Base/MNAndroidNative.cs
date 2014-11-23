////////////////////////////////////////////////////////////////////////////////
//  
// @module Common Android Native Lib
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MNAndroidNative {





	
	//--------------------------------------
	//  MESSAGING
	//--------------------------------------


	public static void showDialog(string title, string message) {
		showDialog (title, message, "Yes", "No");
	}

	public static void showDialog(string title, string message, string yes, string no) {
		CallActivityFunction("ShowDialog", title, message, yes, no);
	}


	public static void showMessage(string title, string message) {
		showMessage (title, message, "Ok");
	}


	public static void showMessage(string title, string message, string ok) {
		CallActivityFunction("ShowMessage", title, message, ok);
	}



	public static void showRateDialog(string title, string message, string yes, string laiter, string no) {
		CallActivityFunction("ShowRateDialog", title, message, yes, laiter, no);
	}
	
	public static void ShowPreloader(string title, string message) {
		CallActivityFunction("ShowPreloader",  title, message);
	}
	
	public static void HidePreloader() {
		CallActivityFunction("HidePreloader");
	}


	public static void OpenAppRatingPage(string url) {
		CallActivityFunction("OpenAppRatingPage",  url);
	}



	#if UNITY_ANDROID

	private static AndroidJavaObject _bridge = null;
	private static AndroidJavaObject _activity = null;

	public static AndroidJavaObject bridge {
		get {
			if(_bridge == null) {
				_bridge = new AndroidJavaObject("com.androidnative.popups.PopUpsManager");
			}

			return _bridge;
		}
	}

	public static AndroidJavaObject activity {
		get {
			if(_activity == null) {
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				_activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}

			return _activity;
		}
	}

	#endif


	public static void CallActivityFunction(string methodName, params object[] args) {
		#if UNITY_ANDROID
		if(Application.platform != RuntimePlatform.Android) {
			return;
		}

		Debug.Log("MN: Using proxy for class method:" + methodName);
		
		try {
		
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => { bridge.CallStatic(methodName, args); }));
			
			
		} catch(System.Exception ex) {
			Debug.LogWarning(ex.Message);
		}
		#endif
	}

}
