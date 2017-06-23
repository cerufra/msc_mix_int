using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlockCommand : UndoableCommand
{
    private long blockIndex;
    ELevel.EObject block;

    public RemoveBlockCommand(long index)
    {
        blockIndex = index;
    }

    public override void Execute()
    {
        block = ELevel.instance.blocksEditor[blockIndex];
        block.gameObject.SetActive(false);
        ELevel.instance.blocksEditor.Remove(blockIndex);
    }

    public override void Undo()
    {
        block.gameObject.SetActive(true);
        ELevel.instance.blocksEditor.Add(blockIndex, block);
    }
}
