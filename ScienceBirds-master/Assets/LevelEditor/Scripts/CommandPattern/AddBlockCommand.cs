using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockCommand : UndoableCommand {

    private long blockIndex;
    private Transform prefab;
    ELevel.EObject block = new ELevel.EObject();

    public AddBlockCommand(string type, float rotation, float x, float y, string material, Transform pref = null)
    {
        block.dados = new BlockData(type, rotation, x, y, material);
        prefab = pref;
    }

    public override void Execute()
    {
        blockIndex = ELevel.instance.objNum++;

        if (prefab != null)
        {
            Vector3 foraDaTela = new Vector3(10, 10, -5);
            block.gameObject = MonoBehaviour.Instantiate(prefab.gameObject, foraDaTela, Quaternion.identity) as GameObject;
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
        }

        ELevel.instance.blocksEditor.Add(blockIndex, block);
    }

    public override void Undo()
    {
        if (prefab != null)
        {
            MonoBehaviour.Destroy(block.gameObject);
        }
        ELevel.instance.blocksEditor.Remove(blockIndex);
    }
}
