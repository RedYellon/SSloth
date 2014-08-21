using UnityEngine;
using System.Collections;

public class BounceObject : MonoBehaviour 
{
	public float bounceSpeed = 1.0f;
	public float bounceAmount = 2.0f;
	public float bounceDecay = 0.3f;
	public int bounceTimes = 2;
	private float actualBounceAmount;
	AudioController audioCont;
	
	
	void Start ()
	{
		audioCont = GameObject.Find ("&MainController").GetComponent <AudioController> ();
	}
	
	
	//
	public void Bounce () 
	{
		StartCoroutine ("BounceEnum");
	}
	
	
	//
	//
	public void StopBounce ()
	{
		StopCoroutine ("BounceEnum");
	}
	
	
	//
	IEnumerator BounceEnum ()
	{
		actualBounceAmount = bounceAmount;
		for (int i = 0; i < bounceTimes; i++)
		{
			//audioCont.PlaySound ("CloudBounce");
			float t = 0.0f;
			while (t < 1.0f) 
			{
				t += Time.deltaTime * bounceSpeed;
				transform.position = new Vector3 (transform.position.x, Bounce(t) * actualBounceAmount, transform.position.z);
				yield return null;
			}
			actualBounceAmount *= bounceDecay;
			
		}
		transform.position = Vector3.zero;
	}
	
	float Bounce (float t) 
	{
		return Mathf.Sin (Mathf.Clamp01 (t) * Mathf.PI);
	}
}
