using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public KeyCode UpKey = KeyCode.W;
	public KeyCode DownKey = KeyCode.S;
	public float MovementSpeed = .1f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var transform = this.gameObject.transform;

		if (Input.GetKey (this.UpKey)) {
			var upMovement = new Vector3(0, this.MovementSpeed, 0);
			transform.position =  transform.position + upMovement;		
		}
		else if (Input.GetKey (this.DownKey)) {		
			var downMovement = new Vector3(0, -this.MovementSpeed, 0);
			transform.position =  transform.position + downMovement;
		}
	}
}
