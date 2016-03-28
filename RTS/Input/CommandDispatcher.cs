using UnityEngine;
using System.Collections;
using System;
using Commands;
using System.Collections.Generic;

public class CommandDispatcher : MonoBehaviour {
    /// <summary>
    /// The tag to use for finding selectable objects
    /// </summary>
    public string SelectableUnitTag = "Unit";

    /// <summary>
    /// Marker to use when a movement is being made
    /// </summary>
    public GameObject MovementMarker;    
	
	void Update () {
        if (Input.GetMouseButtonUp(1))
        {
            this.issueRightClickCommand(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            this.issueStopCommand();
        }
	}

    private void issueStopCommand()
    {
        Dictionary<GameObject, CommandReceiver> selectedCommandableUnits = getSelectedCommandableUnits();

        if (selectedCommandableUnits.Count > 0)
        {
            foreach (var commandReceiver in selectedCommandableUnits.Values)
            {
                commandReceiver.ClearCommands();
            }
        }

    }

    private void issueRightClickCommand(bool appendCommand)
    {
        Dictionary<GameObject, CommandReceiver> selectedCommandableUnits = getSelectedCommandableUnits();

        if (selectedCommandableUnits.Count > 0)
        {
            var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            target.z = 0;
            issueMoveCommand(selectedCommandableUnits, target, appendCommand);
        }
    }

    private Dictionary<GameObject, CommandReceiver> getSelectedCommandableUnits()
    {
        var units = GameObject.FindGameObjectsWithTag(this.SelectableUnitTag);
        Dictionary<GameObject, CommandReceiver> selectedCommandableUnits = new Dictionary<GameObject, CommandReceiver>();

        foreach (var unit in units)
        {
            var selectionComponent = unit.GetComponent<SelectableUnit>();
            if (selectionComponent.IsSelected())
            {
                var commandReceiver = unit.GetComponent<CommandReceiver>();
                if (commandReceiver != null)
                {
                    selectedCommandableUnits[unit] = commandReceiver;
                }
            }
        }

        return selectedCommandableUnits;
    }

    private void issueMoveCommand(Dictionary<GameObject, CommandReceiver> selectableCommandableUnits, Vector3 target, bool appendCommand)
    {
        var avgPos = this.getAveragePosition(selectableCommandableUnits.Keys);

        foreach (var objCommandPair in selectableCommandableUnits)
        {
            var gameObj = objCommandPair.Key;
            var commandRec = objCommandPair.Value;
            var positionDelta = gameObj.transform.position - avgPos;
            var config = gameObj.GetComponent<UnitCommandConfig>() as UnitCommandConfig;

            if (appendCommand)
            {
                commandRec.AppendCommand(new TurnAndMoveCommand(gameObj, config.GetForward, 
                                        target + positionDelta, gameObj.transform.forward, config.TurningSpeed, config.MovementSpeed));
            }
            else
            {
                commandRec.SetCommand(new TurnAndMoveCommand(gameObj, config.GetForward,
                                        target + positionDelta, gameObj.transform.forward, config.TurningSpeed, config.MovementSpeed));
            }
        }

        this.showMovementMarker(target);
    }

    private Vector3 getAveragePosition(ICollection<GameObject> objects)
    {
        Vector3 vecSum = new Vector3();

        foreach (var obj in objects)
        {
            vecSum += obj.transform.position;
        }

        return vecSum / objects.Count;
    }

    private void showMovementMarker(Vector3 target)
    {
        Instantiate(this.MovementMarker, target, new Quaternion());
    }
}
