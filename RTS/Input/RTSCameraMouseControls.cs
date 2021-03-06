﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to a camera pointed down at a map (assumes positive Z axis camera position, Z is the zoom axis)
/// to give it RTS style mouse movement (pan camera by putting mouse on edges of screen, mouse wheel to zoom)
/// </summary>
public class RTSCameraMouseControls : MonoBehaviour {

    /// <summary>
    /// Size of the buffer at the edges of the screen.  E.G 10 = a 10 pixel buffer
    /// </summary>
    public int MovementBuffer = 10;

    /// <summary>
    /// Maximum value on the zoom axis to allow the camera to move
    /// </summary>
    public int MaxZoom = -50;

    /// <summary>
    /// Minimum value on the zoom axis to allow the camera to move
    /// </summary>
    public int MinZoom = -5;

    /// <summary>
    /// The speed at which the camera will pan
    /// </summary>
    public float PanningVelocity = .1f;

    /// <summary>
    /// The speed at which the camera scrolls
    /// </summary>
    public float ScrollVelocity = 50f;

    /// <summary>
    /// Top left of the map, defining the bounds of Min X and Max Y
    /// for camera panning.
    /// </summary>
    public Vector2 TopLeft = new Vector2(-5000, 5000);

    /// <summary>
    /// Bottom right of the map, defining the bounds of Max X and Min Y
    /// for camera panning.
    /// </summary>
    public Vector2 BottomRight = new Vector2(5000, -5000);

	void Update () {
        var mousePos = Input.mousePosition;
        var position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        if(mousePos.x >= Screen.width - this.MovementBuffer)
        {
            position.x += this.PanningVelocity;
        }

        if (mousePos.x <= this.MovementBuffer)
        {
            position.x -= this.PanningVelocity;
        }

        if(mousePos.y <= this.MovementBuffer)
        {
            position.y -= this.PanningVelocity;
        }

        if (mousePos.y >= Screen.height - this.MovementBuffer)
        {
            position.y += this.PanningVelocity;
        }

        //Update camera zoom
        position.z += Input.mouseScrollDelta.y * this.ScrollVelocity * Time.deltaTime;
        position.z  = Mathf.Clamp(position.z, this.MaxZoom, this.MinZoom);

        //Clamp X/Y positions to map bounds
        position.x = Mathf.Clamp(position.x, this.TopLeft.x, this.BottomRight.x);
        position.y = Mathf.Clamp(position.y, this.BottomRight.y, this.TopLeft.y);

        this.gameObject.transform.position = position;
    }
}
