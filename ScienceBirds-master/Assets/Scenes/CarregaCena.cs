using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregaCena : MonoBehaviour {

    public Button manual;
    public Button sketch;

    // Use this for initialization
    void Start()
    {
        Button btn = manual.GetComponent<Button>();
        btn.onClick.AddListener(cenaManual);

        Button btn2 = sketch.GetComponent<Button>();
        btn2.onClick.AddListener(cenaSketch);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void cenaManual()
    {
        SceneManager.LoadScene("Editor2", LoadSceneMode.Single);
    }
    void cenaSketch()
    {
        SceneManager.LoadScene("Editor_Sketch", LoadSceneMode.Single);
    }
}
