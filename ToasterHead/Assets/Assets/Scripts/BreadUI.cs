using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadUI : MonoBehaviour {
	float i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		this.transform.localRotation = Quaternion.Euler( new Vector3 (-41 , i, 0));
	}
}
