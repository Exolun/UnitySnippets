using UnityEngine;
using System.Collections;
using Selection;

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

    /// <summary>
    /// Tag used to determine if the cursor is hovering over an enemy
    /// </summary>
    public string EnemyTag = "Enemy";

    private CursorHoverHightlighter highlighter = new CursorHoverHightlighter();
    private bool attackModeSetByUser = false;

    public bool AttackModeWasSetByUser()
    {
        return this.attackModeSetByUser;
    }

    void Start()
    {
        Cursor.SetCursor(this.DefaultTexture, this.Hotspot, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
    
    void Update()
    {
        if(this.highlighter != null && CurrentSelection.GetInstance().SelectedUnits.Count > 0)
        {
            this.highlighter.Update(this);
        }
    }   

    public void SetAttack(bool setByUser = true)
    {
        if (this.currentStyle != CursorStyle.Attack)
        {
            Cursor.SetCursor(this.AttackTexture, this.Hotspot, CursorMode.ForceSoftware);
            this.currentStyle = CursorStyle.Attack;
            this.attackModeSetByUser = setByUser;
        }
    }

    public void SetDefault(bool setByUser = true)
    {
        if (this.currentStyle != CursorStyle.Default)
        {
            Cursor.SetCursor(this.DefaultTexture, this.Hotspot, CursorMode.ForceSoftware);
            this.currentStyle = CursorStyle.Default;
            this.attackModeSetByUser = setByUser;
        }
    }

    public CursorStyle GetStyle()
    {
        return this.currentStyle;
    }
}
