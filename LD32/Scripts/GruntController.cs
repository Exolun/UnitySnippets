using UnityEngine;
using System.Collections;

public class GruntController : EnemyController {
	public float pushForce;

	private Rigidbody2D rbody;
	private bool facingLeft = true;
	private Animator anim;
	private bool dead = false;
	private float durationDead = 0;

	private GameSimulation simulation;

	void Start(){
		this.rbody = GetComponent<Rigidbody2D> ();
		this.anim = GetComponent<Animator>();
		anim.SetBool("Walking", true);

		this.simulation = GameObject.Find ("GameSimulation").GetComponent<GameSimulation> ();
		this.simulation.IncrementEnemiesAlive ();
	}
	
	void FixedUpdate()
	{
		if (this.dead) {
			this.doDeath();
			return;
		}

		if (TargetPlayer) {
			var yDelta = TargetPlayer.transform.position.y - transform.position.y;
			var xDelta = TargetPlayer.transform.position.x - transform.position.x;
			if(xDelta > 0 && this.facingLeft == true){
				Flip();
			}
			else if(xDelta < 0 && this.facingLeft == false){
				Flip();
			}

			var movement = new Vector2(xDelta, yDelta);
			movement.Normalize();

			this.gameObject.transform.Translate(movement * speed * Time.deltaTime);
		}
	}

	private void doDeath() {
		if (durationDead > 1) {
			this.simulation.DecrementEnemiesAlive();
			Destroy (this.gameObject);
		} 
		else {
			var rotationAmount = -45f;
			var currentScale = this.gameObject.transform.localScale;


			this.gameObject.transform.Rotate(new Vector3(0, 0, rotationAmount*Time.deltaTime));
			this.gameObject.transform.localScale = this.gameObject.transform.localScale * 0.905f;
			this.durationDead += Time.deltaTime;
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var yDelta = other.transform.position.y - transform.position.y;
			var xDelta = other.transform.position.x - transform.position.x;
			var attackDir = new Vector2 (xDelta, yDelta);
			attackDir.Normalize ();
			
			var otherRBody = other.gameObject.GetComponent<Rigidbody2D>();
			otherRBody.AddForce(attackDir * this.pushForce);
		}
	}

	public void Death(){
		anim.SetBool ("Walking", false);
		this.dead = true;
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		this.facingLeft = !this.facingLeft;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
