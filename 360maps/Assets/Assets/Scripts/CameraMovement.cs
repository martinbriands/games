using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float maxDistance;
	public bool isHit;
	public Material green;
	public Material red;

	public GameObject previousHit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		this.transform.Rotate (-v, h, 0);
	}

	void FixedUpdate() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag ("LookAtMe")) {
			isHit = true;
			hit.collider.gameObject.GetComponent<Renderer> ().material = green;
			previousHit = hit.collider.gameObject;
		} else {
			isHit = false;
			if (previousHit != null) {
				previousHit.GetComponent<Renderer> ().material = red;
				previousHit = null;
			} 
		}
	}
}
