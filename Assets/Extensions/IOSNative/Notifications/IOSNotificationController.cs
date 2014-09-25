//#define PUSH_ENABLED
////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
using System.Runtime.InteropServices;
#endif

public class IOSNotificationController : ISN_Singleton<IOSNotificationController>
{


	private static IOSNotificationController _instance;


	//Events
	public const string DEVICE_TOKEN_RECEIVED = "device_token_received";
	public const string REMOTE_NOTIFICATION_RECEIVED = "remote_notification_received";

	//Actions
	public Action<IOSNotificationDeviceToken> OnDeviceTokenReceived = delegate {};
	#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE
	public Action<RemoteNotification> OnRemoteNotificationReceived = delegate {};
	#endif



	private const string PP_ID_KEY = "IOSNotificationControllerKey_ID";

	#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
	[DllImport ("__Internal")]
	private static extern void _ISN_ScheduleNotification (int time, string message, bool sound, string nId, int badges);
	
	[DllImport ("__Internal")]
	private static extern  void _ISN_ShowNotificationBanner (string title, string messgae);
	
	[DllImport ("__Internal")]
	private static extern void _ISN_CancelNotifications();

	[DllImport ("__Internal")]
	private static extern void _ISN_CancelNotificationById(string nId);

	[DllImport ("__Internal")]
	private static extern  void _ISN_ApplicationIconBadgeNumber (int badges);
	#endif

	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}



	#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE
	void FixedUpdate() {
		if(NotificationServices.remoteNotificationCount > 0) {
			foreach(var rn in NotificationServices.remoteNotifications) {
				Debug.Log("Remote Noti: " + rn.alertBody);
				IOSNotificationController.instance.ShowNotificationBanner("", rn.alertBody);
				dispatch(REMOTE_NOTIFICATION_RECEIVED, rn);
				OnRemoteNotificationReceived(rn);
			}
			NotificationServices.ClearRemoteNotifications();
		}
	}
	#endif





	public void RegisterForRemoteNotifications(RemoteNotificationType notificationTypes) {
		#if (UNITY_IPHONE && !UNITY_EDITOR && PUSH_ENABLED) || SA_DEBUG_MODE
		
		NotificationServices.RegisterForRemoteNotificationTypes(notificationTypes);
		DeviceTokenListner.Create ();

		#endif
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public void ShowNotificationBanner (string title, string messgae) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_ShowNotificationBanner (title, messgae);
		#endif
	}

	[System.Obsolete("CancelNotifications is deprecated, please use CancelAllLocalNotifications instead.")]
	public void CancelNotifications () {
		CancelAllLocalNotifications();
	}


	public void CancelAllLocalNotifications () {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_CancelNotifications();
		#endif
	}

	public void CancelLocalNotificationById (int notificationId) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_CancelNotificationById(notificationId.ToString());
		#endif
	}

	public int ScheduleNotification (int time, string message, bool sound, int badges = 0) {
		int nid = GetNextId;

		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
		string notificationId = nid.ToString();
		_ISN_ScheduleNotification (time, message, sound, notificationId, badges);
		#endif

		return nid;
	}

	public void ApplicationIconBadgeNumber (int badges) {
		#if (UNITY_IPHONE && !UNITY_EDITOR) || SA_DEBUG_MODE
			_ISN_ApplicationIconBadgeNumber (badges);
		#endif

	}

	
	


	


	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------


	private int GetNextId {
		get {
			int id = 1;
			if(PlayerPrefs.HasKey(PP_ID_KEY)) {
				id = PlayerPrefs.GetInt(PP_ID_KEY);
				id++;
			} 
			
			PlayerPrefs.SetInt(PP_ID_KEY, id);
			return id;
		}
		
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	public void OnDeviceTockeReceivedAction (IOSNotificationDeviceToken token) {
		dispatch (DEVICE_TOKEN_RECEIVED, token);
		OnDeviceTokenReceived(token);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------




	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
