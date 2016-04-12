using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int HorizontalVelocity;
	public int JumpVelocity;
	public float Torque;

	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		this.rBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			this.rBody.AddForce(new Vector2(-this.HorizontalVelocity*this.rBody.mass, 0));
			//this.gameObject.transform.Rotate(new Vector3(0, 0, 1), 1);
			this.rBody.AddTorque(this.Torque);
		}

		if(Input.GetKey(KeyCode.D)){
			this.rBody.AddForce(new Vector2(this.HorizontalVelocity*this.rBody.mass, 0));
			this.rBody.AddTorque(-this.Torque);
			//this.gameObject.transform.Rotate(new Vector3(0, 0, -1), 1);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			this.rBody.AddForce(new Vector2(0, this.JumpVelocity*this.rBody.mass));
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Block")){
			var rb = collision.gameObject.GetComponent<Rigidbody2D>();
			rb.AddForce(new Vector2(collision.contacts[0].normal.x * -10,  collision.contacts[0].normal.y * -10));
			if(collision.relativeVelocity.magnitude > 10){
				collision.gameObject.SendMessage("Split");
			}
		}
	}
}
