using UnityEngine;
using System.Collections;

public static class RectExtensions {
	
	public static Rect ReplaceX(this Rect rect, float replacement) {
		return new Rect(replacement, rect.y, rect.width, rect.height);
	}

	public static Rect ReplaceY(this Rect rect, float replacement) {
		return new Rect(rect.x, replacement, rect.width, rect.height);
	}

	public static Rect ReplaceWidth(this Rect rect, float replacement) {
		return new Rect(rect.x, rect.y, replacement, rect.height);
	}

	public static Rect ReplaceHeight(this Rect rect, float replacement) {
		return new Rect(rect.x, rect.y, rect.width, replacement);
	}

	public static Rect ReplaceOrigin(this Rect rect, Vector2 replacement) {
		return new Rect(replacement.x, replacement.y, rect.width, rect.height);
	}

	public static Rect ReplaceExtent(this Rect rect, Vector2 replacement) {
		return new Rect(rect.x, rect.y, replacement.x, replacement.y);
	}
}
