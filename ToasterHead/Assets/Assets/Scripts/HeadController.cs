using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

	public GameObject sphere;
	public LineRenderer line;
	public Camera VR;
	// Use this for initialization
	void Start () {
		//line = gameObject.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (sphere.transform);

		transform.rotation = VR.gameObject.transform.rotation;

		Vector3 forward = transform.TransformDirection (Vector3.forward) * 10;
		Debug.DrawRay (transform.position, forward, Color.red);

		//line.SetPosition (0, transform.position);
		//line.SetPosition (1, sphere.transform.position);

	}
}
