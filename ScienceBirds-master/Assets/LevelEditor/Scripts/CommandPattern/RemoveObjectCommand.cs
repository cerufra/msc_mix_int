using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectCommand : Command {

    private long objIndex;
    ELevel.EObject editorObject;

    public RemoveObjectCommand(long index)
    {
        objIndex = index;
    }

    public override void Execute()
    {

        if (ELevel.instance.pigsEditor.ContainsKey(objIndex))
        {
            editorObject = ELevel.instance.pigsEditor[objIndex];
            MonoBehaviour.Destroy(editorObject.gameObject);
            ELevel.instance.pigsEditor.Remove(objIndex);
        }
        else if (ELevel.instance.blocksEditor.ContainsKey(objIndex))
        {
            editorObject = ELevel.instance.blocksEditor[objIndex];
            MonoBehaviour.Destroy(editorObject.gameObject);
            ELevel.instance.blocksEditor.Remove(objIndex);
        }

    }
}
