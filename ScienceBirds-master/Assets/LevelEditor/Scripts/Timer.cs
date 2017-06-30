using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Timer : MonoBehaviour {

    public static Timer instance = null;

    private float inicioSketchEditorTmp , _inicioSketchEditor;
    private float inicioLevelEditorTmp, _inicioLevelEditor;
    private float criacaoLevel;
    private bool editandoLevelTmp = false;
    private bool nenhumLevelCriado = true;
    public int LevelNum = 0;

    public static bool editorManual = false;

	// Use this for initialization
	void Start () {
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
        if (!di.Exists)
            di.Create();
        //Application.OpenURL("www.google.com");

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ComecarContagem()
    {
        inicioSketchEditorTmp = Time.unscaledTime;
        editandoLevelTmp = false;
    }

    public void InicioLevelEditor()
    {
        inicioLevelEditorTmp = Time.unscaledTime;
    }

    // Saves current time and temporary values
    public void CriandoLevel()
    {
        if (editandoLevelTmp)
            return;
        _inicioLevelEditor = inicioLevelEditorTmp;
        _inicioSketchEditor = inicioSketchEditorTmp;
        criacaoLevel = Time.unscaledTime;
        nenhumLevelCriado = false;
    }

    public float TempoCriacaoLevel()
    {

        float result = 0;

        if (editorManual)
        {
            result = criacaoLevel - _inicioLevelEditor;
        }
        else
        {
            result = criacaoLevel - _inicioSketchEditor;
        }

        return result;
    }

    public void EditandoLevel()
    {
        editandoLevelTmp = true;
    }
}
