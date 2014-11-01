
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GoogleMobileAdSettings))]
public class GoogleMobileAdSettingsEditor : Editor {


	
	
	GUIContent IOS_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "IOS Banners Ad Unit Id ");
	GUIContent IOS_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "IOS Interstitials Ad Unit Id");

	GUIContent Android_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "Android Banners Ad Unit Id ");
	GUIContent Android_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "Android Interstitials Ad Unit Id");

	GUIContent WP8_UnitAdId  	 = new GUIContent("Banners Ad Unit Id [?]:",  "WP8 Banners Ad Unit Id ");
	GUIContent WP8_InterstAdId   = new GUIContent("Interstitials Ad Unit Id [?]:", "WP8 Interstitials Ad Unit Id");

	
	GUIContent SdkVersion   = new GUIContent("Plugin Version [?]", "This is Plugin version.  If you have problems or compliments please include this so we know exactly what version to look out for.");
	GUIContent SupportEmail = new GUIContent("Support [?]", "If you have any technical quastion, feel free to drop an e-mail");

	GUIContent deviceNameLabel = new GUIContent("Device Name [?]:", "Name of your device. Just for you");
	GUIContent deviceIdLabel = new GUIContent("Device ID [?]:", "ID of your device. You can get ot from console log");


	private GoogleMobileAdSettings settings;







	private const string version_info_file = "Plugins/StansAssets/Versions/GMA_VersionInfo.txt"; 


	public override void OnInspectorGUI() {


		#if UNITY_WEBPLAYER
		EditorGUILayout.HelpBox("Editing Google Mobile Ad Settings not avaliable with web player platfrom. Please swith to any other platfrom under Build Seting menu", MessageType.Warning);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		if(GUILayout.Button("Switch To WP8",  GUILayout.Width(120))) {
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.WP8Player);
		}

		if(GUILayout.Button("Switch To Android",  GUILayout.Width(120))) {
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
		}

		if(GUILayout.Button("Switch To IOS",  GUILayout.Width(120))) {
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iPhone);
		}
		EditorGUILayout.EndHorizontal();
		
		return;
		
		#endif

		settings = target as GoogleMobileAdSettings;

		GUI.changed = false;



		GeneralOptions();
		EditorGUILayout.Space();
		MainSettings();
		EditorGUILayout.Space();
		TestDevices();
		EditorGUILayout.Space();
		AboutGUI();
	

		if(GUI.changed) {
			DirtyEditor();
		}
	}



	public static bool IsInstalled {
		get {
			if(FileStaticAPI.IsFileExists(PluginsInstalationUtil.ANDROID_DESTANATION_PATH + "androidnative.jar") && FileStaticAPI.IsFileExists(PluginsInstalationUtil.IOS_DESTANATION_PATH + "GoogleMobileAdBanner.h")) {
				return true;
			} else {
				return false;
			}
		}
	}
	
	public static bool IsUpToDate {
		get {
			if(GoogleMobileAdSettings.VERSION_NUMBER.Equals(DataVersion)) {
				return true;
			} else {
				return false;
			}
		}
	}
	
	
	public static string DataVersion {
		get {
			if(FileStaticAPI.IsFileExists(version_info_file)) {
				return FileStaticAPI.Read(version_info_file);
			} else {
				return "Unknown";
			}
		}
	}


	
	public static void UpdateVersionInfo() {
		FileStaticAPI.Write(version_info_file, GoogleMobileAdSettings.VERSION_NUMBER);

	
	}



	private void GeneralOptions() {
		
		if(!IsInstalled) {
			EditorGUILayout.HelpBox("Install Required ", MessageType.Error);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			Color c = GUI.color;
			GUI.color = Color.cyan;
			if(GUILayout.Button("Install Plugin",  GUILayout.Width(120))) {
				PluginsInstalationUtil.Android_InstallPlugin();
				PluginsInstalationUtil.IOS_InstallPlugin();
				UpdateVersionInfo();
			}
			GUI.color = c;
			EditorGUILayout.EndHorizontal();
		}
		
		if(IsInstalled) {
			if(!IsUpToDate) {
				EditorGUILayout.HelpBox("Update Required \nResources version: " + DataVersion + " Plugin version: " + GoogleMobileAdSettings.VERSION_NUMBER, MessageType.Warning);
				
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				Color c = GUI.color;
				GUI.color = Color.cyan;
				if(GUILayout.Button("Update to " + GoogleMobileAdSettings.VERSION_NUMBER,  GUILayout.Width(250))) {
					PluginsInstalationUtil.Android_UpdatePlugin();
					PluginsInstalationUtil.IOS_UpdatePlugin();
					UpdateVersionInfo();
				}
				
				GUI.color = c;
				EditorGUILayout.Space();
				EditorGUILayout.EndHorizontal();
				
			} else {
				EditorGUILayout.HelpBox("Google Mobile Ad Plugin v" + GoogleMobileAdSettings.VERSION_NUMBER + " is installed", MessageType.Info);
				
			}
		}
		
		
		EditorGUILayout.Space();
		
	}

	public void TestDevices() {
		settings.IsTestSettinsOpened = EditorGUILayout.Foldout(settings.IsTestSettinsOpened, "Test Devices");
		if(settings.IsTestSettinsOpened) {

			if(GoogleMobileAdSettings.Instance.testDevices.Count == 0) {
				EditorGUILayout.HelpBox("No Test Devices Registred so far", MessageType.Info);
			}
			foreach(GADTestDevice device in GoogleMobileAdSettings.Instance.testDevices) {
				
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginVertical (GUI.skin.box);
				device.IsOpen = EditorGUILayout.Foldout(device.IsOpen, device.Name);
				if(device.IsOpen) {
					EditorGUI.indentLevel = 2;
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(deviceNameLabel);
					device.Name	 	= EditorGUILayout.TextField(device.Name);
					EditorGUILayout.EndHorizontal();
					
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(deviceIdLabel);
					device.ID	 	= EditorGUILayout.TextField(device.ID);
					if(device.ID.Length > 0) {
						device.ID = device.ID.Trim();
					}
					EditorGUILayout.EndHorizontal();
					
					
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.Space();
					

					
					if(GUILayout.Button("Remove",  GUILayout.Width(80))) {
						GoogleMobileAdSettings.Instance.RemoveDevice(device);
						return;
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();
					
				}
				
				EditorGUILayout.EndVertical ();
				
			}

			EditorGUI.indentLevel = 0;
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.Space();
			if(GUILayout.Button("Register New Device",  GUILayout.Width(135))) {
				GoogleMobileAdSettings.Instance.AddDevice(new GADTestDevice());
			}
			
			EditorGUILayout.EndHorizontal();
		}
	}

	public void MainSettings() {

		EditorGUILayout.HelpBox("Google Mobile Ad Plugin", MessageType.None);
		EditorGUILayout.Space();

		settings.IsIOSSettinsOpened = EditorGUILayout.Foldout(settings.IsIOSSettinsOpened, "IOS");
		if(settings.IsIOSSettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(IOS_UnitAdId);
			settings.IOS_BannersUnitId	 	= EditorGUILayout.TextField(settings.IOS_BannersUnitId);
			if(settings.IOS_BannersUnitId.Length > 0) {
				settings.IOS_BannersUnitId		= settings.IOS_BannersUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(IOS_InterstAdId);
			settings.IOS_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.IOS_InterstisialsUnitId);
			if(settings.IOS_InterstisialsUnitId.Length > 0) {
				settings.IOS_InterstisialsUnitId		= settings.IOS_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
		}


		settings.IsAndroidSettinsOpened = EditorGUILayout.Foldout(settings.IsAndroidSettinsOpened, "Android");
		if(settings.IsAndroidSettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Android_UnitAdId);
			settings.Android_BannersUnitId	 	= EditorGUILayout.TextField(settings.Android_BannersUnitId);
			if(settings.Android_BannersUnitId.Length > 0) {
				settings.Android_BannersUnitId		= settings.Android_BannersUnitId.Trim();
			}


			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(Android_InterstAdId);
			settings.Android_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.Android_InterstisialsUnitId);
			if(settings.Android_InterstisialsUnitId.Length > 0) {
				settings.Android_InterstisialsUnitId		= settings.Android_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
		}



		settings.IsWP8SettinsOpened = EditorGUILayout.Foldout(settings.IsWP8SettinsOpened, "WP8");
		if(settings.IsWP8SettinsOpened) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(WP8_UnitAdId);
			settings.WP8_BannersUnitId	 	= EditorGUILayout.TextField(settings.WP8_BannersUnitId);
			if(settings.WP8_BannersUnitId.Length > 0) {
				settings.WP8_BannersUnitId		= settings.WP8_BannersUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(WP8_InterstAdId);
			settings.WP8_InterstisialsUnitId	 	= EditorGUILayout.TextField(settings.WP8_InterstisialsUnitId);
			if(settings.WP8_InterstisialsUnitId.Length > 0) {
				settings.WP8_InterstisialsUnitId		= settings.WP8_InterstisialsUnitId.Trim();
			}

			EditorGUILayout.EndHorizontal();
		}
	}



	private void AboutGUI() {

		EditorGUILayout.HelpBox("Version Info", MessageType.None);
		EditorGUILayout.Space();
		
		SelectableLabelField(SdkVersion, GoogleMobileAdSettings.VERSION_NUMBER);
		SelectableLabelField(SupportEmail, "stans.assets@gmail.com");
		
		
	}
	
	private void SelectableLabelField(GUIContent label, string value) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(label, GUILayout.Width(180), GUILayout.Height(16));
		EditorGUILayout.SelectableLabel(value, GUILayout.Height(16));
		EditorGUILayout.EndHorizontal();
	}



	private static void DirtyEditor() {
		#if UNITY_EDITOR
		EditorUtility.SetDirty(GoogleMobileAdSettings.Instance);
		#endif
	}
	
	
}
