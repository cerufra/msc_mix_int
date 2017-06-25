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
            Vector3 posicao = new Vector3(pig.dados.x, pig.dados.y, -5);
            pig.gameObject = MonoBehaviour.Instantiate(prefab.gameObject, posicao, Quaternion.identity) as GameObject;
            pig.gameObject.name = "pig_" + pigIndex;
            MonoBehaviour.Destroy(pig.gameObject.GetComponent("ABPig"));
            MonoBehaviour.Destroy(pig.gameObject.GetComponent("ABParticleSystem"));
            if(pig.dados.rotation != 0)
            {
                pig.gameObject.transform.Rotate(new Vector3(0, 0, 1), pig.dados.rotation);
                pig.rotated90Degree = true;
            }
            pig.gameObject.AddComponent<InstantiateObject>();
            pig.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        ELevel.instance.pigsEditor.Add(pigIndex, pig);
    }

    public override void Undo()
    {
        if (!ELevel.instance.pigsEditor.ContainsKey(pigIndex))
            return;

        if (prefab != null)
        {
            MonoBehaviour.Destroy(pig.gameObject);
        }
        ELevel.instance.pigsEditor.Remove(pigIndex);
    }
}
