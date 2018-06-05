using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {

	public float speed;
	public bool firstContact;

	public GameObject enemy;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, enemy.GetComponent<EnemyController>().deathTime);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += transform.up * speed;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player1" && firstContact) {
			firstContact = false;
			Destroy (gameObject);

			//changing parent
			coll.gameObject.GetComponent<StayInside> ().playerCircle = enemy;
			coll.gameObject.tag = "Player2";
			coll.transform.parent.parent.GetComponent<PlayerController> ().playerHealth--;
			coll.gameObject.transform.parent = GameObject.Find ("Body2").transform;

		}

		if (coll.gameObject.tag == "Platform") {
			//test = new Vector2 (1, 1);
			transform.up = Vector2.Reflect (gameObject.transform.up, coll.gameObject.transform.up);

		}
	}
}
