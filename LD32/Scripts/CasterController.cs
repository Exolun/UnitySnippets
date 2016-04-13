using UnityEngine;
using System.Collections;

public class CasterController : EnemyController {
	public GameObject fireBall;
	public float DelayBetweenFireballs;



	private float fireballTempSpeed = 0;
	private Rigidbody2D rbody;
	private bool facingLeft = true;
	private Animator anim;
	private bool dead = false;
	private float durationDead = 0;
	
	private GameSimulation simulation;
	private float lastFireballTime = 0;
	private bool fireballThrown = false;
	private AudioSource throwSfx;

	void Start(){
		this.rbody = GetComponent<Rigidbody2D> ();
		this.anim = GetComponent<Animator>();
		anim.SetBool("Walking", true);
		
		this.simulation = GameObject.Find ("GameSimulation").GetComponent<GameSimulation> ();
		this.simulation.IncrementEnemiesAlive ();

		this.throwSfx = GetComponent<AudioSource> ();
	}
	
	void FixedUpdate()
	{
		if (this.dead) {
			this.doDeath ();
			return;
		}
		//Throw fireball
		else if (Time.time > lastFireballTime + this.DelayBetweenFireballs && fireballThrown == false) {
			this.throwFireball ();
		} 
		else if (this.fireballThrown) {
			this.resetToWalkModeIfReady();
		} 
		else {
			this.marchTowardTarget();
		}


	}

	private void resetToWalkModeIfReady() {
		if (Time.time > lastFireballTime + this.DelayBetweenFireballs / 2) {
			this.fireballThrown = false;
			this.speed = fireballTempSpeed;
			anim.SetBool("Walking", true);
		}
	}

	private void marchTowardTarget(){
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
			//this.rbody.AddForce(movement * speed);
		}
	}

	private void throwFireball(){
		if (TargetPlayer) {
			this.anim.SetBool("Walking", false);
			this.anim.Play("CasterFireball");
			this.fireballTempSpeed = this.speed;
			this.speed = 0;

			var firePosition = this.gameObject.GetComponentInChildren<ParticleSystem>().transform.position;
			var yDelta = this.gameObject.transform.position.y - firePosition.y;
			var xDelta = this.gameObject.transform.position.x - firePosition.x;			
			var fireballDirection = new Vector2(xDelta, yDelta);
			fireballDirection.Normalize();

			var fireball = Instantiate(this.fireBall);
			fireball.transform.position = firePosition;
			var fireballCtrlr = fireBall.GetComponent<FireballController>();
			fireballCtrlr.direction = fireballDirection;
			this.fireballThrown = true;
			this.lastFireballTime = Time.time;

			this.throwSfx.Play();
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
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			
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
