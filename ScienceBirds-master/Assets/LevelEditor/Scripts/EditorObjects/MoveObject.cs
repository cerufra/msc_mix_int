using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 posicaoAntesDeArrastar = new Vector3();
    bool instanciado = false;

    void Awake()
    {
        Destroy(gameObject.GetComponent<InstantiateObject>());
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        posicaoAntesDeArrastar = gameObject.transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        string[] objName = gameObject.name.Split('_');
        long objectIndex = long.Parse(objName[1]);
        ChangeObjectPositionCommand cbp = new ChangeObjectPositionCommand(objectIndex, posicaoAntesDeArrastar);
        if (instanciado)
            LevelEditorManager.commandManager.ExecuteCommand(cbp);
        else
            instanciado = true;
    }


}