using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour {
	
	public GameObject screen;
	public bool open;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Mouse2)) {
			transform.Rotate (new Vector3 (0, 2, 0));
		} else { 
			transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.identity, 0.05f);
		}


		if (Input.GetKeyDown (KeyCode.Q)) {
			if (open) {
				screen.SetActive (false);
				open = false;
			} else {
				screen.SetActive (true);
				open = true;
			}
		}
	}
}
