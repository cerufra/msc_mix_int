using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Return()
    {
        SceneController.Instance().Return();
    }

    public void Edit()
    {
        SceneController.Instance().Edit();
        Hide();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
