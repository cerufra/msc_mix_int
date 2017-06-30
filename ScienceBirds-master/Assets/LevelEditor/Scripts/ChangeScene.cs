using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ChangeScene : MonoBehaviour {

    public void changeScene(string target)
    {
        if (target.Equals("Editor_Sketch"))
        {
            Timer.instance.ComecarContagem();
            Timer.instance.LevelNum = 0;

            DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
            if (!di.Exists)
                di.Create();
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        SceneManager.LoadScene(target);
    }

    public void editarLevel()
    {
        LevelEditorManager.loadXMLFile = true;
        Timer.instance.EditandoLevel();
        SceneManager.LoadScene("LevelEditor");
    }

    public void GoogleForm()
    {
        Debug.Log("Tempo criacao = " + Timer.instance.TempoCriacaoLevel() + " segundo(s)");
        Application.OpenURL("https://docs.google.com/forms/d/1_3mO1cO5pvV6zpJ1gByR6m4eYFeF6v5ExQgyRPyVpXA/prefill");
    }

    public void NextLevel()
    {
        Timer.instance.ComecarContagem();
        Timer.instance.LevelNum++;
        SceneManager.LoadScene("Editor_Sketch");
    }

}
