using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Commands;

/// <summary>
/// This component receives and executes commands issued from an external source.
/// 
/// A command is an executable action carried out by the GameObject
/// </summary>
public class CommandReceiver : MonoBehaviour {

    private Queue<ICommand> commands = new Queue<ICommand>();
    private ICommand currentCommand = null;    
    	
	void Update () {        
        if (this.currentCommand == null && this.commands.Count > 0) {
            this.currentCommand = this.commands.Dequeue();
        }

        if (this.currentCommand != null) {
            if (this.currentCommand.IsComplete()) {
                this.currentCommand = null;
            }
            else
            {
                this.currentCommand.Do();
            }
        }
	}

    /// <summary>
    /// Append a command to be executed after all current commands
    /// are complete.
    /// </summary>
    /// <param name="command"></param>
    public void AppendCommand(ICommand command) {
        this.commands.Enqueue(command);
    }

    /// <summary>
    /// Clears all issued commands and sets the current command
    /// </summary>
    /// <param name="command"></param>
    public void SetCommand(ICommand command)
    {
        ClearCommands();
        this.currentCommand = command;
    }

    /// <summary>
    /// Clears all commands
    /// </summary>
    public void ClearCommands() {
        this.commands.Clear();
        this.currentCommand = null;
    }
}
