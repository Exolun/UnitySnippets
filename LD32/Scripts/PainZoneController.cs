using UnityEngine;
using System.Collections;

public class PainZoneController : MonoBehaviour {

	private AudioSource enemyDeathSfx;
	private AudioSource playerDeathSfx;	

	void Start () {
		var soundEffects = GetComponents<AudioSource> ();
		this.enemyDeathSfx = soundEffects [0];
		this.playerDeathSfx = soundEffects [1];
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			other.gameObject.SendMessage ("Death");
			this.enemyDeathSfx.Play ();
		} 
		else if (other.gameObject.CompareTag ("Player")) {
			var ctrlr = other.gameObject.GetComponent<PlayerController>();

			if(!ctrlr.IsDead){
				this.playerDeathSfx.Play ();				
				other.gameObject.SendMessage ("Death");
			}
		}
	}
}
