using UnityEngine;
using System.Collections;

public class BodilyAttackController : MonoBehaviour {
	public int StrikingForce = 15000;
	public ParticleSystem CollisionParticle;

	private AudioSource sfx;

	void Start(){
		this.sfx = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			var yDelta = other.transform.position.y - transform.position.y;
			var xDelta = other.transform.position.x - transform.position.x;
			var attackDir = new Vector2 (xDelta, yDelta);
			attackDir.Normalize ();

			var otherRBody = other.GetComponent<Rigidbody2D>();
			otherRBody.AddForce(attackDir * this.StrikingForce);
			otherRBody.mass = otherRBody.mass * .75f;
			var shockwave = Instantiate(this.CollisionParticle);	
			shockwave.transform.position = other.transform.position;
			shockwave.Play();
			this.sfx.Play();
			Destroy(shockwave.gameObject, 1);
		}
	}
}
