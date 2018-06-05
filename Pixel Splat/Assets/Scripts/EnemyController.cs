using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject bullet;
	public GameObject gun;
	public float shotSpeed;
	public float deathTime;

	public bool advanced;
	public GameObject player;

	// Use this for initialization
	void Start () {
		StartCoroutine (Shoot ());
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponentsInChildren<StayInside> ().Length == 0) {
			gun.transform.parent = null;
			Destroy (gameObject);
		}

		if (advanced) {
			Vector2 mouseScreenPosition = player.transform.position;
			Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
			transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * 10);
		}
	}

	IEnumerator Shoot() {
		while (true) {
			GameObject bulletObject = Instantiate (bullet, gun.transform.position, gun.transform.rotation);
			bulletObject.GetComponent<EnemyBulletController> ().enemy = gameObject;
			yield return new WaitForSeconds (shotSpeed);
		}
	}
}
