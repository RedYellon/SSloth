using UnityEngine;
using System.Collections;

public static class TransformExtensions {
	
	public static Vector3 LocalToWorldPoint(this Transform transform, Vector3 point) {
		return transform.TransformPoint(point);
	}

	public static Vector3 WorldToLocalPoint(this Transform transform, Vector3 point) {
		return transform.InverseTransformPoint(point);
	}

	public static Vector3 ToWorldPoint(this Vector3 point, Transform transform) {
		return transform.TransformPoint(point);
	}

	public static Vector3 ToLocalPoint(this Vector3 point, Transform transform) {
		return transform.InverseTransformPoint(point);
	}

	public static Vector3 LocalToWorldDirection(this Transform transform, Vector3 direction) {
		return transform.TransformDirection(direction);
	}

	public static Vector3 WorldToLocalDirection(this Transform transform, Vector3 direction) {
		return transform.InverseTransformDirection(direction);
	}

	public static Vector3 ToWorldDirection(this Vector3 direction, Transform transform) {
		return transform.TransformDirection(direction);
	}

	public static Vector3 ToLocalDirection(this Vector3 direction, Transform transform) {
		return transform.InverseTransformDirection(direction);
	}
	
	public static Vector2 PositionToScreenPoint(this Transform transform, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return camera.WorldToScreenPoint(transform.position);
	}
	
	public static Vector2 PositionToViewportPoint(this Transform transform, Camera camera = null) {
		if (camera == null) camera = Camera.main;
		return camera.WorldToViewportPoint(transform.position);
	}

	public static bool IsDescendentOf(this Transform transform, Transform potentialAncestor) {
		Transform ancestor = transform.parent;
		while (ancestor != null) {
			if (ancestor == potentialAncestor) return true;
			ancestor = ancestor.parent;
		}
		return false;
	}
	
	public static bool IsAncestorOf(this Transform transform, Transform potentialDescendent) {
		return potentialDescendent.IsDescendentOf(transform);
	}

	public static void TranslateLocalToWorldPoint(this Transform transform, Vector3 localPoint, Vector3 worldPoint) {
		transform.Translate(localPoint.ToWorldPoint(transform).DirectionTo(worldPoint), Space.World);
	}
}
