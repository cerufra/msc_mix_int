using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public string SketchEditor;
    public string LevelEditor;

    static private SceneController instance;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        instance = this;
        LoadSketchEdior();
    }

    static public SceneController Instance()
    {
        return instance;
    }

    public void LoadSketchEdior()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SketchEditor, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void LoadLevelEditor()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(LevelEditor, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void DestroySketchEditor()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(SketchEditor);
    }

    public void DestroyLevelEditor()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(LevelEditor);
    }

    public void Preview()
    {
        LevelEditorManager.loadXMLFile = true;
        LevelEditorManager.isPreview = true;
        LoadLevelEditor();
        sHelper.GetInstance().EnableObjects(false);
        sHelper.GetInstance().EnableCamera(false);
    }

    public void Return()
    {
        sHelper.GetInstance().EnableCamera(true);
        LevelEditorManager.Instance().EnableCamera(false);
        LevelEditorManager.Instance().EnableObjects(false);
        DestroyLevelEditor();
        sHelper.GetInstance().EnableObjects(true);
    }

    public void Edit()
    {
        DestroySketchEditor();
        LevelEditorManager.isPreview = false;
        LevelEditorManager.Instance().EnableMenu(true);
    }
}
