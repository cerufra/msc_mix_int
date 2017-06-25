using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectPositionCommand : UndoableCommand {

    private long objectIndex;
    private Vector3 posicaoAntes = new Vector3();
    ELevel.EObject editorObject = new ELevel.EObject();

    public ChangeObjectPositionCommand(long index, Vector3 antes)
    {
        objectIndex = index;
        posicaoAntes = antes;
    }

    public override void Execute()
    {
        if(ELevel.instance.blocksEditor.ContainsKey(objectIndex))
        {
            editorObject = ELevel.instance.blocksEditor[objectIndex];
        }
        else if (ELevel.instance.pigsEditor.ContainsKey(objectIndex))
        {
            editorObject = ELevel.instance.pigsEditor[objectIndex];
        }
        else
        {
            Debug.Log("Erro: Change object position");
            return;
        }
    }

    public override void Undo()
    {
        editorObject.gameObject.transform.position = posicaoAntes;
    }
}
