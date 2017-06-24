using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour {

    public static MenuObject instance = null;

    private long objectIndex = -1;
    private ELevel.EObject refObject;
    private string type;

    public Button rotate;
    public Button delete;
    public Button cancel;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        rotate.onClick.AddListener(RotateObject);
        delete.onClick.AddListener(DeleteObject);
        cancel.onClick.AddListener(HideMenu);

        HideMenu();
    }

    public void RotateObject()
    {
        Command cmd = new RotateObjectCommand(objectIndex);

        LevelEditorManager.commandManager.ExecuteCommand(cmd);

        HideMenu();
    }

    public void DeleteObject()
    {
        Command cmd = null;
        if (type.Equals("block"))
        {
            cmd = new RemoveBlockCommand(objectIndex);
        }
        else if (type.Equals("pig"))
        {
            cmd = new RemovePigCommand(objectIndex);
        }

        if (cmd != null)
            LevelEditorManager.commandManager.ExecuteCommand(cmd);
        else
            Debug.Log("MenuObject : delete object error");

        HideMenu();
    }

    public void HideMenu()
    {
        objectIndex = -1;
        gameObject.SetActive(false);
    }
	
    public void AtivateMenu(long index, string type)
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
        Vector3 position = new Vector3(Input.mousePosition.x + 50, Input.mousePosition.y - 60, Input.mousePosition.z);
        transform.position = position;

        objectIndex = index;
        this.type = type;

        if (type.Equals("block"))
        {
            refObject = ELevel.instance.blocksEditor[index];
        }
        else if (type.Equals("pig"))
        {
            refObject = ELevel.instance.pigsEditor[index];
        }
    }
}
