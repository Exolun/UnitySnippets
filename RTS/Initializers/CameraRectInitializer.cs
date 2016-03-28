using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The Camera Rectangle is a transparent sprite overlaying the minimap that represents the
/// current location of the camera on the minimap.
/// 
/// Although we don't need to move it because it can simply be attached to the camera, we must update the 
/// rectangle to match the screen size's approximate coverage.
/// </summary>
public class CameraRectInitializer : MonoBehaviour {
    private float screenWidth = Screen.width;
    private float screenHeight = Screen.height;

    void Start () {
        this.updateScale();
        var renderer = this.gameObject.GetComponent<SpriteRenderer>();
        var rdr = renderer as SpriteRenderer;
        rdr.enabled = true;
	}
	
	void Update () {
        if (Screen.width != this.screenWidth || Screen.height != screenHeight)
        {
            this.updateScale();
            this.screenHeight = Screen.height;
            this.screenWidth = Screen.width;
        }
    }

    private void updateScale()
    {
        var scale = new Vector3(Screen.width*.01f, Screen.height*.01f, 0);
        this.gameObject.transform.localScale = scale;
    }
}
