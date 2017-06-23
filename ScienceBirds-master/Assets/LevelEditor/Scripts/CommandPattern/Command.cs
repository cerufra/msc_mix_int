using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Command
{
    public abstract void Execute();	
}

public abstract class UndoableCommand : Command
{
    public abstract void Undo();
}