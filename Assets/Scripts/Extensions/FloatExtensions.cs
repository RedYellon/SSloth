using UnityEngine;
using System.Collections;

public static class FloatExtensions {

	public static float Smooth(this float value, float from = 0F, float to = 1F) {
		return Mathf.SmoothStep(from, to, value);
	}

	public static float Clamp(this float value, float from = 0F, float to = 1F) {
		return Mathf.Clamp(value, from, to);
	}
	
	public static float Lerp(this float value, float from = 0F, float to = 1F) {
		return Mathf.Lerp(from, to, value);
	}

	public static float InverseLerp(this float value, float from = 0F, float to = 1F) {
		return Mathf.InverseLerp(from, to, value);
	}

	public static float Wrap(this float value, float to) {
		if (to > 0F) {
			return value.Wrap(0F, to);
		} else {
			return value.Wrap(to, 0F);
		}
	}

	public static float Wrap(this float value, float min, float max) {
		float range = max - min;
		if (range == 0) return min;

		if (value > max) {
			return min + (value - max) % range;
		} else if (value < min) {
			return max + (value - min) % range;
		}

		return value;
	}

	public static float Floor(this float value) {
		return Mathf.Floor(value);
	}

	public static float Ceil(this float value) {
		return Mathf.Ceil(value);
	}

	public static float Round(this float value) {
		return Mathf.Round(value);
	}

	public static float RaisedTo(this float value, float exponent) {
		return Mathf.Pow(value, exponent);
	}

	public static float Fraction(this float value) {
		return value % 1F;
	}

	public static float EulerAngleTo(this float value, float other) {
		float angle = other - value;
		
		if (angle > 180F) {
			angle -= 360F;
		} else if (angle < -180F) {
			angle += 360F;
		}

		return angle;
	}

	public static int[] GetDigits(this float value) {
		var digits = new int[(int)Mathf.Log10(value) + 1];
		for (int i = 0; i < digits.Length; i++) {
			digits[digits.Length - (i + 1)] = (int)(value / Mathf.Pow(10F, i) % 10F);
		}
		return digits;
	}
}
