using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Diagnostics;

public class GeraPontos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CriaFase()
    {
        /*//UnityEngine.Debug.Log(@"/StreamingAssets/Line2Blocks/Lines2Blocks.jar");
        string basePath = Path.GetFullPath(@"StreamingAssets\Line2Blocks\");
        ProcessStartInfo startInfo = new ProcessStartInfo("java", "-jar " + basePath + "Lines2Blocks.jar");
        //Process.Start("C:\\Users\\DPIMESTRADO\\Documents\\GitHub\\msc_mix_int\\ScienceBirds-master\\Assets\\StreamingAssets\\Line2Blocks\\Lines2Blocks.jar");
        startInfo.WorkingDirectory = basePath;
        startInfo.UseShellExecute = false;
        Process process = Process.Start(startInfo);*/
        string basePath = "C:\\Users\\DPIMESTRADO\\Documents\\GitHub\\msc_mix_int\\ScienceBirds-master\\Assets\\StreamingAssets\\Line2Blocks\\";
        ProcessStartInfo startInfo = new ProcessStartInfo("java", "-jar " + basePath + "Lines2Blocks.jar");
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.CreateNoWindow = true;
        startInfo.WorkingDirectory = basePath;
        startInfo.UseShellExecute = false;
        Process.Start(startInfo);
    }
}
