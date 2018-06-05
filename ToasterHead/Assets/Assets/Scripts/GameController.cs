using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] Enemies;
	public int numEnemies;
	public int numKilled;

	public GameObject player;
	public GameObject cane;
	public GameObject head, hat, target;

	public Camera GOCamera;
	public Camera curr;
	public GameObject traj;

	public GameObject canvas;

	public bool gameOver;
	public bool won;
	public float lerpTime;

	// Use this for initialization
	void Start () {
		numEnemies = Enemies.Length;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.JoystickButton1) && (won || gameOver)) {
			SceneManager.LoadScene ("Scene01");
			//Application.LoadLevel (Application.loadedLevel);
		}

		if (numKilled == numEnemies) {
			// You won the game
			numKilled++;
			curr.enabled = false;
			GOCamera.enabled = true;

			head.SetActive (true);
			hat.SetActive (true);

			target.SetActive (false);
			canvas.SetActive (false);
			won = true;

			//won stuff

		}

		if ((!gameOver && player.GetComponent<PlayerMovement> ().gameOver)) {
			gameOver = true;
			transform.position = player.transform.position + new Vector3(0, 1,0);

			curr.enabled = false;
			GOCamera.enabled = true;

			//make it ragdoll
			player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

			//plus create the head
			cane.transform.parent = null;
			cane.AddComponent<Rigidbody> ();

			//tell them they are dead
			//transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 5, 0), lerpTime * Time.deltaTime);
			//
			//transform.LookAt (player.transform.position);

			head.SetActive (true);
			hat.SetActive (true);
			traj.GetComponent<LineRenderer> ().enabled = false;

			target.SetActive (false);
			canvas.SetActive (false);

		}

		if (gameOver) {
			transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 5, 0), lerpTime * Time.deltaTime);
			transform.LookAt (player.transform.position);
		}

		if (won) {
			transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 3, -3), 1 * Time.deltaTime);
			transform.LookAt (player.transform.position);

		}

	}
}
