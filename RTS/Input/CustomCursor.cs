using UnityEngine;
using System.Collections;

/// <summary>
/// Provides a custom game cursor
/// </summary>
public class CustomCursor : MonoBehaviour
{
    public enum CursorStyle
    {
        Attack,
        Default
    }

    private CursorStyle currentStyle = CursorStyle.Default;

    /// <summary>
    /// Texture to use for the pointer
    /// </summary>
    public Texture2D DefaultTexture;

    /// <summary>
    /// Texture to use for the pointer
    /// </summary>
    public Texture2D AttackTexture;

    /// <summary>
    /// Position in the texture where the click event should occur.
    /// </summary>
    public Vector2 Hotspot;

    void Start()
    {
        Cursor.SetCursor(this.DefaultTexture, this.Hotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }

    public void SetAttack()
    {
        if (this.currentStyle != CursorStyle.Attack)
        {
            Cursor.SetCursor(this.AttackTexture, this.Hotspot, CursorMode.ForceSoftware);
            this.currentStyle = CursorStyle.Attack;
        }
    }

    public void SetDefault()
    {
        if (this.currentStyle != CursorStyle.Default)
        {
            Cursor.SetCursor(this.DefaultTexture, this.Hotspot, CursorMode.ForceSoftware);
            this.currentStyle = CursorStyle.Default;
        }
    }

    public CursorStyle GetStyle()
    {
        return this.currentStyle;
    }
}
