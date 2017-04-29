using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sMenu : MonoBehaviour {

    private int mouseX;
    private int mouseY;
    private bool active;

    public void Select (int opt) {
        sHelper.GetInstance().ResolveMenu(mouseX, mouseY, opt);
    }

    public void Init (int X, int Y) {
        mouseX = X;
        mouseY = Y;
        gameObject.transform.position = Input.mousePosition;
    }

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
