using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
        if (!di.Exists)
            di.Create();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
