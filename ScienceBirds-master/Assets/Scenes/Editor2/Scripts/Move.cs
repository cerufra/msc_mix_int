using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }


    private Vector3 screenPoint;
    private Vector3 offset;
    private GameObject selected, obj;


    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        selected = GameObject.Find("Select");
        if (selected != null)
        {
            //string aux = GetComponent<ABGameObject>().name;
            string aux = GetComponent<Transform>().gameObject.name;
            if (selected.transform.GetChild(0).name == "select")
            {
                selected.transform.GetChild(0).name = aux+"-";
                selected.transform.GetChild(1).name = aux+"_";
            }
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
}
