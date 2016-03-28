using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleUnitSelector : MonoBehaviour {
    /// <summary>
    /// The tag to use for finding selectable objects
    /// </summary>
    public string SelectableUnitTag = "Unit";

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var units = GameObject.FindGameObjectsWithTag(this.SelectableUnitTag);
            SelectableUnit hit = null;
            List<SelectableUnit> allSelectables = new List<SelectableUnit>();

            foreach (var unit in units)
            {
                var selectable = unit.GetComponent<SelectableUnit>();
                allSelectables.Add(selectable);
                if(selectable != null)
                {
                    var collider = unit.GetComponent<BoxCollider2D>();
                    if (collider != null)
                    {
                        var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

                        if (collider.bounds.Contains(target))
                        {
                            selectable.Select();
                            hit = selectable;
                        }
                    }
                }
            }

            
            foreach (var selectable in allSelectables)
            {
                if (selectable != hit)
                    selectable.Deselect();
            }
            
        }
	}
}
