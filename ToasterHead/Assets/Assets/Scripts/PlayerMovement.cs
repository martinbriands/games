using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	Rigidbody rb;
	public float jumpForce;
	public bool grounded;
	public bool isMoving;

	public float speed;
	public float rotateSpeed;

	//public Transform leftHole;
	//public Transform rightHole;
	//public GameObject leftSlider;
	//public GameObject rightSlider;

	//public int switchHole;
	//public bool reinitialize;

	public Transform throwArm;

	public GameObject bread;
	public float launchForce;
	public Slider slider;
	public float charge;

	public GameObject mostRecentSlice;

	//line renderer shit
	public GameObject trajectorySim;
	TrajectorySim tjs;

	public float h, v, h2;

	public Text breadCount;
	public int count;

	public bool gameOver;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		tjs = trajectorySim.GetComponent<TrajectorySim> ();
		tjs.fireStrength = 0;
		//trajectorySim.SetActive (false);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!gameOver) {
			h = Input.GetAxis ("XboxLH");
			v = -Input.GetAxis ("XboxLV");

			gameObject.transform.position += transform.forward * speed * v;
			gameObject.transform.position += transform.right * speed * h;


			h2 = Input.GetAxis ("XboxRH");
			//gameObject.transform.position += transform.right * speed * h;
			gameObject.transform.Rotate (new Vector3 (0f, h2, 0f) * rotateSpeed);
			//rb.AddForce (new Vector3 (v, 0f, h) * speed);]

			if (Mathf.Abs (h) > 0f || Mathf.Abs (v) > 0f) {
				isMoving = true;
			} else {
				isMoving = false;
			}
		}
	}

	void Update () {
		if (!gameOver) {
		//charging
		if (Input.GetButton ("XboxRB")) {
			//trajectorySim.SetActive (true);
			tjs.fireStrength = 500 * slider.value;
		
			//reinitialize = false;
			slider.value += 0.02f;
			//switch(switchHole) 
			//{
			//case 0:
			//	leftSlider.transform.localPosition = Vector3.Lerp(leftSlider.transform.localPosition, new Vector3 (-0.2f, 0.55f, -0.3f), 0.05f);
			//	break;
			//case 1:
			//	rightSlider.transform.localPosition = Vector3.Lerp(rightSlider.transform.localPosition, new Vector3 (0.2f, 0.55f, -0.3f), 0.05f);
			//	break;
			//case 2:
			//	leftSlider.transform.localPosition = Vector3.Lerp(leftSlider.transform.localPosition, new Vector3 (-0.2f, 0.55f, -0.3f), 0.05f);
			//	rightSlider.transform.localPosition = Vector3.Lerp(rightSlider.transform.localPosition, new Vector3 (0.2f, 0.55f, -0.3f), 0.05f);
			//	break;
			//} 

		} 
		if (Input.GetButtonUp ("XboxRB")) {

			if (slider.value > 0.5f) {
				//if (switchHole == 0) {
					//mostRecentSlice = Instantiate (bread, leftHole.position, leftHole.rotation);
				mostRecentSlice = Instantiate (bread, throwArm.position, throwArm.rotation);
				mostRecentSlice.GetComponent<Rigidbody> ().AddForce (mostRecentSlice.transform.forward * launchForce * slider.value);

				breadCount.text = "x " + ++count;
				//	switchHole++;
				//} else if (switchHole == 1) {
				//	mostRecentSlice = Instantiate (bread, rightHole.position, rightHole.rotation);
				//	mostRecentSlice.GetComponent<Rigidbody> ().AddForce (mostRecentSlice.transform.forward * launchForce * slider.value);
				//	switchHole++;
				//} else if (switchHole == 2) {
				//	//change it to every third shot
				//	GameObject slice1 = Instantiate (bread, leftHole.position, leftHole.rotation);
				//	slice1.GetComponent<Rigidbody> ().AddForce (slice1.transform.forward * launchForce * slider.value);

				//	mostRecentSlice = Instantiate (bread, rightHole.position, rightHole.rotation);
				//	mostRecentSlice.GetComponent<Rigidbody> ().AddForce (mostRecentSlice.transform.forward * launchForce * slider.value);

				//	switchHole = 0;
				//}
			}
			slider.value = 0;
			tjs.fireStrength = 0;
			//trajectorySim.SetActive (false);
			//reinitialize = true;
		}

		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			rb.AddForce (Vector3.up * jumpForce);
			grounded = false;
		}

		//if (reinitialize) {
		//	leftSlider.transform.localPosition = Vector3.Lerp(leftSlider.transform.localPosition, new Vector3 (-0.2f, 0.55f, 0.3f), 0.2f);
		//	rightSlider.transform.localPosition = Vector3.Lerp(rightSlider.transform.localPosition, new Vector3 (0.2f, 0.55f, 0.3f), 0.2f);
		//}
		}

	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
		}

		if (coll.gameObject.tag == "Birb") {
			gameOver = true;
			//Destroy (gameObject);
		}
	}		
		

}
