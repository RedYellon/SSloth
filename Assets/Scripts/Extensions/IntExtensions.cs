using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class IntExtensions {
	
	static readonly Dictionary<string, string> subscriptDigits = new Dictionary<string, string>() {
		{"0", "\u2080"},
		{"1", "\u2081"},
		{"2", "\u2082"},
		{"3", "\u2083"},
		{"4", "\u2084"},
		{"5", "\u2085"},
		{"6", "\u2086"},
		{"7", "\u2087"},
		{"8", "\u2088"},
		{"9", "\u2089"}
	};

	public static string ToSubscriptString(this int value) {
		string subscriptString = "";
		foreach (var character in value.ToString()) {
			var c = character.ToString();
			if (subscriptDigits.ContainsKey(c)) {
				subscriptString += subscriptDigits[c];
			} else {
				subscriptString += c;
			}
		}
		return subscriptString;
	}
}
