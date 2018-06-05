using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingDestroyer : MonoBehaviour {

	public int points; 
	public bool isDestroyed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll) {
		isDestroyed = true;
		gameObject.SetActive (false);
	}
}
