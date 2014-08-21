using UnityEngine;
using System.Collections;

public class TitleGyrate : MonoBehaviour 
{
public bool isGo = false;

	public float maxUpAndDown = 0.03f;       // amount of meters going up and down
	public float speed = 300f;      // up and down speed
	float angle = 0f;       // angle to determin the height by using the sinus
	float toDegrees = Mathf.PI/180;    // radians to degrees
	
	Transform trans;
	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isGo) Gyrate ();
	}
	
	void Gyrate ()
	{
		angle += speed * Time.deltaTime;
		if (angle > 360) angle -= 360;
		trans.position = new Vector3 (trans.position.x, trans.position.y + (maxUpAndDown * Mathf.Sin (angle * toDegrees)), trans.position.z);
	}
}
