using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PlayerController : MonoBehaviour {
	public float speed = .1f;
	public bool IsDead = false;


	private Animator anim;
	private Rigidbody2D rBody;
	private bool facingRight = false;
	private bool inputLocked = false;

	private long unlockDuration = 0;
	private Stopwatch timer = new Stopwatch();
	private List<Behaviour> componentsToDisable = new List<Behaviour> ();

	private Vector3? preBlinkScale = null;
	private Vector2? blinkDirection = null;

	void Start () {
		//Cursor.visible = false;
		anim = GetComponent<Animator>();
		anim.SetBool("Walking", false);

		rBody = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate()
	{
		if (this.inputLocked == true) {			
			this.performBlinkIfApplicable();
			this.resetStateIfReady();
			return;		
		}

		Vector2 movementDir = this.HandleMovementInput();		
		this.HandleAttackInput(movementDir);
	}

	private void HandleAttackInput(Vector2 movement){
		if (this.inputLocked == true) {
			return;
		}

		if (Input.GetMouseButton(0)) {
			this.anim.SetBool ("Walking", false);

			if (movement.x != 0) {
				this.anim.Play ("HorizontalStrike");
				this.inputLocked = true;
				this.timer.Start ();
				this.unlockDuration = 200;
				this.activateBodyHitbox ();
				this.activateLegHitboxes();
			}
			else if(movement.y > 0){
				this.anim.Play ("VerticalStrike");
				this.inputLocked = true;
				this.timer.Start ();
				this.unlockDuration = 200;
				this.activateArmHitboxes ();
			}
			else if(movement.y < 0){
				this.anim.Play ("DownStrike");
				this.inputLocked = true;
				this.timer.Start ();
				this.unlockDuration = 200;
				this.activateLegHitboxes();
				this.activateBodyHitbox();
			}
			else {
				this.anim.Play ("ArmStrike");
				this.inputLocked = true;
				this.timer.Start ();
				this.unlockDuration = 200;
				this.activateArmHitboxes ();
			}
		} 
		else if (Input.GetMouseButton(1)) {
			if(movement.x == 0 && movement.y == 0){
				this.blinkDirection = this.facingRight ? -Vector2.right : Vector2.right;
			}
			else {
				this.blinkDirection = movement;
			}

			this.preBlinkScale = this.gameObject.transform.localScale;
			UnityEngine.Debug.Log(this.preBlinkScale);
			this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * .01f,
			                                                   this.gameObject.transform.localScale.y * .01f,
			                                                   this.gameObject.transform.localScale.z * .01f);
			this.unlockDuration = 250;
			this.timer.Start();
			this.inputLocked = true;
		}
	}

	private bool isOutOfBounds(){
		if (this.gameObject.transform.position .x > 17.5 || this.gameObject.transform.position.x < -17.8 ||
		    this.gameObject.transform.position.y > 11.2 || this.gameObject.transform.position.y < -15.8) {
			return true;
		}

		return false;
	}

	private void performBlinkIfApplicable(){
		if (this.blinkDirection != null) {	
			if(!this.isOutOfBounds()){
				this.gameObject.transform.Translate (new Vector3 (((Vector2)this.blinkDirection).x * Time.deltaTime*10, ((Vector2)this.blinkDirection).y * Time.deltaTime*10, 0));
			}
		}
	}

	private void activateArmHitboxes(){		
		var arms = GameObject.FindGameObjectsWithTag("Arms");
		foreach (var arm in arms) {
			var armCollider = arm.GetComponent<BoxCollider2D>();
			armCollider.enabled = true;
			this.componentsToDisable.Add((Behaviour)armCollider);
		}
	}

	private void activateBodyHitbox(){
		var body = GameObject.FindGameObjectWithTag ("Body");
		var collider = body.GetComponent<BoxCollider2D> ();
		collider.enabled = true;
		this.componentsToDisable.Add((Behaviour)collider);
	}

	private void activateLegHitboxes(){
		var legs = GameObject.FindGameObjectsWithTag("Arms");
		foreach (var arm in legs) {
			var legCollider = arm.GetComponent<BoxCollider2D>();
			legCollider.enabled = true;
			this.componentsToDisable.Add((Behaviour)legCollider);
		}
	}

	private void deactivateWaitingComponents(){
		for (int i = this.componentsToDisable.Count - 1; i >= 0; i--) {
			var component = this.componentsToDisable[i];
			component.enabled = false;
			this.componentsToDisable.Remove(component);
		}
	}

	private void resetStateIfReady(){
		if(timer.ElapsedMilliseconds > unlockDuration){
			//Special case, if they are blinking, and the key is still held. renew it.
			if(this.blinkDirection != null && Input.GetMouseButton(1)){
				this.unlockDuration += 250;
				return;
			}

			this.inputLocked = false;
			this.timer.Reset();
			timer.Stop();
			this.unlockDuration = 0;

			if (this.componentsToDisable.Count > 0) {
				this.deactivateWaitingComponents();
			}

			if(this.blinkDirection != null || this.preBlinkScale != null){
				this.finishBlink();
			}
		}
	}

	private void finishBlink(){
		this.blinkDirection = null;
		this.gameObject.transform.localScale = (Vector3)this.preBlinkScale;
		this.preBlinkScale = null;
	}

	private Vector2 HandleMovementInput(){
		if (this.inputLocked == true) {
			return new Vector2();
		}

		Vector2 movementDir = new Vector2();
		
		if(Input.GetKey(KeyCode.W)) {
			movementDir += Vector2.up;
		}
		
		if(Input.GetKey(KeyCode.A)) {
			movementDir += -Vector2.right;
			if(!facingRight){
				flip();
			}
		}
		
		if(Input.GetKey(KeyCode.S)) {
			movementDir += -Vector2.up;
		}
		
		if(Input.GetKey(KeyCode.D)) {
			movementDir += Vector2.right;
			if(facingRight){
				flip();
			}
		}
		
		if (movementDir.x != 0 || movementDir.y != 0)
		{
			
			this.rBody.AddForce(movementDir * speed);
			//this.transform.position +=  new Vector3(movementDir.normalized.x*speed, movementDir.normalized.y*speed, 0);
			
			//rigidbody2D.AddForce((Vector2)movementDir * speed);
			anim.SetBool("Walking", true);
		}
		else 
		{
			anim.SetBool("Walking", false);
		}

		//If the player has moved out of bounds.  terminate movement.
		if (this.isOutOfBounds()) {
			movementDir = new Vector3();
		}

		return movementDir;
	}

	public void Death()
	{
		if (this.blinkDirection != null) {
			this.finishBlink();
		}

		this.deactivateWaitingComponents ();
		this.inputLocked = true;
		this.unlockDuration = long.MaxValue;
		this.IsDead = true;
		anim.Play("Death");

		var UI = GameObject.Find ("UI");
		UI.SendMessage ("ShowDeathDialog");
	}

	private void flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
