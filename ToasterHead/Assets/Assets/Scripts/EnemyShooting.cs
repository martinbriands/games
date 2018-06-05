using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public Transform leftHole;
	public Transform rightHole;
	public GameObject bread;
	public int switchHole;
	public float launchForce;
	public float fireRate;

	// Use this for initialization
	void Start () {
		StartCoroutine (Shooting ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Shooting() {
		while (true) {
			if (switchHole == 0) {
				GameObject slice = Instantiate (bread, leftHole.position, leftHole.rotation);
				slice.GetComponent<Rigidbody> ().AddForce (slice.transform.forward * launchForce);
				switchHole++;
			} else if (switchHole == 1) {
				GameObject slice = Instantiate (bread, rightHole.position, rightHole.rotation);
				slice.GetComponent<Rigidbody> ().AddForce (slice.transform.forward * launchForce);
				switchHole++;
			} else if (switchHole == 2) {
				//change it to every third shot
				GameObject slice1 = Instantiate (bread, leftHole.position, leftHole.rotation);
				slice1.GetComponent<Rigidbody> ().AddForce (slice1.transform.forward * launchForce);

				GameObject slice2 = Instantiate (bread, rightHole.position, rightHole.rotation);
				slice2.GetComponent<Rigidbody> ().AddForce (slice2.transform.forward * launchForce);

				switchHole = 0;
			}
			yield return new WaitForSeconds (fireRate);
		}
	}
}
