using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDir : MonoBehaviour {

	Vector3 dir;
	float angle;

	public GameObject target;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform.position);
	}
}
