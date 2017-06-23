using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPigCommand : UndoableCommand {

    private long pigIndex;
    private Transform prefab;
    ELevel.EObject pig = new ELevel.EObject();

    public AddPigCommand(string type, float rotation, float x, float y, Transform pref = null)
    {
        pig.dados = new OBjData(type, rotation, x, y);
        prefab = pref;
    }

    public override void Execute()
    {
        pigIndex = ELevel.instance.objNum++;

        if (prefab != null)
        {
            Vector3 foraDaTela = new Vector3(10, 10, -5);
            pig.gameObject = MonoBehaviour.Instantiate(prefab.gameObject, foraDaTela, Quaternion.identity) as GameObject;
            MonoBehaviour.Destroy(pig.gameObject.GetComponent("ABPig"));
            MonoBehaviour.Destroy(pig.gameObject.GetComponent("ABParticleSystem"));
            pig.gameObject.GetComponent<Transform>().gameObject.AddComponent<InstantiateObject>();
            pig.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            pig.gameObject.name = "pig_" + pigIndex;
        }

        ELevel.instance.pigsEditor.Add(pigIndex, pig);
    }

    public override void Undo()
    {
        if (prefab != null)
        {
            MonoBehaviour.Destroy(pig.gameObject);
        }
        ELevel.instance.blocksEditor.Remove(pigIndex);
    }
}
