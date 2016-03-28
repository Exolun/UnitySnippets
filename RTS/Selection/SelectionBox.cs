using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// A selection area for multi-unit selection
/// 
/// Assumes you're using a small mostly transparent monocolored sprite for the SelectionFrame,
/// which is a child of a root canvas with default settings.
/// </summary>
public class SelectionBox : MonoBehaviour {
    private bool mouseDown = false;
    private RectTransform selectionBoxTransform;

    //Screen position of the mousedown event
    private Vector2 initialMouseDownPosition;
    //Canvas position of the selecton box's first click
    private Vector2 initialSelectionBoxPosition;

    /// <summary>
    /// An object to use to provide the selection box around objects.
    /// 
    /// This should be something like a plain mostly transparent square (true square, symmetrical vertically / horizontally)
    /// </summary>
    public GameObject SelectionFrame;

    /// <summary>
    /// The tag to use for finding selectable objects
    /// </summary>
    public string SelectableUnitTag = "Unit";
    
    
	void Start () {
        this.SelectionFrame.SetActive(false);
        this.selectionBoxTransform = this.SelectionFrame.GetComponent<RectTransform>();
    }    
    
	void Update () {
        //Initial click, start dragging
        if (Input.GetMouseButton(0) && !this.mouseDown)
        {
            this.mouseDown = true;
            this.initialMouseDownPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // This calculation for the selection box position assumes that the SelectionFrame you used is the child of a canvas with the default
            // positioning relative to the entire screen (spans the whole screen).  Consequently we must offset the mouse position to
            // match the expected canvas coordinate, since the origin becomes the center of the screen due to the parent canvas.
            this.initialSelectionBoxPosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
            this.SelectionFrame.SetActive(true);
            this.selectionBoxTransform.localScale = new Vector3(1, 1, 1);
            this.selectionBoxTransform.anchoredPosition = this.initialSelectionBoxPosition;
        }
        //Drag finished, make selection
        else if (this.mouseDown && !Input.GetMouseButton(0))
        {
            var currentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            this.SelectionFrame.SetActive(false);
            this.mouseDown = false;

            if(Vector2.Distance(currentPos, initialMouseDownPosition) > 3)
            {
                this.doSelection(this.initialMouseDownPosition, currentPos);
            }
        }
        //Update selection box during drag
        else if (Input.GetMouseButton(0) && this.mouseDown)
        {
            var delta = (this.initialMouseDownPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            this.selectionBoxTransform.anchoredPosition = this.initialSelectionBoxPosition - delta / 2;            
            this.selectionBoxTransform.localScale = new Vector3(delta.x, delta.y, 1);         
        }
    }

    private void doSelection(Vector2 screenStartPos, Vector2 screenEndPos)
    {
        var units = GameObject.FindGameObjectsWithTag(this.SelectableUnitTag);
        List<GameObject> selectedUnits = new List<GameObject>();

        foreach (var unit in units)
        {
            SelectableUnit selectableObj = unit.GetComponent(typeof(SelectableUnit)) as SelectableUnit;
            if(selectableObj != null)
            {
                var unitScreen = Camera.main.WorldToScreenPoint(unit.transform.position);

                var delta = (this.initialMouseDownPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                var corner = new Vector2(screenEndPos.x > screenStartPos.x ? screenStartPos.x : screenEndPos.x, 
                                         screenEndPos.y > screenStartPos.y ? screenStartPos.y : screenEndPos.y);
                var rect = new Rect(corner, new Vector2(Mathf.Abs(delta.x), Mathf.Abs(delta.y)));                

                if (rect.Contains(unitScreen))
                {
                    selectableObj.Select();
                    selectedUnits.Add(unit);
                }
                else
                {
                    selectableObj.Deselect();
                }
            }
        }

        notifySelectionUpdated(selectedUnits);
    }



    #region ObservableSelection Implementation    
    private void notifySelectionUpdated(IEnumerable<GameObject> selectedObjects)
    {
        foreach (var selectionUpdatedHandler in subscribers.Values)
        {
            selectionUpdatedHandler(selectedObjects);
        }
    }

    private Dictionary<int, Action<IEnumerable<GameObject>>> subscribers = new Dictionary<int, Action<IEnumerable<GameObject>>>();
    public void Subscribe(GameObject subscriber, Action<IEnumerable<GameObject>> onSelectionUpdated)
    {
        this.subscribers[subscriber.GetInstanceID()] = onSelectionUpdated;
    }

    public void Unsubscribe(GameObject subscriber)
    {
        if (this.subscribers.ContainsKey(gameObject.GetInstanceID()))
        {
            this.subscribers.Remove(gameObject.GetInstanceID());
        }
    }
    #endregion
}
