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
                string n = selected.transform.GetChild(0).name;
                n = n.Substring(0, n.Length - 1);
                if (selected.transform.GetChild(0).name != "objeto")
                    GameObject.Find(n).GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Difuse");
            }
            string n1 = selected.transform.GetChild(0).name;
            n1 = n1.Substring(0, n1.Length - 1);
            string n2 = selected.transform.GetChild(1).name;
            n2 = n2.Substring(0, n2.Length - 1);
            if (n1 != n2)
            {
                GameObject g = GameObject.Find(n2);
                if (g != null)
                {
                    g.GetComponent<Renderer>().material.shader = exemplo.GetComponent<Renderer>().sharedMaterial.shader;
                    g.name += "_";
                    selected.transform.GetChild(0).name = "objeto";
                }
            }
        }
    }

    private GameObject selected, obj;
    public void acaoSelect()
    {
        selected = GameObject.Find("Select");
        if (selected != null)
        {
            if (selected.transform.GetChild(0).name != "objeto" && selected.transform.GetChild(0).name != "select")
            {
               /* string n1 = selected.transform.GetChild(0).name;
                n1 = n1.Substring(0, n1.Length - 1);
                string n2 = selected.transform.GetChild(1).name;
                n2 = n2.Substring(0, n2.Length - 1);
                if (n1 == n2)
                {
                    return;
                }*/
                string n = selected.transform.GetChild(0).name;
                n = n.Substring(0, n.Length - 1);
                GameObject.Find(n).GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
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
                    string n = selected.transform.GetChild(0).name;
                    n = n.Substring(0, n.Length - 1);
                    Destroy(GameObject.Find(n));
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
                if (selected.transform.GetChild(0).name != "objeto" && !selected.transform.GetChild(0).name.Contains("Pig"))
                {
                    string n = selected.transform.GetChild(0).name;
                    n = n.Substring(0, n.Length - 1);
                    GameObject.Find(n).GetComponent<Renderer>().material.shader = exemplo.GetComponent<Renderer>().sharedMaterial.shader;
                    if (selected.transform.GetChild(0).name.Contains("Rotated_"))
                    {
                        GameObject.Find(n).transform.Rotate(new Vector3(0, 0, 1), -90);
                        GameObject.Find(n).name = selected.transform.GetChild(0).name.Substring(0, selected.transform.GetChild(0).name.Length - 8);
                    }else
                    {
                        // selected.transform.GetChild(0).name += "Rotated";
                        GameObject.Find(n).transform.Rotate(new Vector3(0, 0, 1), 90);
                        GameObject.Find(n).name += "Rotated_";
                    }
                    selected.transform.GetChild(0).name = "objeto";
                }

            }
        }
    }
}
