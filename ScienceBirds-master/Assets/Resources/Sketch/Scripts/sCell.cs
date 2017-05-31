using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCell : MonoBehaviour {

    private int posX;
    public int X {
        get { return posX; }
        set { posX = value; }
    }

    private int posY;
    public int Y {
        get { return posY; }
        set { posY = value; }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void Init()
    //{
    //    if (X == 0 || Y == 0)
    //    {
    //        this.collider2D.
    //    }
    //}

    public void OnMouseEnter() {
        sHelper.GetInstance().UpdateMousePosition(posX, posY);
    }

    public void OnMouseExit()
    {
        sHelper.GetInstance().UpdateMousePosition(null, null);
    }
}
