using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePigCommand : UndoableCommand
{
    private long pigIndex;
    ELevel.EObject pig;

    public RemovePigCommand(long index)
    {
        pigIndex = index;
    }

    public override void Execute()
    {
        pig = ELevel.instance.pigsEditor[pigIndex];
        pig.gameObject.SetActive(false);
        ELevel.instance.pigsEditor.Remove(pigIndex);
    }

    public override void Undo()
    {
        pig.gameObject.SetActive(true);
        ELevel.instance.pigsEditor.Add(pigIndex, pig);
    }
}
