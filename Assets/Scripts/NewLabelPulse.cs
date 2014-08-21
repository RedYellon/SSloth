using UnityEngine;
using System.Collections;

public class NewLabelPulse : MonoBehaviour 
{
	public float speed = 5.0f;
	public float smallSize = 0;
	public float bigSize = 0;
	Transform t;
	bool isGrowing = true;
	
	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isGrowing) Grow ();
		else Shrink ();
	}
	
	void Grow ()
	{
		if (t.localScale.x < bigSize - 0.01f)
		{
			t.localScale = Vector3.Lerp (t.localScale, new Vector3 (bigSize, bigSize, bigSize), Time.deltaTime * speed);
		}
		else
			isGrowing = false;
	}
	
	void Shrink ()
	{
		if (t.localScale.x > smallSize + 0.01f)
		{
			t.localScale = Vector3.Lerp (t.localScale, new Vector3 (smallSize, smallSize, smallSize), Time.deltaTime * speed);
		}
		else
			isGrowing = true;
	}
	
	
	
}
