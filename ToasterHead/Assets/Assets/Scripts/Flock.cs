using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

	public float speed = 0.5f;
	public float maxSpeed = 2f;
	public float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighbourDistance = 2.0f;

	public bool turning;
	public GameObject tank;
	public float tankSize = 6.0f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		speed = Random.Range (1f, 1f);
		tank = GameObject.Find ("Tank");
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
////		if (Vector3.Distance (transform.position, tank.transform.position) >= tankSize) {
////			turning = true;
////		} else if (Vector3.Distance (transform.position, tank.transform.position) <= tankSize){
////			turning = false;
////		}
//
//		if (turning) {
//			Vector3 direction = tank.transform.position - transform.position;
//			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);
//		}

		if (Random.Range (0, 5) < 3) {
			ApplyRules ();
		}
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyRules() {
		GameObject[] gos;
		gos = GlobalFlock.allBirbs;

		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.05f;

		//Vector3 goalPos = GlobalFlock.goalPos;
		Vector3 goalPos;

		GameObject[] leftovers = GameObject.FindGameObjectsWithTag ("Bread");
		if (leftovers.Length > 0) {
			float minDistance = Vector3.Distance (transform.position, leftovers [0].transform.position);
			GameObject closest = leftovers [0];
			for (int i = 1; i < leftovers.Length; i++) {
				if (Vector3.Distance (transform.position, leftovers [i].transform.position) < minDistance) {
					minDistance = Vector3.Distance (tank.transform.position, leftovers [i].transform.position);
					closest = leftovers [i];
				}
			}
			goalPos = closest.transform.position;
		} else {
			goalPos = new Vector3(transform.position.x, 3, transform.position.z);
		}

		float dist;

		int groupSize = 0;
		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighbourDistance) {
					vcentre += go.transform.position;
					groupSize++;

					if (dist < 1f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}

					Flock anotherFlock = go.GetComponent<Flock> ();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		}

		if (groupSize > 0) {
			vcentre = vcentre / groupSize + (goalPos - this.transform.position);
			speed = Mathf.Min (gSpeed / groupSize, maxSpeed);

			Vector3 direction = (vcentre + vavoid) - transform.position;
			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);
			}
		} else {
			transform.LookAt (goalPos);
		}
	}

}
