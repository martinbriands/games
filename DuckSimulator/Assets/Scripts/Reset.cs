using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

	public GameObject parent;
	public Quaternion newQuaternion;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		//newQuaternion = parent.transform.rotation.eulerAngles;
		//newQuaternion [0] = 0f;
		newQuaternion.eulerAngles = new Vector3 (parent.transform.rotation.eulerAngles.x, parent.transform.rotation.eulerAngles.y, 0f);
		transform.transform.rotation = newQuaternion;
	}
}
