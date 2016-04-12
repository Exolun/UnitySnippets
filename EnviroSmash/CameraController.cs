using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject target;
	public float followDistance = .25f;
	public float cameraVelocity = 2;

	private float originalDistance;
    
	void Start () {
		originalDistance = Mathf.Abs ((this.gameObject.transform.position - target.gameObject.transform.position).magnitude);
	}
	
	void Update () {
		var diff = (this.gameObject.transform.position - target.gameObject.transform.position);
		var curDistance = Mathf.Abs ((this.gameObject.transform.position - target.gameObject.transform.position).magnitude);
		var delta = Mathf.Abs (this.originalDistance - curDistance);

		if (Mathf.Abs(delta) > followDistance) {
			var desiredPosition = diff.normalized * this.followDistance;
			this.gameObject.transform.Translate(-desiredPosition.x* (cameraVelocity * delta / followDistance) * Time.smoothDeltaTime, -desiredPosition.y*  (cameraVelocity * delta / followDistance)* Time.smoothDeltaTime, 0);
		}
	}
}
