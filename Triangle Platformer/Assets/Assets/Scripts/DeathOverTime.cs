using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverTime : MonoBehaviour {

	bool smaller = true;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.localScale.x < 20 && smaller) {
			gameObject.transform.localScale += new Vector3 (1, 1, 0);
		} else {
			smaller = false;
			gameObject.transform.localScale += new Vector3 (-0.2f, -0.2f, 0);
			if (gameObject.transform.localScale.x < 0.2) {
				Destroy (gameObject);
			}
		}
	}
}
