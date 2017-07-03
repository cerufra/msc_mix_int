#define EDITOR_MANUAL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Random = UnityEngine.Random;

public class ChangeScene : MonoBehaviour {

    static float[] tempoLevel = new float[3];

    private void Start()
    {
#if EDITOR_MANUAL
        Timer.editorManual = true;
#else
        Timer.editorManual = false;
#endif
    }

    public void changeScene(string target)
    {
        if (target.Equals("MultiEditor"))
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

#if EDITOR_MANUAL
        if (target.Equals("MultiEditor"))
            target = "LevelEditor";
#endif

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
        tempoLevel[2] = Timer.instance.TempoCriacaoLevel();

        Debug.Log("Tempo criacao = " + Timer.instance.TempoCriacaoLevel() + " segundo(s)");
        
        string nome_editor = "";

#if EDITOR_MANUAL
        nome_editor = "EDITOR_MANUAL";
#else
        nome_editor = "EDITOR_INICIATIVA_MISTA";
#endif

        string link = "https://docs.google.com/forms/d/e/1FAIpQLScCD8b3RFKIuUeMcPgacq-N95nwNvQTW-tRmXBEHGYoH4l_Zw/viewform?usp=pp_url&entry.439976361=";
        link += nome_editor + "&entry.91620893=" + tempoLevel[0];
        link += "&entry.1343175451=" + tempoLevel[1] + "&entry.209042438=" + tempoLevel[2];

        Application.OpenURL(link);

        

       /*
        * Save test data
        *string pathDados = Application.dataPath + "/StreamingAssets/Dados/";

        DirectoryInfo dadosDir = new DirectoryInfo(pathDados);
        if (!dadosDir.Exists)
            dadosDir.Create();

        pathDados += Path.GetRandomFileName();
        DirectoryInfo dadosTeste = new DirectoryInfo(pathDados);
        if (!dadosTeste.Exists)
            dadosTeste.Create();
        else
            return;

        DirectoryInfo levelsDir = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
        foreach (FileInfo file in levelsDir.GetFiles())
        {
            File.Move(file.FullName,)
        }*/
    }

    public void NextLevel()
    {
        Debug.Log("Temp " + Timer.instance.TempoCriacaoLevel() + "l " + Timer.instance.LevelNum);

        if (Timer.instance.LevelNum < 2)
            tempoLevel[Timer.instance.LevelNum] = Timer.instance.TempoCriacaoLevel();
        else
            Debug.Log("ChangeScene Level num = " + Timer.instance.LevelNum);

        Timer.instance.ComecarContagem();
        Timer.instance.LevelNum++;
#if EDITOR_MANUAL
        SceneManager.LoadScene("LevelEditor");
#else
        SceneManager.LoadScene("Editor_Sketch");
#endif
    }

}
