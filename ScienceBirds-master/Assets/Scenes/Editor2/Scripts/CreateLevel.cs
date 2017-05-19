using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CreateLevel : MonoBehaviour {

    public Button criar;
    public GameObject loading;
    private string levelXML = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n" +
                              "<Level width = \"2\" >\n" +
                              "<Camera x=\"0\" y=\"-1\"> minWidth=\"15\" maxWidth=\"17.5\">\n";
    private string path = @"C:\Users\DPIMESTRADO\Documents\GitHub\msc_mix_int\ScienceBirds-master\Assets\StreamingAssets\Levels\";
    StreamWriter sw;

    // Use this for initialization
    void Start () {
        Button btn = criar.GetComponent<Button>();
        btn.onClick.AddListener(criaFase);

        path += "level-5.xml";

        if (!File.Exists(path))
        {
            File.CreateText(path);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

 
    void criaFase()
    {
        // adiciona passaros
        levelXML += "<Birds>\n";
        addBirds();
        levelXML += "</Birds>\n";

        levelXML += "<Slingshot x=\"-8.5\" y=\"-2.5\">\n";

        // adiciona objetos
        levelXML += "<GameObjects>\n";
        addObjects();
        levelXML += "</GameObjects>\n";
        levelXML += "</Level>\n";

        // escreve no arquivo
        /*using (sw = File.AppendText(path))
        {
            sw.WriteLine(levelXML);
        }*/
        File.WriteAllText(path, levelXML);
        loading.SetActive(true);
        StartCoroutine("Wait");
        StartCoroutine("LoadingText");
    }

    IEnumerator LoadingText()
    {
        while (true)
        {
            yield return new WaitForSeconds(01);
            loading.transform.GetChild(1).GetComponent<Text>().text += " .";
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(04);
        SceneManager.LoadScene("LevelSelectMenu", LoadSceneMode.Single);
    }

    protected virtual bool FileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }

    void addBirds()
    {
        GameObject blackBirds = GameObject.Find("BlackBirds");
        if (blackBirds != null)
        {
            int count = blackBirds.transform.childCount;
            while(count > 0)
            {
                count--;
                levelXML += "<Bird type=\"BirdBlack\"/>\n";
            }
        }
        GameObject blueBirds = GameObject.Find("BlueBirds");
        if (blueBirds != null)
        {
            int count = blueBirds.transform.childCount;
            while (count > 0)
            {
                count--;
                levelXML += "<Bird type=\"BirdBlue\"/>\n";
            }
        }
        GameObject redBirds = GameObject.Find("RedBirds");
        if (redBirds != null)
        {
            int count = redBirds.transform.childCount;
            while (count > 0)
            {
                count--;
                levelXML += "<Bird type=\"BirdRed\"/>\n";
            }
        }
    }

    void addObjects()
    {
        GameObject objetos = GameObject.Find("Objects");

        if (objetos != null)
        {
            int objCount = objetos.transform.childCount;
            for (int i = 0; i < objCount; i++)
            {
                string nome = objetos.transform.GetChild(i).name;
                if (nome.Contains("Circle"))
                {
                    levelXML += "<Block type = \"Circle\" material = \"wood\" ";
                }
                else if (nome.Contains("CircleSmall"))
                {
                    levelXML += "<Block type = \"CircleSmall\" material = \"wood\" ";
                }
                else if (nome.Contains("RectBig"))
                {
                    levelXML += "<Block type = \"RectBig\" material = \"wood\" ";
                }
                else if (nome.Contains("RectFat"))
                {
                    levelXML += "<Block type = \"RectFat\" material = \"wood\" ";
                }
                else if (nome.Contains("RectMedium"))
                {
                    levelXML += "<Block type = \"RectMedium\" material = \"wood\" ";
                }
                else if (nome.Contains("RectSmall"))
                {
                    levelXML += "<Block type = \"RectSmall\" material = \"wood\" ";
                }
                else if (nome.Contains("RectTiny"))
                {
                    levelXML += "<Block type = \"RectTiny\" material = \"wood\" ";
                }
                else if (nome.Contains("SquareHole"))
                {
                    levelXML += "<Block type = \"SquareHole\" material = \"wood\" ";
                }
                else if (nome.Contains("SquareSmall"))
                {
                    levelXML += "<Block type = \"SquareSmall\" material = \"wood\" ";
                }
                else if (nome.Contains("SquareTiny"))
                {
                    levelXML += "<Block type = \"SquareTiny\" material = \"wood\" ";
                }
                else if (nome.Contains("Triangle"))
                {
                    levelXML += "<Block type = \"Triangle\" material = \"wood\" ";
                }
                else if (nome.Contains("TriangleHole"))
                {
                    levelXML += "<Block type = \"TriangleHole\" material = \"wood\" ";
                }
                else if (nome.Contains("BasicPig"))
                {
                    //<Pig      type="BasicBig"  x="2"  y="2"  />
                    levelXML += "<Pig type=\"BasicBig\" ";
                }
                levelXML += "x=\"" + objetos.transform.GetChild(i).transform.position.x + "\" ";

                levelXML += "y=\"" + objetos.transform.GetChild(i).transform.position.y + "\" />\n";
            }
        }
    }

}
