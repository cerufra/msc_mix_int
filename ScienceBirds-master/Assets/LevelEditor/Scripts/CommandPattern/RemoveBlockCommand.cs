using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlockCommand : UndoableCommand
{

    private long blockIndex;
    private Transform prefab;
    ELevel.EObject block = new ELevel.EObject();

    public RemoveBlockCommand(long index)
    {
        blockIndex = index;
    }

    public override void Execute()
    {
        block = ELevel.instance.blocksEditor[blockIndex];
        if (prefab != null)
        {
            MonoBehaviour.Destroy(block.gameObject);
        }
        ELevel.instance.blocksEditor.Remove(blockIndex);
    }

    public override void Undo()
    {
        block.gameObject = MonoBehaviour.Instantiate(prefab.gameObject) as GameObject;
        MATERIALS mat = (MATERIALS)System.Enum.Parse(typeof(MATERIALS), ((BlockData)block.dados).material);
        block.gameObject.GetComponent<ABBlock>().SetMaterial(mat);
        MonoBehaviour.Destroy(block.gameObject.GetComponent("ABBlock"));
        MonoBehaviour.Destroy(block.gameObject.GetComponent("ABParticleSystem"));
        block.gameObject.GetComponent<Transform>().gameObject.AddComponent<InstantiateObject>();
        block.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        block.gameObject.name = "block_" + blockIndex;
        if (block.dados.type.Equals("TriangleHole"))
        {
            MonoBehaviour.Destroy(block.gameObject.GetComponent<PolygonCollider2D>());
            block.gameObject.AddComponent<BoxCollider2D>();
        }
        ELevel.instance.blocksEditor.Add(blockIndex, block);
    }
}
