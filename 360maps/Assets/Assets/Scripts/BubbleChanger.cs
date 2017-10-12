using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChanger : MonoBehaviour {

	public bool contact;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "LookAtMe") {
			contact = true;
			coll.gameObject.GetComponent<Material> ().color = Color.green;
		} else {
			contact = false;
		}
	}
}
