using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sMenu : MonoBehaviour {

    public GameObject btnHollow;

    private int mouseX;
    private int mouseY;
    private bool active;

    public void Select (int opt) {
        sHelper.GetInstance().ResolveMenu(mouseX, mouseY, opt);
    }

    public void Init (int X, int Y, bool VertexMenu) {
        mouseX = X;
        mouseY = Y;
        gameObject.transform.position = Input.mousePosition;
        btnHollow.GetComponent<Button>().enabled = VertexMenu;
    }

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
