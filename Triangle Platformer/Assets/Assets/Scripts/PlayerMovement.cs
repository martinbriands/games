using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rb;
	public float speed;
	public int jumpForce;
	public int launchForce;
	public int rotateSpeed;
	public int jumps;
	Vector3 move;

	public bool grounded = false;
	bool facingRight = true;
	public bool charging = false;

	Animator anim;
	public GameObject powerCircle;

	public Image jump1;
	public Image jump2;
	public Image jump3;
	Color temp;

	public Image charge;
	public float chargePercent;

	public ParticleSystem trails;
	public Sprite baseSprite;
	public string color;
	bool firstJump = true;
	public KeyCode userButton;

	public GameObject smoke;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody2D> ();
		anim = GetComponent <Animator> ();
		GetComponent<SpriteRenderer> ().sprite = baseSprite;
	}

	void Update ()
	{
		if (grounded) 
		{
			jumps = 0;
			firstJump = true;
		}

		//new jumping
		if (grounded && Input.GetKeyDown (userButton)) 
		{
			rb.AddForce(new Vector2 (0, jumpForce));
		}


		if (Input.GetKeyDown (userButton) && jumps < 3 && !grounded) 
		{
			jumps += 1;
			Instantiate (powerCircle, rb.transform.position, Quaternion.Euler (new Vector3 (0,0,-60)));

			trails.Stop ();
		}

		if (Input.GetKeyUp (userButton) && jumps < 4) 
		{
			rb.constraints = RigidbodyConstraints2D.None;
			//make launchForce a charging attack
			rb.AddForce (rb.transform.up * launchForce * chargePercent); 

			charge.fillAmount = 0;
			chargePercent = 0;

			firstJump = false;

			//bad but whatever
			if (jumps == 3) {
				jumps++;
			}
		}

		//jumpbars stuff
		if (jumps == 0) {
			Color tmp = jump1.color;
			tmp.a = 1f;
			jump1.color = tmp;
			jump2.color = tmp;
			jump3.color = tmp;
		} else if (jumps == 1) {
			Color tmp = jump1.color;
			tmp.a = 0.3f;
			jump1.color = tmp;
		} else if (jumps == 2) {
			Color tmp = jump2.color;
			tmp.a = 0.3f;
			jump2.color = tmp;
		} else if (jumps == 3) {
			Color tmp = jump3.color;
			tmp.a = 0.3f;
			jump3.color = tmp;
		}
	}
	// Update is called once per frame
	void FixedUpdate () {

		//rotato potato
		if (!grounded && Input.GetKey (userButton) && jumps < 4  && firstJump == false){
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			rb.angularVelocity = 0;
			rb.transform.Rotate (Vector3.forward * rotateSpeed /** (Mathf.Pow(1/chargePercent, 2))*/, Space.Self);
			charging = true;
			if (chargePercent <= 1) 
			{
				chargePercent += 0.03f;
				charge.fillAmount = chargePercent/4;
			}

			//powerCircle.transform.position = rb.transform.position;
			//powerCircle.SetActive (true);

		} else {
			
			//StartCoroutine (PowerCircleOff ());

			//movement
			//move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			//rb.transform.position += move * speed * Time.deltaTime;
			charging = false;

			anim.SetFloat ("speed", Mathf.Abs (move.x));

			if (move.x > 0 && !facingRight) {
				Flip ();
			} else if (move.x < 0 && facingRight) {
				Flip ();
			}
		}
	}

	void Flip() 
	{
		facingRight = !facingRight;
		Vector3 scale = rb.transform.localScale;
		scale.x *= -1;
		rb.transform.localScale = scale;
			
	}

	IEnumerator PowerCircleOff()
	{
		yield return new WaitForSeconds (1f);
		if (!Input.GetKey (userButton)) 
			{
			powerCircle.SetActive (false);
			}
		
	}
		
	//detects if you are on a platform
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Platform") {
			grounded = true;
		}
	}

	//detects when you leave the platform
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Platform") {
			grounded = false;
		}
	}
		
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.GetComponent<PlayerMovement> ().color.CompareTo ("Water") == 0 && color == "Grass") {
				Instantiate (smoke, coll.gameObject.transform.position, Quaternion.identity);
				Destroy (coll.gameObject);
			} else if (coll.gameObject.GetComponent<PlayerMovement> ().color.CompareTo ("Fire") == 0 && color == "Water") {
				Instantiate (smoke, coll.gameObject.transform.position, Quaternion.identity);
				Destroy (coll.gameObject);
			} else if (coll.gameObject.GetComponent<PlayerMovement> ().color.CompareTo ("Grass") == 0 && color == "Fire") {
				Instantiate (smoke, coll.gameObject.transform.position, Quaternion.identity);
				Destroy (coll.gameObject);
			}
		}
	}
}
