using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDBT : MonoBehaviour {

	public GameObject other1;
	public GameObject other2;
	Collider2D cool;
	SpriteRenderer sprite;

	public float lifeTime;
	public Material trailsMaterial;
	public Sprite newSprite;
	public string wanted;

	void Start () {
		cool = gameObject.GetComponent<Collider2D> ();
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (!cool.isTrigger) 
		{
			Color tmp = sprite.color;
			tmp.a -= 0.05f;
			sprite.color = tmp;
			gameObject.transform.localScale += new Vector3 (0.5f, 0.5f, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Player" && coll.gameObject.name.CompareTo(wanted) == 0)
		{
			//trails
			GameObject.Find (coll.name).GetComponent<PlayerMovement> ().trails.Play ();
			GameObject.Find (coll.name).GetComponent<PlayerMovement> ().color = gameObject.name;

			if (gameObject.name.CompareTo ("Water") == 0)
			{
				trailsMaterial.SetColor ("_Color", Color.blue);
				GameObject.Find (coll.name).GetComponent<SpriteRenderer> ().sprite = newSprite;
			}
			else if (gameObject.name.CompareTo ("Fire") == 0)
			{
				trailsMaterial.SetColor ("_Color", Color.red);
				GameObject.Find (coll.name).GetComponent<SpriteRenderer> ().sprite = newSprite;
			}
			else if (gameObject.name.CompareTo ("Grass") == 0)
			{
				trailsMaterial.SetColor ("_Color", Color.green);
				GameObject.Find (coll.name).GetComponent<SpriteRenderer> ().sprite = newSprite;
			}

			Destroy (other1);
			Destroy (other2);
			cool.isTrigger = false;

			//Destroy (gameObject, lifeTime);
			Destroy (gameObject.transform.parent.gameObject, lifeTime);
		}
	}

}
