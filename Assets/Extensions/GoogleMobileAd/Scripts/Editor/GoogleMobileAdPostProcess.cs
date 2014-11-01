//#define CODE_DISABLED

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class GoogleMobileAdPostProcess  {


	private const string BUNLDE_KEY = "SA_PP_BUNLDE_KEY";



	[PostProcessBuild(49)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {


		#if UNITY_IPHONE && !CODE_DISABLED
		string StoreKit = "StoreKit.framework";
		if(!ISDSettings.Instance.frameworks.Contains(StoreKit)) {
			ISDSettings.Instance.frameworks.Add(StoreKit);
		}


		string CoreTelephony = "CoreTelephony.framework";
		if(!ISDSettings.Instance.frameworks.Contains(CoreTelephony)) {
			ISDSettings.Instance.frameworks.Add(CoreTelephony);
		}


		string AdSupport = "AdSupport.framework";
		if(!ISDSettings.Instance.frameworks.Contains(AdSupport)) {
			ISDSettings.Instance.frameworks.Add(AdSupport);
		}
	

		string MessageUI = "MessageUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(MessageUI)) {
			ISDSettings.Instance.frameworks.Add(MessageUI);
		}

		string EventKit = "EventKit.framework";
		if(!ISDSettings.Instance.frameworks.Contains(EventKit)) {
			ISDSettings.Instance.frameworks.Add(EventKit);
		}

		string EventKitUI = "EventKitUI.framework";
		if(!ISDSettings.Instance.frameworks.Contains(EventKitUI)) {
			ISDSettings.Instance.frameworks.Add(EventKitUI);
		}

		string linkerFlasg = "-ObjC";
		if(!ISDSettings.Instance.linkFlags.Contains(linkerFlasg)) {
			ISDSettings.Instance.linkFlags.Add(linkerFlasg);
		}
		#endif

		#if UNITY_ANDROID
		string file = PluginsInstalationUtil.ANDROID_DESTANATION_PATH + "AndroidManifest.xml";
		string Manifest = FileStaticAPI.Read(file);
		Manifest = Manifest.Replace("%APP_BUNDLE_ID%", PlayerSettings.bundleIdentifier);
		
		//checking for bundle change
		if(OldBundle != string.Empty) {
			if(OldBundle != PlayerSettings.bundleIdentifier) {
				int result = EditorUtility.DisplayDialogComplex("Google Mobile Ad: bundle id change detected", "Project bundle Identifier changed, do you wnat to replase old bundle: " + OldBundle + "with new one: " + PlayerSettings.bundleIdentifier, "Yes", "No", "Later");
				
				
				switch(result) {
				case 0:
					Manifest = Manifest.Replace(QUOTE +  OldBundle + QUOTE, QUOTE +  PlayerSettings.bundleIdentifier + QUOTE);
					Manifest = Manifest.Replace(QUOTE +  OldBundle + ".fileprovider" + QUOTE, QUOTE +  PlayerSettings.bundleIdentifier + ".fileprovider" + QUOTE);
					OldBundle = PlayerSettings.bundleIdentifier;
					break;
				case 1:
					OldBundle = PlayerSettings.bundleIdentifier;
					break;
					
				}
				
			}
			
			
			
		} else {
			OldBundle = PlayerSettings.bundleIdentifier;
		}
		
		FileStaticAPI.Write(file, Manifest);
		Debug.Log("GMA Post Process Done");
		#endif

	}


	private static string OldBundle {
		get {
			if(EditorPrefs.HasKey(BUNLDE_KEY)) {
				return EditorPrefs.GetString(BUNLDE_KEY);
			} else {
				return string.Empty;
			}
		}
		
		
		set {
			EditorPrefs.SetString(BUNLDE_KEY, value);
		}
	}
	
	private static string QUOTE {
		get {
			return "\"";
		}
	}

}
