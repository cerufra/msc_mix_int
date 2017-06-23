using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Text;

public class ELevel : MonoBehaviour {

    public static ELevel instance = null;

    public ABLevel level = new ABLevel();
    public int pigCount { get; set; }
    public int levelNum = 0;
    public long objNum = 0;

    public class EObject
    {
        public GameObject gameObject { get; set; }
        public OBjData dados { get; set; }
    }

    public Dictionary<long, EObject> blocksEditor = new Dictionary<long, EObject>();
    public Dictionary<long, EObject> pigsEditor = new Dictionary<long, EObject>();

    public bool PrepareForSaving()
    {
        for (int i = 0; i < blocksEditor.Count; i++)
        {
            level.blocks.Add((BlockData)blocksEditor[i].dados);
        }
        for (int i = 0; i < pigsEditor.Count; i++)
        {
            level.pigs.Add(pigsEditor[i].dados);
        }
        if (blocksEditor.Count == 0 || pigsEditor.Count == 0 || level.birds.Count == 0)
            return false;
        return true;
    }

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        AddCamera();
        AddSlingShot();
        instance.level.width = 2;

        DontDestroyOnLoad(gameObject);

        // Limpa levels criados ateriormente
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
    }

    void AddCamera()
    {
        CameraData cd = new CameraData(15, (float)17.5, 0, -1);
        instance.level.camera = cd;
    }

    void AddSlingShot()
    {
        SlingData sd = new SlingData(-6, (float)-2.5);
        instance.level.slingshot = sd;
    }

    public static string GetXmlLevel(ABLevel level)
    {

        StringBuilder output = new StringBuilder();
        XmlWriterSettings ws = new XmlWriterSettings();
        ws.Indent = true;

        using (XmlWriter writer = XmlWriter.Create(output, ws))
        {
            writer.WriteStartElement("Level");
            writer.WriteAttributeString("width", level.width.ToString());

            writer.WriteStartElement("Camera");
            writer.WriteAttributeString("x", level.camera.x.ToString());
            writer.WriteAttributeString("y", level.camera.y.ToString());
            writer.WriteAttributeString("minWidth", level.camera.minWidth.ToString());
            writer.WriteAttributeString("maxWidth", level.camera.maxWidth.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Birds");

            foreach (BirdData abBird in level.birds)
            {
                writer.WriteStartElement("Bird");
                writer.WriteAttributeString("type", abBird.type.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Slingshot");
            writer.WriteAttributeString("x", level.slingshot.x.ToString());
            writer.WriteAttributeString("y", level.slingshot.y.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("GameObjects");

            foreach (BlockData abObj in level.blocks)
            {
                writer.WriteStartElement("Block");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("material", abObj.material.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (OBjData abObj in level.pigs)
            {
                writer.WriteStartElement("Pig");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (OBjData abObj in level.tnts)
            {
                writer.WriteStartElement("TNT");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (PlatData abObj in level.platforms)
            {
                writer.WriteStartElement("Platform");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteAttributeString("scaleX", abObj.scaleX.ToString());
                writer.WriteAttributeString("scaleY", abObj.scaleY.ToString());
                writer.WriteEndElement();
            }
        }

        // StreamWriter streamWriter = new StreamWriter(path);
        //streamWriter.WriteLine(output.ToString());
        //streamWriter.Close();
        return output.ToString();
    }

    public void SaveXmlLevel(string path)
    {
        StringBuilder output = new StringBuilder();
        XmlWriterSettings ws = new XmlWriterSettings();
        ws.Indent = true;

        using (XmlWriter writer = XmlWriter.Create(output, ws))
        {
            writer.WriteStartElement("Level");
            writer.WriteAttributeString("width", level.width.ToString());

            writer.WriteStartElement("Camera");
            writer.WriteAttributeString("x", level.camera.x.ToString());
            writer.WriteAttributeString("y", level.camera.y.ToString());
            writer.WriteAttributeString("minWidth", level.camera.minWidth.ToString());
            writer.WriteAttributeString("maxWidth", level.camera.maxWidth.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Birds");

            foreach (BirdData abBird in level.birds)
            {
                writer.WriteStartElement("Bird");
                writer.WriteAttributeString("type", abBird.type.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Slingshot");
            writer.WriteAttributeString("x", level.slingshot.x.ToString());
            writer.WriteAttributeString("y", level.slingshot.y.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("GameObjects");

            foreach (BlockData abObj in level.blocks)
            {
                writer.WriteStartElement("Block");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("material", abObj.material.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (OBjData abObj in level.pigs)
            {
                writer.WriteStartElement("Pig");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (OBjData abObj in level.tnts)
            {
                writer.WriteStartElement("TNT");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteEndElement();
            }

            foreach (PlatData abObj in level.platforms)
            {
                writer.WriteStartElement("Platform");
                writer.WriteAttributeString("type", abObj.type.ToString());
                writer.WriteAttributeString("x", abObj.x.ToString());
                writer.WriteAttributeString("y", abObj.y.ToString());
                writer.WriteAttributeString("rotation", abObj.rotation.ToString());
                writer.WriteAttributeString("scaleX", abObj.scaleX.ToString());
                writer.WriteAttributeString("scaleY", abObj.scaleY.ToString());
                writer.WriteEndElement();
            }
        }

        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.WriteLine(output.ToString());
        streamWriter.Close();
        //return output.ToString();
    }

}
