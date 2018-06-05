using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

	public GameObject player;
	Animator ac;

	// Use this for initialization
	void Start () {
		ac = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		ac.SetBool ("isMoving", player.GetComponent<PlayerMovement> ().isMoving);
		if (player.GetComponent<PlayerMovement> ().gameOver) {
			ac.SetTrigger ("Death");
		}
	}
}
