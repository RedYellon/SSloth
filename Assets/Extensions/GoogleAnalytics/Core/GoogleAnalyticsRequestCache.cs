using UnityEngine;
using System.Collections;

public class GoogleAnalyticsRequestCache  {

	private const string DATA_SPLITTER = "|";
	private const string GA_DATA_CACHE_KEY = "GoogleAnalyticsRequestCache";

	public static void SaveRequest(string cache) {
		string data = SavedData;
		if(data != string.Empty) {
			data = data + DATA_SPLITTER + cache;
		} else {
			data = cache;
		}

		SavedData = data;
	}

	public static void SendChashedRequests() {

		string data = SavedData;
		if(data != string.Empty) {
			string[] requests = data.Split(DATA_SPLITTER [0]);
			foreach(string request in requests) {
				GoogleAnalytics.SendSkipCache(request);
			}
			
		} 

		Clear();
	}


	public static void Clear() {
		PlayerPrefs.DeleteKey(GA_DATA_CACHE_KEY);
	}

	public static string SavedData {
		get {
			if(PlayerPrefs.HasKey(GA_DATA_CACHE_KEY)) {
				return PlayerPrefs.GetString(GA_DATA_CACHE_KEY);
			} else {
				return string.Empty;
			}
		}

		set {
			PlayerPrefs.SetString(GA_DATA_CACHE_KEY, value);
		}
	}
}
