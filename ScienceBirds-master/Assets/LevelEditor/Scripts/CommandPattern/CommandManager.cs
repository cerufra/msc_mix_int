using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager
{
    private Stack commandStack = new Stack();

    public void ExecuteCommand(Command cmd)
    {
        //Debug.Log("Command: " + cmd.GetType());
        cmd.Execute();
        if (cmd is UndoableCommand)
        {
            commandStack.Push(cmd);
            //Debug.Log(commandStack.Count);
        }
    }

    public void Undo()
    {
        if (commandStack.Count > 0)
        {
            UndoableCommand cmd = (UndoableCommand)commandStack.Pop();
            cmd.Undo();
            //Debug.Log("Undo: " + cmd.GetType());
        }
    }
}