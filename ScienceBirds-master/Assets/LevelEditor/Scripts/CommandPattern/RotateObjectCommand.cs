using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectCommand : UndoableCommand {

    private long objectIndex;
    ELevel.EObject editorObject;

    public RotateObjectCommand(long index)
    {
        objectIndex = index;
    }

    public override void Execute()
    {
        if (ELevel.instance.blocksEditor.ContainsKey(objectIndex))
        {
            editorObject = ELevel.instance.blocksEditor[objectIndex];
        }
        else if (ELevel.instance.pigsEditor.ContainsKey(objectIndex))
        {
            editorObject = ELevel.instance.pigsEditor[objectIndex];
        }
        else
        {
            Debug.Log("Erro: Rotate object");
            return;
        }

        Rotate();
    }

    public override void Undo()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (editorObject.rotated90Degree)
        {
            editorObject.gameObject.transform.Rotate(new Vector3(0, 0, 1), -90);
            editorObject.rotated90Degree = false;
        }
        else
        {
            editorObject.gameObject.transform.Rotate(new Vector3(0, 0, 1), 90);
            editorObject.rotated90Degree = true;
        }
    }
}
