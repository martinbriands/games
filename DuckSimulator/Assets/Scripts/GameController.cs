using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] rings;
	public GameObject arrow;
	public GameObject bird;

	public Text timer;
	public float timeTaken;

	public Text victorious;

	public Material nextMaterial;
	public Material currMaterial;

	GameObject current;
	GameObject next;

	public int i = 0;

	public bool victory;

	// Use this for initialization
	void Start () {
		current = rings [0];
		next = rings [1];
		arrow.GetComponent<ArrowDir> ().target = current;

		current.SetActive (true);

		next.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		if (current.GetComponent<RingDestroyer> ().isDestroyed == true && !victory) {
			i++;
			if (i == 5)
				victory = true;
			else if (i == 4) {
				current = rings [i];
				arrow.GetComponent<ArrowDir> ().target = current;
			} else {
				current = rings [i];
				next = rings [i + 1];
				arrow.GetComponent<ArrowDir> ().target = current;

				//current.SetActive (true);

				next.SetActive (true);
			}
		}

		if (victory) {
			arrow.GetComponent<ArrowDir> ().target = bird;
			victorious.gameObject.SetActive (true);
		}

		if (!victory) {
			timeTaken += Time.deltaTime;
			timer.text = (Mathf.Round (timeTaken * 100) / 100).ToString ();
		}

		if (victory && Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (0);
	}
}
