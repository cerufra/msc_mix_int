using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Text;

public class CarregaObjetos : MonoBehaviour {

    string xmlText = "";

    public Transform circle, circleSmall, rectBig, rectFat, rectMedium, rectSmall, rectTiny, 
        squareHole, squareSmall, SquareTiny, triangle, triangleHole, basicPig, basicPigMedium, basicPigSmall;

    // Use this for initialization
    void Start () {
        string path = Application.dataPath + @"/StreamingAssets/Line2Blocks/level-1.xml";
        xmlText = File.ReadAllText(path);

        XmlReader reader = XmlReader.Create(new StringReader(xmlText));


        reader.ReadToFollowing("GameObjects");
        reader.Read();

        while (reader.Read())
        {
            string nodeName = reader.LocalName;
            if (nodeName == "GameObjects")
                break;

            reader.MoveToAttribute("type");
            string type = reader.Value;

            string material = "";
            if (reader.GetAttribute("material") != null)
            {
                reader.MoveToAttribute("material");
                material = reader.Value;
            }

            reader.MoveToAttribute("x");
            float x = (float)Convert.ToDouble(reader.Value);

            reader.MoveToAttribute("y");
            float y = (float)Convert.ToDouble(reader.Value);

            float rotation = 0f;
            if (!type.Contains("Basic") && reader.GetAttribute("rotation") != null)
            {
                reader.MoveToAttribute("rotation");
                rotation = (float)Convert.ToDouble(reader.Value);
            }

            criaInstancia(type, (int)rotation, x, y, material);
            reader.Read();

            /*if (nodeName == "Block")
            {

                level.blocks.Add(new BlockData(type, rotation, x, y, material));
                reader.Read();
            }
            else if (nodeName == "Pig")
            {

                level.pigs.Add(new OBjData(type, rotation, x, y));
                reader.Read();
            }
            else if (nodeName == "TNT")
            {

                level.tnts.Add(new OBjData(type, rotation, x, y));
                reader.Read();
            }
            else if (nodeName == "Platform")
            {

                float scaleX = 1f;
                if (reader.GetAttribute("scaleX") != null)
                {

                    reader.MoveToAttribute("scaleX");
                    scaleX = (float)Convert.ToDouble(reader.Value);
                }

                float scaleY = 1f;
                if (reader.GetAttribute("scaleY") != null)
                {

                    reader.MoveToAttribute("scaleY");
                    scaleY = (float)Convert.ToDouble(reader.Value);
                }

                level.platforms.Add(new PlatData(type, rotation, x, y, scaleX, scaleY));
                reader.Read();
            }*/
        }

    }
	
	// Update is called once per frame
	void Update () {}

    void criaInstancia(string tipo, int rotation, float x, float y, string material)
    {
        GameObject carregar = GameObject.Find("CarregarObj");
        if(carregar != null)
        {
            GameObject objetos = GameObject.Find("Objects");

            Transform bloco;

            if (tipo.Contains("Circle"))
            {
                bloco = circle;
            }else if (tipo.Contains("CircleSmall")){
                bloco = circleSmall;
            }else if (tipo.Contains("RectBig")){
                bloco = rectBig;
            }else if (tipo.Contains("RectFat"))
            {
                bloco = rectFat;
            }else if (tipo.Contains("RectMedium"))
            {
                bloco = rectMedium;
            }else if (tipo.Contains("RectSmall"))
            {
                bloco = rectSmall;
            }else if (tipo.Contains("RectTiny"))
            {
                bloco = rectTiny;
            }else if (tipo.Contains("SquareHole"))
            {
                bloco = squareHole;
            }else if (tipo.Contains("SquareSmall"))
            {
                bloco = squareSmall;
            }else if (tipo.Contains("SquareTiny"))
            {
                bloco = SquareTiny;
            }
            else if (tipo.Contains("TriangleHole"))
            {
                bloco = triangleHole;
            }
            else if (tipo.Contains("Triangle"))
            {
                bloco = triangle;
            }
            else if (tipo.Contains("BasicBig"))
            {
                bloco = basicPig;
            }else if (tipo.Contains("BasicMedium"))
            {
                bloco = basicPigMedium;
            }else
            {
                bloco = basicPigSmall;
            }

            if (objetos != null)
            {
                //GameObject obj = new GameObject();
                UnityEngine.Object obj = Instantiate(bloco, new Vector3(x, y, 2), Quaternion.identity, objetos.transform);
                obj.name = material + "_" + tipo + "_" + HandleClick.count.ToString();

                HandleClick.count++;

                Transform child = objetos.transform.GetChild(objetos.transform.childCount - 1);

                if(rotation == 90)
                {
                    child.transform.Rotate(new Vector3(0, 0, 1), 90);
                    obj.name += "Rotated_";
                }

                Rigidbody2D f = child.GetComponent<Collider2D>().attachedRigidbody;
                f.gravityScale = 0;
                f.freezeRotation = true;
                if (child.name.Contains("Basic"))
                {
                    Destroy(child.GetComponent("ABPig"));
                }
                else
                {
                    MATERIALS mat = (MATERIALS)System.Enum.Parse(typeof(MATERIALS), material);
                    child.GetComponent<ABBlock>().SetMaterial(mat);
                    Destroy(child.GetComponent("ABBlock"));
                }
                Destroy(child.GetComponent("ABParticleSystem"));
                child.GetComponent<Transform>().gameObject.AddComponent<Move>();
                child.GetComponent<Collider2D>().isTrigger = true;

            }
        }
    }
}
