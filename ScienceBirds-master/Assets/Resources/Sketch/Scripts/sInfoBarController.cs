using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sInfoBarController : MonoBehaviour {

    public GameObject PointerPositionXBox;
    public GameObject PointerPositionYBox;

    public GameObject LeftClickFunctionBox;
    public GameObject RightClickFunctionBox;

    private Text PositionX;
    private Text PositionY;
    private Text LeftClick;
    private Text RightClick;

	// Use this for initialization
	void Start () {
        PositionX = PointerPositionXBox.GetComponent<Text>();
        PositionY = PointerPositionYBox.GetComponent<Text>();
        LeftClick = LeftClickFunctionBox.GetComponent<Text>();
        RightClick = RightClickFunctionBox.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {

    }

    public void UpdatePointerPosition(Vector2 MousePosition) {
        if (MousePosition.x < 0 || MousePosition.y < 0 || MousePosition.x > 199 || MousePosition.y > 199) {
            PositionX.text = "X:";
            PositionY.text = "Y:";
        }
        PositionX.text = "X: " + MousePosition.x.ToString();
        PositionY.text = "Y: " + MousePosition.y.ToString();
    }

    public void UpdateLeftClick (string function) {
        LeftClick.text = function;
    }

    public void UpdateRightClick (string function) {
        RightClick.text = function;
    }
}
