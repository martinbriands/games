using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject mostRecentSlice;
	public GameObject bread;
	public Transform throwArm;
	public int launchForce;
	float timer;
	public float delay;
	// Use this for initialization
	void Start () {
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer > delay) {
			mostRecentSlice = Instantiate (bread, throwArm.position, throwArm.rotation);
			mostRecentSlice.GetComponent<Rigidbody> ().AddForce (mostRecentSlice.transform.forward * launchForce);
			timer = 0;
		} else {
			timer += Time.deltaTime;
		}
	}
		

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag == "Birb") {
			Destroy (gameObject);
		}
	}		
}
