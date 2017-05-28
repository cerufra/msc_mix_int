using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregaCena : MonoBehaviour {

    public Button manual;
    public Button sketch;
    public Button editor;
    public Button mainMenu;
    // Use this for initialization
    void Start()
    {
        if (manual != null)
        {
            Button btn = manual.GetComponent<Button>();
            btn.onClick.AddListener(cenaManual);
        }
        if(sketch != null)
        {
            Button btn2 = sketch.GetComponent<Button>();
            btn2.onClick.AddListener(cenaSketch);
        }
        if (editor != null)
        {
            Button btn3 = editor.GetComponent<Button>();
            btn3.onClick.AddListener(menuEditor);
        }
        if (mainMenu != null)
        {
            Button btn4 = mainMenu.GetComponent<Button>();
            btn4.onClick.AddListener(menuPrincipal);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void cenaManual()
    {
        SceneManager.LoadScene("Editor2", LoadSceneMode.Single);
    }
    public void cenaSketch()
    {
        SceneManager.LoadScene("Editor_Sketch", LoadSceneMode.Single);
    }

    public void menuEditor()
    {
        SceneManager.LoadScene("EditorMenu", LoadSceneMode.Single);
    }

    public void menuPrincipal()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
