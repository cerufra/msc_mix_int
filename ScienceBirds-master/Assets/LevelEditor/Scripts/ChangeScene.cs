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

    public void GoogleForm()
    {
        Debug.Log("Tempo criacao = " + Timer.instance.TempoCriacaoLevel() + " segundo(s)");
        Application.OpenURL("https://docs.google.com/forms/d/1_3mO1cO5pvV6zpJ1gByR6m4eYFeF6v5ExQgyRPyVpXA/prefill");
    }
}
