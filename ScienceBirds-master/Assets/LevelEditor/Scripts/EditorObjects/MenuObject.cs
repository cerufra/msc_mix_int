﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuObject : MonoBehaviour, IPointerExitHandler {

    public static MenuObject instance = null;

    private long objectIndex = -1;
    private string type;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

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
        gameObject.SetActive(true);
        Vector3 position = new Vector3(Input.mousePosition.x + 45, Input.mousePosition.y - 55, Input.mousePosition.z);
        transform.position = position;

        objectIndex = index;
        this.type = type;
    }

    public bool DifferentIndex(long idx)
    {
        return (objectIndex != idx);
    }

    /*void OnMouseExit()
    {
        HideMenu();
        Debug.Log("Mouse exit");
    }*/

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer exit");
        HideMenu();
    }
}
