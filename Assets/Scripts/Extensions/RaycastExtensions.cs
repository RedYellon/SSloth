using UnityEngine;
using System.Collections;

public static class RaycastHitExtensions {
	
	public static bool Contains(this RaycastHit[] hits, Collider collider) {
		foreach (var hit in hits) if (hit.collider == collider) return true;
		return false;
	}

	public static bool Cast(this Ray ray, out RaycastHit hit, float distance = Mathf.Infinity, int layerMask = ~(1 << 2)) {
		return Physics.Raycast(ray, out hit, distance, layerMask);
	}
	
	public static bool Cast(this Ray ray, Collider collider, out RaycastHit hit, float distance = Mathf.Infinity) {
		return collider.Raycast(ray, out hit, distance);
	}
	
	public static Vector3 Cast(this Ray ray, Plane plane) {
		float distance;
		plane.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	public static RaycastHit[] CastAll(this Ray ray, float distance = Mathf.Infinity, int layerMask = ~(1 << 2)) {
		return Physics.RaycastAll(ray, distance, layerMask);
	}
}
