using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject target;
	public float followDistance = .25f;
	public float cameraVelocity = 2;
	
	private float originalDistance;	
	
	void Update () {
		this.gameObject.transform.position = new Vector3 (target.gameObject.transform.position.x, target.gameObject.transform.position.y, this.gameObject.transform.position.z);
	}
}
