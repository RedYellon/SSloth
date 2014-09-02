using UnityEngine;
using System.Collections;

public static class VectorExtensions {
	
	public static Vector2 Lerp(this float value, Vector2 from, Vector2 to) {
		return Vector2.Lerp(from, to, value);
	}
	
	public static Vector3 Lerp(this float value, Vector3 from, Vector3 to) {
		return Vector3.Lerp(from, to, value);
	}

	public static float DistanceTo(this Vector3 vector, Vector3 other) {
		return Vector3.Distance(vector, other);
	}

	public static Vector2 DirectionTo(this Vector2 vector, Vector2 other) {
		return other - vector;
	}

	public static Vector3 DirectionTo(this Vector3 vector, Vector3 other) {
		return other - vector;
	}

	public static Vector2 WithoutX(this Vector3 vector) {
		return new Vector2(vector.y, vector.z);
	}
	
	public static Vector2 WithoutY(this Vector3 vector) {
		return new Vector2(vector.x, vector.z);
	}
	
	public static Vector2 WithoutZ(this Vector3 vector) {
		return new Vector2(vector.x, vector.y);
	}

	public static Vector3 WithX(this Vector2 vector, float insert = 0F) {
		return new Vector3(insert, vector.x, vector.y);
	}

	public static Vector3 WithY(this Vector2 vector, float insert = 0F) {
		return new Vector3(vector.x, insert, vector.y);
	}

	public static Vector3 WithZ(this Vector2 vector, float insert = 0F) {
		return new Vector3(vector.x, vector.y, insert);
	}
	
	public static Vector2 ReplaceX(this Vector2 vector, float replacement) {
		return new Vector3(replacement, vector.y);
	}

	public static Vector2 ReplaceY(this Vector2 vector, float replacement) {
		return new Vector2(vector.x, replacement);
	}

	public static Vector3 ReplaceX(this Vector3 vector, float replacement) {
		return new Vector3(replacement, vector.y, vector.z);
	}

	public static Vector3 ReplaceY(this Vector3 vector, float replacement) {
		return new Vector3(vector.x, replacement, vector.z);
	}

	public static Vector3 ReplaceZ(this Vector3 vector, float replacement) {
		return new Vector3(vector.x, vector.y, replacement);
	}

	public static Vector2 ScreenToGUIPoint(this Vector2 point, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return new Vector2(point.x, camera.pixelHeight - point.y);
	}

	/*public static Vector2 ViewportToGUIPoint(this Vector2 point, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return camera.ViewportToScreenPoint(point).ScreenToGUIPoint(camera);
	}*/

	public static Vector2 GUIToScreenPoint(this Vector2 point, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return new Vector2(point.x, camera.pixelHeight - point.y);
	}

	public static Vector2 GUIToViewportPoint(this Vector2 point, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return camera.ScreenToViewportPoint(point.GUIToScreenPoint(camera));
	}

	public static float TriangulateEulerAngle(this Vector2 coordinates) {
		float angle = Mathf.Atan(coordinates.x / coordinates.y) * Mathf.Rad2Deg;
		float sinSign = Mathf.Sign(coordinates.x / coordinates.magnitude);
		float cosSign = Mathf.Sign(coordinates.y / coordinates.magnitude);
		if (cosSign == -1)
			angle += 180;
		else if (sinSign == -1)
			angle += 360;
		return angle;
	}
	
	public static float TriangulateEulerAngle(this Vector3 vertex, Vector3 start, Vector3 end) {
		start = start - vertex;
		end = end - vertex;
		float startAngle = start.WithoutZ().TriangulateEulerAngle();
		float endAngle = end.WithoutZ().TriangulateEulerAngle();
		return startAngle.EulerAngleTo(endAngle);
	}
}
