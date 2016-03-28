using UnityEngine;
using System.Collections;

/// <summary>
/// A Unit that's selectable
/// 
/// Requires an object assigned to serve as the selection highlighting. Recommend adding a child object
/// to whatever object your unit is with a halo or selection sprite for this class to toggle on/off to visually
/// indicate selection.
/// </summary>
public class SelectableUnit : MonoBehaviour {
    private bool isSelected = false;

    /// <summary>
    /// Object to toggle on/off for highlighting
    /// </summary>
    public GameObject SelectionHighlighting;

    void Start()
    {
        this.SelectionHighlighting.SetActive(false);
    }

    public bool IsSelected() {
        return this.isSelected;
    }

    public void Select() {
        this.isSelected = true;
        this.SelectionHighlighting.SetActive(true);
    }

    public void Deselect() {
        this.isSelected = false;
        this.SelectionHighlighting.SetActive(false);
    }
}
