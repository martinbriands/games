using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

	public GameObject prefab;
	public GameObject goalPrefab;
	static int numBirb = 20;
	public static GameObject[] allBirbs = new GameObject[numBirb];

	public GameObject tank;
	public GameObject mostRecentSlice;
	public GameObject player;
	public GameObject[] leftovers;
	public float speed;

	public static Vector3 goalPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < numBirb; i++) {
			Vector3 pos = new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (1, 2.5f), Random.Range (2, 3.5f));
			allBirbs [i] = (GameObject)Instantiate (prefab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0, 10000) < 50) {
			goalPos = new Vector3 (Random.Range (tank.transform.position.x - 1.5f, tank.transform.position.x + 1.5f), Random.Range (tank.transform.position.y - 1.5f, tank.transform.position.y + 1.5f), Random.Range (tank.transform.position.z - 1.5f, tank.transform.position.z + 1.5f));
			goalPrefab.transform.position = goalPos;
		}

		//mostRecentSlice = player.GetComponent<PlayerMovement> ().mostRecentSlice;

		leftovers = GameObject.FindGameObjectsWithTag ("Bread");
		if (leftovers.Length > 0) {
			float minDistance = Vector3.Distance (tank.transform.position, leftovers [0].transform.position);
			GameObject closest = leftovers [0];
			for (int i = 1; i < leftovers.Length; i++) {
				if (Vector3.Distance (tank.transform.position, leftovers [i].transform.position) < minDistance) {
					minDistance = Vector3.Distance (tank.transform.position, leftovers [i].transform.position);
					closest = leftovers [i];
				}
			}
			mostRecentSlice = closest;
		} else {
			mostRecentSlice = null;
			tank.transform.position = Vector3.Lerp (tank.transform.position, new Vector3 (tank.transform.position.x, 3f,  tank.transform.position.y), Time.deltaTime * speed/2);
		}

		if (mostRecentSlice != null) {
			Vector3 mostRecentSlicePos = new Vector3 (mostRecentSlice.transform.position.x, 3f, mostRecentSlice.transform.position.z);
			tank.transform.position = Vector3.Lerp (tank.transform.position, mostRecentSlicePos, Time.deltaTime * speed);
			goalPos = mostRecentSlice.transform.position;
			goalPrefab.transform.position = goalPos;
		}
		
	}
}
