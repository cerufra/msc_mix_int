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
    public GameObject needBird;
    private string levelXML = "";
    private string baseXML = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n" +
                              "<Level width = \"2\" >\n" +
                              "<Camera x=\"0\" y=\"-1\"> minWidth=\"15\" maxWidth=\"17.5\">\n";
    private string path;
    StreamWriter sw;

    private int birdcount = 0;

    // Use this for initialization
    void Start () {
        path = Application.dataPath + "/StreamingAssets/Levels/";
        Button btn = criar.GetComponent<Button>();
        btn.onClick.AddListener(criaFase);

        path += "level-5.xml";

        if (!File.Exists(path))
        {
            File.CreateText(path);
        }
        levelXML = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    void criaFase()
    {
        levelXML = baseXML;
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
        if(birdcount > 0)
        {
            File.WriteAllText(path, levelXML);
            loading.SetActive(true);
            StartCoroutine("Wait");
            StartCoroutine("LoadingText");
            Debug.Log(path);
        }else
        {
            needBird.SetActive(true);
        }
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
            int count = int.Parse(blackBirds.GetComponent<Transform>().GetChild(0).name);
            birdcount += count;
            while(count > 0)
            {
                count--;
                levelXML += "<Bird type=\"BirdBlack\"/>\n";
            }
        }
        GameObject blueBirds = GameObject.Find("BlueBirds");
        if (blueBirds != null)
        {
            int count = int.Parse(blueBirds.GetComponent<Transform>().GetChild(0).name);
            birdcount += count;
            while (count > 0)
            {
                count--;
                levelXML += "<Bird type=\"BirdBlue\"/>\n";
            }
        }
        GameObject redBirds = GameObject.Find("RedBirds");
        if (redBirds != null)
        {
            int count = int.Parse(redBirds.GetComponent<Transform>().GetChild(0).name);
            birdcount += count;
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
            string nome,nome2;
            for (int i = 0; i < objCount; i++)
            {

                nome = objetos.transform.GetChild(i).name;
                nome2 = nome;
                if (nome.Contains("Circle"))
                {
                    levelXML += "<Block type = \"Circle\" ";
                }
                else if (nome.Contains("CircleSmall"))
                {
                    levelXML += "<Block type = \"CircleSmall\" ";
                }
                else if (nome.Contains("RectBig"))
                {
                    levelXML += "<Block type = \"RectBig\" ";
                }
                else if (nome.Contains("RectFat"))
                {
                    levelXML += "<Block type = \"RectFat\" ";
                }
                else if (nome.Contains("RectMedium"))
                {
                    levelXML += "<Block type = \"RectMedium\" ";
                }
                else if (nome.Contains("RectSmall"))
                {
                    levelXML += "<Block type = \"RectSmall\" ";
                }
                else if (nome.Contains("RectTiny"))
                {
                    levelXML += "<Block type = \"RectTiny\" ";
                }
                else if (nome.Contains("SquareHole"))
                {
                    levelXML += "<Block type = \"SquareHole\" ";
                }
                else if (nome.Contains("SquareSmall"))
                {
                    levelXML += "<Block type = \"SquareSmall\" ";
                }
                else if (nome.Contains("SquareTiny"))
                {
                    levelXML += "<Block type = \"SquareTiny\" ";
                }
                else if (nome.Contains("Triangle"))
                {
                    levelXML += "<Block type = \"Triangle\" ";
                }
                else if (nome.Contains("TriangleHole"))
                {
                    levelXML += "<Block type = \"TriangleHole\" ";
                }
                else if (nome.Contains("BasicPigSmall"))
                {
                    levelXML += "<Pig type=\"BasicSmall\" ";
                }
                else if (nome.Contains("BasicPigMedium"))
                {
                    levelXML += "<Pig type=\"BasicMedium\" ";
                }
                else if (nome.Contains("BasicPig"))
                {
                    //<Pig      type="BasicBig"  x="2"  y="2"  />
                    levelXML += "<Pig type=\"BasicBig\" ";
                }


                if (nome.Contains("wood"))
                {
                    levelXML += " material=\"wood\" ";
                }else if (nome.Contains("ice"))
                {
                    levelXML += " material=\"ice\" ";
                }else if (nome.Contains("stone"))
                {
                    levelXML += " material=\"stone\" ";
                }

                levelXML += "x=\"" + objetos.transform.GetChild(i).transform.position.x + "\" ";
                levelXML += "y=\"" + objetos.transform.GetChild(i).transform.position.y + "\" ";

                if (nome.Contains("Rotated"))
                {
                    levelXML += " rotation=\"90\" ";
                }

                levelXML += " />\n";
            }
        }
    }

}
