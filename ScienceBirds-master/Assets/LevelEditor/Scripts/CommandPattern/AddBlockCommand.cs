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
            Vector3 posicao = new Vector3(block.dados.x, block.dados.y, -5);
            block.gameObject = MonoBehaviour.Instantiate(prefab.gameObject, posicao, Quaternion.identity) as GameObject;
            MATERIALS mat = (MATERIALS)System.Enum.Parse(typeof(MATERIALS), ((BlockData)block.dados).material);
            block.gameObject.GetComponent<ABBlock>().SetMaterial(mat);
            MonoBehaviour.Destroy(block.gameObject.GetComponent("ABBlock"));
            MonoBehaviour.Destroy(block.gameObject.GetComponent("ABParticleSystem"));
            block.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            block.gameObject.name = "block_" + blockIndex;
            if (block.dados.type.Equals("TriangleHole"))
            {
                MonoBehaviour.Destroy(block.gameObject.GetComponent<PolygonCollider2D>());
                block.gameObject.AddComponent<BoxCollider2D>();
            }
            if(block.dados.rotation != 0)
            {
                block.rotated90Degree = true;
                block.gameObject.transform.Rotate(new Vector3(0, 0, 1), 90);
            }
            block.gameObject.AddComponent<InstantiateObject>();
        }

        ELevel.instance.blocksEditor.Add(blockIndex, block);
    }

    public override void Undo()
    {
        //Debug.Log("Undo add block");
        if (!ELevel.instance.blocksEditor.ContainsKey(blockIndex))
            return;

        if (prefab != null)
        {
            MonoBehaviour.Destroy(block.gameObject);
        }
        ELevel.instance.blocksEditor.Remove(blockIndex);
    }
}
