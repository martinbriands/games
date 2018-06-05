using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybehaviour : MonoBehaviour {

	public bool invincible;
	public bool engaged;

	public int lives;
	public int fleeForce;
	public float invincibleTime;

	public GameObject[] points;
	public int index;
	public GameObject target;

	public float speed;
	public GameObject gameController;

	Rigidbody rb;
	Animator anim;
	float t;

	// Use this for initialization
	void Start () {
		index = 0;
		if (points.Length > 0) {
			target = points [index];
		}

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (engaged) {
			if (t == 0) {
				rb.AddForce (transform.up * fleeForce);
			}
			invincible = true;
			while (t < invincibleTime) {
				t += Time.deltaTime;
				return;
			} 
			engaged = false;
			invincible = false;
		} else if (lives > 0 && target != null) {
			transform.LookAt (target.transform);
			transform.rotation = Quaternion.Euler(new Vector3 (0, transform.rotation.eulerAngles.y, 0));

//			if (switching) {
//				transform.LookAt (startPoint);
//				transform.rotation = Quaternion.Euler(new Vector3 (0, transform.rotation.eulerAngles.y, 0));
//			} else {
//				transform.LookAt (endPoint);
//				transform.rotation = Quaternion.Euler(new Vector3 (0, transform.rotation.eulerAngles.y, 0));
//			}
			transform.position += transform.forward * speed;
		}
	}


	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag == "Birb" && !invincible && lives > 0) {
			lives--;
			engaged = true;
			t = 0;
			if (lives <= 0) {
				anim.SetTrigger ("Death");
				rb.constraints = RigidbodyConstraints.None;
				gameController.GetComponent<GameController> ().numKilled++;
			}
		}
	}

	void OnTriggerEnter (Collider coll) {
		if (target != null && coll.gameObject.name == target.name) {

			if (index < points.Length - 1) {
				index++;
			} else {
				index = 0;
			}
			target = points [index];
			transform.Rotate(new Vector3(0f, 180, 0f));
		}
	}
}
