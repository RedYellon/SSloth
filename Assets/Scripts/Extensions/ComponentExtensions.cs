using UnityEngine;
using System.Collections;

public static class ComponentExtensions {

	public static bool IsComponentOf(this Component component, GameObject gameObject) {
		return component.gameObject == gameObject;
	}

	public static bool IsComponentOfDescendentOf(this Component component, GameObject gameObject) {
		return component.transform.IsDescendentOf(gameObject.transform);
	}
}
