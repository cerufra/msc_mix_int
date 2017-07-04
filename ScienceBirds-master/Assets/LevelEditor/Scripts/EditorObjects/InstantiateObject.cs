using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateObject : MonoBehaviour {

    private Vector3 mousePosition;
    private Color tmpColor;
    static int objCount = 0;
    static Queue<GameObject> toInstantiate = new Queue<GameObject>();

    void Awake()
    {
        if (ELevel.instance.loadingLevelFromFile)
        {
            gameObject.AddComponent<MoveObject>();
        }
        else
        {
            tmpColor = gameObject.GetComponentInParent<SpriteRenderer>().color;
            tmpColor.a = 0.55f;
            gameObject.GetComponentInParent<SpriteRenderer>().color = tmpColor;

            objCount++;
            if (objCount > 1)
            {
                Debug.Log("Instatiate " + toInstantiate.Count);
                while (toInstantiate.Count > 0)
                {
                    GameObject obj = toInstantiate.Dequeue();
                    if (obj != null)
                    {
                        if (obj.GetComponent<InstantiateObject>() != null)
                            Destroy(obj);
                    }
                }
                objCount = 1;
            }
            toInstantiate.Enqueue(gameObject);

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
            objCount--;
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


