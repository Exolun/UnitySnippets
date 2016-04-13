using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

	public Vector3 direction = new Vector3();
	public float speed = 2f;
	public ParticleSystem CollisionParticle;
	public int StrikingForce;
	public GameObject fireComplex;
	public float Duration;
	public float FollowDuration;

	private GameObject player;

	private float startTime;

	void Start () {
		this.startTime = Time.time;
		this.player = GameObject.Find ("Player");
	}
	
	void Update () {
		if (Time.time > this.startTime + Duration) {
			Destroy (this.gameObject);
		} 
		else {
			if(Time.time < this.startTime + FollowDuration){
				this.adjustDirection();
			}
			this.gameObject.transform.Translate (direction * speed * Time.deltaTime);
		}
	}

	private void adjustDirection(){
		var newDir = this.player.transform.position - this.gameObject.transform.position;
		newDir.Normalize ();
		this.direction = newDir;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			this.fireComplex.SetActive(false);
			var yDelta = other.transform.position.y - transform.position.y;
			var xDelta = other.transform.position.x - transform.position.x;
			var attackDir = new Vector2 (xDelta, yDelta);
			attackDir.Normalize ();
			
			var otherRBody = other.GetComponent<Rigidbody2D>();
			otherRBody.AddForce(attackDir * this.StrikingForce);
			var shockwave = Instantiate(this.CollisionParticle);	
			shockwave.transform.position = other.transform.position;
			shockwave.Play();
			GetComponent<AudioSource> ().Play();
			Destroy(GetComponent<BoxCollider2D>());
			Destroy(shockwave, 1);
			Destroy(this.gameObject, 2);
			this.direction = new Vector3();
		}
	}
}
