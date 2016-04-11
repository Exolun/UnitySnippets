using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public int Speed = 150;
	public GameObject RestartPanel;

	void Start () {
		var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
		rigidBody.AddForce (new Vector2 (this.Speed, this.Speed));
	}
	
	void Restart()
	{
		var scoreLabel2 = GameObject.FindGameObjectWithTag("P2Score");
		var text = scoreLabel2.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = "0";

		var scoreLabel1 = GameObject.FindGameObjectWithTag("P1Score");
		text = scoreLabel1.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = "0";

		this.gameObject.transform.position = new Vector3(-3.0f, -1.0f, 0);
		var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
		rigidBody.velocity = new Vector2();
		rigidBody.AddForce (new Vector2 (this.Speed, this.Speed));
		this.RestartPanel.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Scoring Zone 1")) {
			this.gameObject.transform.position = new Vector3(-3.0f, -1.0f, 0);				
			
			var scoreLabel2 = GameObject.FindGameObjectWithTag("P2Score");
			var text = scoreLabel2.GetComponentInChildren<UnityEngine.UI.Text>();
			text.text = (int.Parse(text.text) + 1).ToString();

			var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
			rigidBody.velocity = new Vector2();

			if(int.Parse(text.text) > 4){
				this.RestartPanel.SetActive(true);
			}
			else{
				rigidBody.AddForce (new Vector2 (this.Speed, this.Speed));
			}
		} 
		else if (other.gameObject.CompareTag ("Scoring Zone 2")) {
			this.gameObject.transform.position = new Vector3(3.0f, 1.0f, 0);
			var scoreLabel1 = GameObject.FindGameObjectWithTag("P1Score");
			var text = scoreLabel1.GetComponentInChildren<UnityEngine.UI.Text>();
			text.text = (int.Parse(text.text) + 1).ToString();

			var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
			rigidBody.velocity = new Vector2();

			if(int.Parse(text.text) > 4){
				this.RestartPanel.SetActive(true);
			}
			else{
				rigidBody.AddForce (new Vector2 (-this.Speed, -this.Speed));
			}
		}
	}

	void OnCollisionExit2D (Collision2D other){
		var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
		rigidBody.velocity *= 1.05f;
		var audio = this.gameObject.GetComponent<AudioSource> ();
		audio.Play();

		var particles = GameObject.FindWithTag ("Particles");
		particles.transform.position = this.transform.position;
		particles.GetComponent<ParticleSystem> ().Play();
	}
}
