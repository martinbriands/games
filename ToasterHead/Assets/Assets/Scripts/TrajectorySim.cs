using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySim : MonoBehaviour {

	public LineRenderer sightLine;
	public GameObject playerFire;
	public float fireStrength;
	public int segmentCount = 20;
	public float segmentScale = 1;
	private Collider _hitObject;
	public Collider hitObject { get { return _hitObject; } }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		simulatePath ();
	}

	void simulatePath() {
		Vector3[] segments = new Vector3[segmentCount];
		segments [0] = playerFire.transform.position;
		Vector3 segVelocity = playerFire.transform.up * fireStrength * Time.deltaTime;
		_hitObject = null;

		for (int i = 1; i < segmentCount; i++) {
			float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;
			segVelocity = segVelocity + Physics.gravity * segTime;

			RaycastHit hit;
			if (Physics.Raycast (segments [i - 1], segVelocity, out hit, segmentScale)) {
				_hitObject = hit.collider;

				segments [i] = segments [i - 1] + segVelocity.normalized * hit.distance;
				segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
				//segVelocity = Vector3.Reflect (segVelocity, hit.normal);
			} else {
				segments [i] = segments [i - 1] + segVelocity * segTime;
			}
		}
		Color startColor = Color.red;
		Color endColor = startColor;
		startColor.a = 1;
		endColor.a = 0;
		sightLine.startColor = startColor;
		sightLine.endColor = endColor;

		sightLine.positionCount = segmentCount;
		for (int i = 0; i < segmentCount; i++) {
			sightLine.SetPosition (i, segments [i]);
		}
	}
}
