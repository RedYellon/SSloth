using UnityEngine;
using System.Collections;

public static class CameraExtensions {

	public static Vector3 ViewportToWorldPoint(this Camera camera, Vector2 point) {
		return camera.ViewportToWorldPoint(point.WithZ());
	}

	public static Vector3 ViewportToScreenPoint(this Camera camera, Vector2 point) {
		return camera.ViewportToScreenPoint(point.WithZ());
	}

	public static Ray ViewportPointToRay(this Camera camera, Vector2 point) {
		return camera.ViewportPointToRay(point.WithZ());
	}

	public static Vector3 ScreenToViewportPoint(this Camera camera, Vector2 point) {
		return camera.ScreenToViewportPoint(point.WithZ());
	}

	public static Vector3 ScreenToWorldPoint(this Camera camera, Vector2 point) {
		return camera.ScreenToWorldPoint(point.WithZ());
	}

	public static Ray ScreenPointToRay(this Camera camera, Vector2 point) {
		return camera.ScreenPointToRay(point.WithZ());
	}

	public static float ViewportToWorldX(this Camera camera, float viewportX) {
		return camera.ViewportToWorldPoint(Vector3.right * viewportX).x;
	}

	public static float ViewportToWorldY(this Camera camera, float viewportY) {
		return camera.ViewportToWorldPoint(Vector3.up * viewportY).y;
	}
	
	public static float ViewportToWorldZ(this Camera camera, float viewportZ) {
		return camera.ViewportToWorldPoint(Vector3.forward * viewportZ).z;
	}

	public static float WorldToViewportX(this Camera camera, float worldX) {
		return camera.WorldToViewportPoint(Vector3.right * worldX).x;
	}
	
	public static float WorldToViewportY(this Camera camera, float worldY) {
		return camera.WorldToViewportPoint(Vector3.up * worldY).y;
	}
	
	public static float WorldToViewportZ(this Camera camera, float worldZ) {
		return camera.WorldToViewportPoint(Vector3.forward * worldZ).z;
	}
	
	public static float ScreenToWorldX(this Camera camera, float screenX) {
		return camera.ScreenToWorldPoint(Vector3.right * screenX).x;
	}

	public static float ScreenToWorldY(this Camera camera, float screenY) {
		return camera.ScreenToWorldPoint(Vector3.up * screenY).y;
	}
	
	public static float ScreenToWorldZ(this Camera camera, float screenZ) {
		return camera.ScreenToWorldPoint(Vector3.forward * screenZ).z;
	}

	public static float WorldToScreenX(this Camera camera, float worldX) {
		return camera.WorldToScreenPoint(Vector3.right * worldX).x;
	}
	
	public static float WorldToScreenY(this Camera camera, float worldY) {
		return camera.WorldToScreenPoint(Vector3.up * worldY).y;
	}
	
	public static float WorldToScreenZ(this Camera camera, float worldZ) {
		return camera.WorldToScreenPoint(Vector3.forward * worldZ).z;
	}
	
}
