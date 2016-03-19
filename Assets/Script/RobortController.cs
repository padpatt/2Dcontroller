using UnityEngine;
using System.Collections;

public class RobortController : MonoBehaviour {
	public float maxSpeed = 10f;
	bool facingRight = true;
	private Rigidbody2D rb2D;
	Animator anim;
	bool jump1 = false;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadiuus = 0.2f;
	public LayerMask whatIsGround;

	public float jumpForce = 350f;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		rb2D.velocity = new Vector2 (move * maxSpeed, rb2D.velocity.y);
		if (grounded) {
			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}
		}
		{
			//
			grounded = Physics2D.OverlapCircle (groundCheck.position,
		                                    groundRadiuus,
		                                    whatIsGround);
			anim.SetBool ("Ground", grounded);
			anim.SetFloat ("vSpeed",rb2D.velocity.y);
		}
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			rb2D.AddForce (new Vector2 (0, jumpForce));
			jump1 = true;
		} else 
		{
			if(jump1 && Input.GetKeyDown (KeyCode.Space))
			{
				anim.SetBool ("Ground", false);
				rb2D.AddForce (new Vector2 (0, jumpForce));
				jump1 = false;
			}
		}

		if (grounded && Input.GetKeyDown (KeyCode.DownArrow)) {
			anim.SetBool ("Roll", true);
		}
		if (grounded && Input.GetKeyDown (KeyCode.UpArrow)) {
			anim.SetBool ("Roll", false);
		}
	}
}
