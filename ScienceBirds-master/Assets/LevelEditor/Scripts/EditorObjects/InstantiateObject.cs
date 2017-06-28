using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateObject : MonoBehaviour {

    private Vector3 mousePosition;
    private Color tmpColor;

    void Awake()
    {
        if (ELevel.instance.loadingLevelFromFile)
        {
            gameObject.AddComponent<MoveObject>();
        }else
        {
            tmpColor = gameObject.GetComponentInParent<SpriteRenderer>().color;
            tmpColor.a = 0.55f;
            gameObject.GetComponentInParent<SpriteRenderer>().color = tmpColor;
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (ELevel.instance.loadingLevelFromFile)
            return;

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = -5;
        transform.position = mousePosition;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            tmpColor.a = 1f;
            gameObject.GetComponentInParent<SpriteRenderer>().color = tmpColor;
            gameObject.AddComponent<MoveObject>();

            ELevel.instance.creatingObject = true;
        }
        
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            long indexObject = ELevel.instance.objNum - 1;
            Command cancel = new RemoveObjectCommand(indexObject);
            LevelEditorManager.commandManager.ExecuteCommand(cancel);
        }
    }
}


