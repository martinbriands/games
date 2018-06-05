using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

	public float tiltSpeed;
	public float rotationSpeed;
	public float jump;

	public Transform wings;
	public Transform body;

	public bool turningLeft = false;
	public bool turningRight = false;
	public bool tilting = false;

	public float rotationSpeedTilt;
	public float rotationSpeedReset;

	private Rigidbody rb;
	public Animator flyAC;

	public Transform reset;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		flyAC = gameObject.GetComponent<Animator> ();

	}

	void Update() {


		//checking

		//rotato potato
		if (Input.GetKeyDown (KeyCode.W)) {
			tilting = true;
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			tilting = false;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			tilting = true;
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			tilting = false;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			turningLeft = true;
		}

		if (Input.GetKeyUp (KeyCode.A)) {
			turningLeft = false;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			turningRight = true;
		}
		
		if (Input.GetKeyUp (KeyCode.D)) {
			turningRight = false;
		}

		//jumping
		if (Input.GetKeyDown (KeyCode.Space)) {

			flyAC.SetTrigger ("Space");

			//Vector3 velocity = rb.velocity;
			rb.velocity = Vector3.zero;
			//rb.velocity = transform.forward * velocity.magnitude;

			rb.AddForce (transform.forward * jump);
		}

		//unglide
		if (Input.GetKeyUp (KeyCode.LeftShift))
			rb.useGravity = true;
		

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!tilting && !turningRight && !turningLeft && wings.localRotation != Quaternion.identity) {
			wings.localRotation = Quaternion.Lerp (wings.localRotation, Quaternion.identity, Time.deltaTime * rotationSpeedReset);
			body.localRotation = Quaternion.Lerp (body.localRotation, Quaternion.identity, Time.deltaTime * rotationSpeedReset);
		}

		//glide
		if (Input.GetKey (KeyCode.LeftShift)) 
		{
			rb.useGravity = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			//if (rb.velocity.magnitude <= 5)
			//	rb.AddForce (transform.forward * speed);

			transform.Rotate (Vector3.right * rotationSpeed * Time.deltaTime);

			//Vector3 velocity = rb.velocity;
			//rb.velocity = Vector3.zero;
			//rb.velocity = transform.forward * velocity.magnitude;

			wings.localRotation = Quaternion.Lerp (wings.localRotation, Quaternion.Euler(10f, 0f, 0f), Time.deltaTime * rotationSpeedTilt);
			body.localRotation = Quaternion.Lerp (body.localRotation, Quaternion.Euler(10f, 0f, 0f), Time.deltaTime * rotationSpeedTilt);
		}

			
		if (Input.GetKey (KeyCode.S)) {
			transform.Rotate (Vector3.left * rotationSpeed * Time.deltaTime);

			//Vector3 velocity = rb.velocity;
			//rb.velocity = Vector3.zero;
			//rb.velocity = transform.forward * velocity.magnitude;

			wings.localRotation = Quaternion.Lerp (wings.localRotation, Quaternion.Euler(-10f, 0f, 0f), Time.deltaTime * rotationSpeedTilt);
			body.localRotation = Quaternion.Lerp (body.localRotation, Quaternion.Euler(-10f, 0f, 0f), Time.deltaTime * rotationSpeedTilt);
		}

		if (Input.GetKey (KeyCode.A)) {

			transform.Rotate (Vector3.down * tiltSpeed * Time.deltaTime);

			Vector3 velocity = rb.velocity;
			rb.velocity = Vector3.zero;
			rb.velocity = transform.forward * velocity.magnitude;

			wings.localRotation = Quaternion.Lerp (wings.localRotation, Quaternion.Euler(0f, 0f, 15), Time.deltaTime * rotationSpeedTilt);
			body.localRotation = Quaternion.Lerp (body.localRotation, Quaternion.Euler(0f, 0f, 15), Time.deltaTime * rotationSpeedTilt);
		}


		
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.up * tiltSpeed * Time.deltaTime);
			Vector3 velocity = rb.velocity;
			rb.velocity = Vector3.zero;
			rb.velocity = transform.forward * velocity.magnitude;

			wings.localRotation = Quaternion.Lerp (wings.localRotation, Quaternion.Euler(0f, 0f, -15), Time.deltaTime * rotationSpeedTilt);
			body.localRotation = Quaternion.Lerp (body.localRotation, Quaternion.Euler(0f, 0f, -15), Time.deltaTime * rotationSpeedTilt);
		}

		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
			//rb.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			rb.transform.rotation = Quaternion.Lerp (transform.rotation, reset.rotation, Time.deltaTime * 2);
		}
	}
}
