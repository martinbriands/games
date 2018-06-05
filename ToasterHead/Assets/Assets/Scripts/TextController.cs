using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour {

	public GameObject c;
	public GameObject t;
	public GameController gc;

	bool once;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gc.won && !once) {
			GetComponent<Text> ().text = "Winner";
			c.transform.parent = this.gameObject.transform.parent;
			t.transform.parent = this.gameObject.transform.parent;
			c.transform.localPosition = new Vector3 (-23, -134.5f, 65f);
			t.transform.localRotation = Quaternion.Euler (0, 0, 0);
			t.transform.localPosition = new Vector3 (93, -134.5f, 65f);
			once = true;
		}
	}
}
