using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void changeScene(string target)
    {
        SceneManager.LoadScene(target);

        if (target.Equals("Editor_Sketch"))
            Timer.instance.ComecarContagem();

    }

    public void editarLevel()
    {
        LevelEditorManager.loadXMLFile = true;
        SceneManager.LoadScene("LevelEditor");
        Timer.instance.EditandoLevel();
    }
}
