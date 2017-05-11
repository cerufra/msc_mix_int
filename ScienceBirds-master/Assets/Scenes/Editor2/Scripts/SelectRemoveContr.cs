using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRemoveContr : MonoBehaviour {

    public Button select, remove;

	// Use this for initialization
	void Start () {
        select.GetComponent<Button>().onClick.AddListener(acaoSelect);
        remove.GetComponent<Button>().onClick.AddListener(acaoRemove);
	}
	
	// Update is called once per frame
	void Update () {
        selected = GameObject.Find("Select");
        if (selected != null)
        {
            if (selected.transform.GetChild(0).name != "select")
            {
                if (selected.transform.GetChild(0).name != "objeto")
                    GameObject.Find(selected.transform.GetChild(0).name).GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Difuse");
            }
        }
    }

    private GameObject selected, obj;
    public void acaoSelect()
    {
        selected = GameObject.Find("Select");
        if (selected != null)
        {
            if(selected.transform.GetChild(0).name != "objeto")
                GameObject.Find(selected.transform.GetChild(0).name).GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
            selected.transform.GetChild(0).name = "select";
        }
    }
    public void acaoRemove()
    {
        selected = GameObject.Find("Select");
        if (selected != null)
        {
            if (selected.transform.GetChild(0).name != "select")
            {
                if (selected.transform.GetChild(0).name != "objeto")
                {
                    Destroy(GameObject.Find(selected.transform.GetChild(0).name));
                    selected.transform.GetChild(0).name = "objeto";
                }
                    
            }
        }
    }
}
