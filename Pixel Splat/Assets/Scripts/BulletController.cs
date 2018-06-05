using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public float deathTime;
	public bool firstContact;

	public int playerNumber;
	string playerTag;
	string bodyTag;

	public Vector2 test;

	public GameObject player;

	// Use this for initialization
	void Start () {
		playerTag = "Player" + playerNumber;
		bodyTag = "Body" + playerNumber;
		player = GameObject.Find (playerTag);
		Destroy (gameObject, deathTime);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += transform.up * speed;
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player2" && firstContact) {
			firstContact = false;
			Destroy (gameObject);

			//changing parent
			coll.gameObject.GetComponent<StayInside> ().playerCircle = player;
			coll.gameObject.tag = playerTag;
			coll.gameObject.transform.parent = GameObject.Find (bodyTag).transform;
			player.GetComponent<PlayerController> ().playerHealth++;
		}

		if (coll.gameObject.tag == "Platform") {
			//test = new Vector2 (1, 1);
			test = Vector2.Reflect (gameObject.transform.up, coll.gameObject.transform.up);
			transform.up = test;

		}
	}
}
