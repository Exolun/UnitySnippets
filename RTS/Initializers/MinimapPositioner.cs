using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Positions a minimap UI object in the bottom-right corner
/// 
/// Assumes a canvas with the default setting as the parent of the minimap
/// </summary>
public class MinimapPositioner : MonoBehaviour
{
    public GameObject MinimapImage;

    /// <summary>
    /// Width of the minimap UI object
    /// </summary>
    public float Width = 200;

    /// <summary>
    /// Height of the minimap UI object
    /// </summary>
    public float Height = 200;

    public float VerticalPadding = 20;
    public float HorizontalPadding = 20;

    private float screenWidth = Screen.width;
    private float screenHeight = Screen.height;

    void Start()
    {
        this.setPosition();
        this.MinimapImage.SetActive(true);
    }

    private void setPosition()
    {
        this.gameObject.transform.position = new Vector3(Screen.width - Width / 2 - HorizontalPadding, Height / 2 + VerticalPadding, 0);
    }

    void Update()
    {
        if (Screen.width != this.screenWidth || Screen.height != screenHeight)
        {
            setPosition();
            this.screenHeight = Screen.height;
            this.screenWidth = Screen.width;
        }
    }
}
