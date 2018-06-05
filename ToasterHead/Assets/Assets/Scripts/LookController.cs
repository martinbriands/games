using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour {

	public GameObject player;
	public float isworking;
	public float factor;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		isworking = Input.GetAxis ("XboxRH");
		gameObject.transform.position += - transform.right * Input.GetAxis ("XboxRH") * factor;
		gameObject.transform.position += - transform.up * Input.GetAxis ("XboxRV") * factor;

//		gameObject.transform.localPosition = Input.mousePosition / 1000 - new Vector3 (1f, 0.5f);

		gameObject.transform.localPosition = new Vector3 (
			Mathf.Clamp (transform.localPosition.x, -1, 1),
			Mathf.Clamp (transform.localPosition.y, -0.5f, 0.55f),
			0f
		);
			
		transform.LookAt (player.transform);

	}
}
