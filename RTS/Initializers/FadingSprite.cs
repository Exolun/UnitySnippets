using UnityEngine;
using System.Collections;

/// <summary>
/// A simple gameobject with a sprite that appears at full opacity,
/// then fades to invisible and destroys itself over a specified duration.
/// </summary>
public class FadingSprite : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private float opacity = 1.0f;

    public float FadeDuration = 1.5f;
    
	void Start () {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();        
	}
	
	void Update () {
        this.opacity -= Time.deltaTime * (1.0f / FadeDuration);
        var color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.opacity);

        this.spriteRenderer.color = color;

        if(opacity <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
