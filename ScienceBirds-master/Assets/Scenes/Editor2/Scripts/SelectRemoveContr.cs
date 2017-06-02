using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRemoveContr : MonoBehaviour {

    public Button select, remove,rotate;
    public Transform exemplo;

    // Use this for initialization
    void Start () {
        select.GetComponent<Button>().onClick.AddListener(acaoSelect);
        remove.GetComponent<Button>().onClick.AddListener(acaoRemove);
        rotate.GetComponent<Button>().onClick.AddListener(acaoRotate);
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
            if (selected.transform.GetChild(0).name != "objeto")
            {
                GameObject.Find(selected.transform.GetChild(0).name).GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
            }

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
    public void acaoRotate()
    {
        selected = GameObject.Find("Select");
        if (selected != null)
        {
            if (selected.transform.GetChild(0).name != "select")
            {
                if (selected.transform.GetChild(0).name != "objeto")
                {
                    GameObject.Find(selected.transform.GetChild(0).name).GetComponent<Renderer>().material.shader = exemplo.GetComponent<Renderer>().sharedMaterial.shader;
                    if (selected.transform.GetChild(0).name.Contains("Rotated_"))
                    {
                        GameObject.Find(selected.transform.GetChild(0).name).transform.Rotate(new Vector3(0, 0, 1), -90);
                        GameObject.Find(selected.transform.GetChild(0).name).name = selected.transform.GetChild(0).name.Substring(0, selected.transform.GetChild(0).name.Length - 8);
                    }else
                    {
                        // selected.transform.GetChild(0).name += "Rotated";
                        GameObject.Find(selected.transform.GetChild(0).name).transform.Rotate(new Vector3(0, 0, 1), 90);
                        GameObject.Find(selected.transform.GetChild(0).name).name += "Rotated_";
                    }
                    selected.transform.GetChild(0).name = "objeto";
                }

            }
        }
    }
}
