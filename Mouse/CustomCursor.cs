using UnityEngine;
using System.Collections;

/// <summary>
/// Provides a custom game cursor
/// </summary>
public class CustomCursor : MonoBehaviour {
    /// <summary>
    /// Texture to use for the pointer
    /// </summary>
    public Texture2D Texture;

    /// <summary>
    /// Position in the texture where the click event should occur.
    /// </summary>
    public Vector2 Hotspot;
    
	void Start () {
        Cursor.SetCursor(this.Texture, this.Hotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
}
