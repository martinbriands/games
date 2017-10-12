using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//addtime between dashes

	Rigidbody2D rb;

	public float speed;
	public float rotationSpeed;
	public GameObject gun;

	public GameObject bullet;
	public GameObject body;

	//
	float buttonCoolerUp = 0.5f;
 	int buttonCountUp;

	float buttonCoolerDown = 0.5f;
	int buttonCountDown;

	float buttonCoolerLeft= 0.5f;
	int buttonCountLeft;

	float buttonCoolerRight = 0.5f;
	int buttonCountRight;
	//

	public float doubleTapTime;
	public float doubleTapForce;
	public float doubleTapSpeed;

	public int playerHealth;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		playerHealth = GetComponentsInChildren<StayInside> ().Length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		rb.velocity = new Vector2 (h, v) * speed;
	}

	void Update () {

		doubleTapTime += Time.deltaTime;

		Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
		transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * rotationSpeed);

		if (Input.GetKeyDown (KeyCode.Mouse0))
			Instantiate(bullet, gun.transform.position, gun.transform.rotation);

		//dodging
		if (Input.GetKeyDown (KeyCode.Space))
			GetComponent<PointEffector2D> ().forceMagnitude = 200;

		if (Input.GetKeyUp (KeyCode.Space))
			GetComponent<PointEffector2D> ().forceMagnitude = -140;

		//dashing
		if (Input.GetKeyDown (KeyCode.W)) {
			if (buttonCoolerUp > 0 && buttonCountUp == 1) {
				StartCoroutine(Dash ("up"));
			} else {
				buttonCoolerUp = 0.5f;
				buttonCountUp += 1;
			}
		}
		if (buttonCoolerUp > 0) {
			buttonCoolerUp -= 1 * Time.deltaTime;
		}else {
			buttonCountUp = 0;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			if (buttonCoolerDown > 0 && buttonCountDown == 1) {
				StartCoroutine(Dash ("down"));
			} else {
				buttonCoolerDown = 0.5f;
				buttonCountDown += 1;
			}
		}
		if (buttonCoolerDown > 0) {
			buttonCoolerDown -= 1 * Time.deltaTime;
		} else {
			buttonCountDown = 0;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			if (buttonCoolerRight > 0 && buttonCountRight == 1) {
				StartCoroutine(Dash ("right"));
			} else {
				buttonCoolerRight = 0.5f;
				buttonCountRight += 1;
			}
		}
		if (buttonCoolerRight > 0) {
			buttonCoolerRight -= 1 * Time.deltaTime;
		}else {
			buttonCountRight = 0;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			if (buttonCoolerLeft > 0 && buttonCountLeft == 1) {
				StartCoroutine(Dash ("left"));
			} else {
				buttonCoolerLeft = 0.5f;
				buttonCountLeft += 1;
			}
		}
		if (buttonCoolerLeft > 0) {
			buttonCoolerLeft -= 1 * Time.deltaTime;
		}else {
			buttonCountLeft = 0;
		}

		//finish
		if (playerHealth == 0) {
			gun.transform.parent = null;
			Destroy (gameObject);
		}
	}

	IEnumerator Dash (string direction) {
		if (doubleTapTime > 1) {
			doubleTapTime = 0;
			Vector2 currentPos = transform.position;
			float t = 0f;

			while (t < 1) {
				t += Time.deltaTime / doubleTapSpeed;
				if (direction == "up")
					rb.AddForce (Vector2.up * doubleTapForce);
				if (direction == "down")
					rb.AddForce (Vector2.down * doubleTapForce);
				if (direction == "left")
					rb.AddForce (Vector2.left * doubleTapForce);
				if (direction == "right")
					rb.AddForce (Vector2.right * doubleTapForce);
				//transform.position = Vector2.Lerp (currentPos, currentPos + Vector2.up * doubleTapForce, t);
				yield return null;
			}
		}
	}
}
