using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider coll) {

		if (coll.gameObject.tag == "Ground") {
			gameObject.tag = "Bread";
		}

		if (coll.gameObject.tag == "Birb") {
			Destroy (gameObject);
		}
	}
}
