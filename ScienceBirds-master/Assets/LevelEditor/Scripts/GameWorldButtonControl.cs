using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWorldButtonControl : MonoBehaviour {

    public GameObject nextButton;
    public GameObject formButton;

	// Use this for initialization
	void Awake () {
		if(Timer.instance != null)
        {
            if(Timer.instance.LevelNum == 2)
            {
                nextButton.SetActive(false);
                formButton.SetActive(true);
                Debug.Log("form");
            }
            else
            {
                nextButton.SetActive(true);
                formButton.SetActive(false);
                Debug.Log("next");
            }
            Debug.Log("Buttons gameWorld");
        }
        else
        {
            Debug.Log("GameWorld: No timer found");
        }
	}
	
}
