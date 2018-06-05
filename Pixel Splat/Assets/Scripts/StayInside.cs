using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour {

	public float circleRadius;
	public GameObject playerCircle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 diff = playerCircle.transform.position - gameObject.transform.position;
		float distance = diff.magnitude;

		if (distance > circleRadius)
			gameObject.transform.position = (Vector2) playerCircle.transform.position - diff / distance * 2;
	}
}
