using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the control script for a block that smashes into smaller blocks when struck.
/// (Split is invoked externally by the destroyer of it)
/// </summary>
public class BlockController : MonoBehaviour {
	public GameObject NextSize = null;

	public void Split()
	{
		if (this.NextSize == null) {
			var particles = GameObject.Find("Particles");
			particles.transform.position = this.gameObject.transform.position;
			particles.GetComponent<ParticleSystem>().Play();
			Destroy (this.gameObject, .2f);
		} 
		else {
			var block1 = Instantiate(NextSize);
			block1.gameObject.transform.position = this.gameObject.transform.position;
			block1.gameObject.transform.Translate(new Vector3(.5f, .5f));

			var block2 = Instantiate(NextSize);
			block2.gameObject.transform.position = this.gameObject.transform.position;
			block2.gameObject.transform.Translate(new Vector3(.5f, -.5f));

			var block3 = Instantiate(NextSize);
			block3.gameObject.transform.position = this.gameObject.transform.position;
			block3.gameObject.transform.Translate(new Vector3(-.5f, .5f));

			var block4 = Instantiate(NextSize);
			block4.gameObject.transform.position = this.gameObject.transform.position;
			block4.gameObject.transform.Translate(new Vector3(-.5f, -.5f));

			Destroy(this.gameObject);
		}
	}	
}
