using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEditorManager : MonoBehaviour
{
    // Prefabs
    public List<Transform> blocks;
    public List<Transform> birds;
    public List<Transform> pigs;

    // Store the idex of the prefabs and their type
    private Dictionary<string, int> typePrefabIndex = new Dictionary<string, int>();

    // Sprites buttons
    public List<Sprite> iceSprites;
    public List<Sprite> stoneSprites;
    public List<Sprite> woodSprites;

    // Buttons to change sprites
    public List<Button> buttons;

    // Panels for UI
    public Transform birdsPanel;
    public Transform bottomPanel;
    public GameObject warning;
    //public Transform topPanel;

    // Add/Remove bird controllers
    Dictionary<string, Transform> birdControllers = new Dictionary<string, Transform>();

    // Material atual
    private string currentMaterial = "wood";

    // Obj atual
    private string toInstantiate = null;

    // Command Manager
    public static CommandManager commandManager = new CommandManager();

    // Caso true, permite carregar level xml para editor
    public static bool loadXMLFile = true;
    public static string pathXMLFile = @"/StreamingAssets/Line2Blocks/level-1.xml";

    void Awake()
    {
        // Bird Controllers
        for (int i = 0; i < birds.Count; i++)
        {
            // Panel and Sprite
            GameObject birdController = Instantiate(birdsPanel.gameObject) as GameObject;
            birdController.transform.SetParent(bottomPanel.transform);
            birdController.transform.position = new Vector3(i * 100 + 42, 40);
            Sprite birdSprite = birds[i].GetComponent<SpriteRenderer>().sprite;
            birdController.transform.GetChild(0).GetComponent<Image>().sprite = birdSprite;
            // Add Button
            AddBirdCommand addBird = new AddBirdCommand(birds[i].name, birdController.transform);
            Button addButton = birdController.transform.GetChild(1).GetComponent<Button>();
            addButton.onClick.AddListener(delegate 
                                            {
                                                commandManager.ExecuteCommand(addBird);
                                            });
            // Remove Button
            RemoveBirdCommand removeBird = new RemoveBirdCommand(birds[i].name, birdController.transform);
            Button removeButton = birdController.transform.GetChild(2).GetComponent<Button>();
            removeButton.onClick.AddListener(delegate { commandManager.ExecuteCommand(removeBird); });
            // guarda referencia
            birdControllers.Add(birds[i].name, birdController.transform);
        }

        // Guarda index dos blocos/porcos pelo tipo -> para encontrar prefab pelo tipo
        for (int i = 0; i < blocks.Count; i++)
        {
            typePrefabIndex.Add(blocks[i].name,i);
        }
        for (int i = 0; i < pigs.Count; i++)
        {
            typePrefabIndex.Add(pigs[i].name, i);
        }

        if (loadXMLFile)
        {
            CarregaXmlLevel(Application.dataPath + pathXMLFile);
            loadXMLFile = false;
        }
    }

    public void clickInstatiateObj(string type)
    {
        toInstantiate = type;
        createInstance();
    }

    void createInstance()
    {
        Command addObject = null;
        int index = typePrefabIndex[toInstantiate];
        if (toInstantiate.Contains("Basic"))
        {
            addObject = new AddPigCommand(toInstantiate, 0, 10, 10, pigs[index]);
        }else if(toInstantiate != null)
        {
            addObject = new AddBlockCommand(toInstantiate, 0, 10, 10, currentMaterial, blocks[index]);
        }
        if (addObject != null)
            commandManager.ExecuteCommand(addObject);
        //Debug.Log(toInstantiate);
    }

    void FixedUpdate()
    {
        // CTRL + Z
        if (Input.anyKeyDown)
        {
            if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Z))
            {
                commandManager.Undo();
                //Debug.Log("Undo");
            }
            else if (Input.GetKey(KeyCode.M))
            {
                changeCurrentMaterial();
            }
        }

        /*if(toInstantiate != null)
        {

        }
        */
    }

    public void changeCurrentMaterial()
    {
        if (currentMaterial == "wood")
        {
            currentMaterial = "ice";
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].image.sprite = iceSprites[i];
            }
        }
        else if (currentMaterial == "ice")
        {
            currentMaterial = "stone";
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].image.sprite = stoneSprites[i];
            }
        }
        else if (currentMaterial == "stone")
        {
            currentMaterial = "wood";
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].image.sprite = woodSprites[i];
            }
        }
    }

    public void CancelButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreateLevelButton()
    {
        if (ELevel.instance.PrepareForSaving()) 
        {
            //Debug.Log(ELevel.GetXmlLevel(ELevel.instance.level));
            // LevelLoader.LoadXmlLevel(ELevel.GetXmlLevel(ELevel.instance.level));
            string path = Application.dataPath + "/StreamingAssets/Levels/level-"
                + ELevel.instance.levelNum + ".xml";
            ELevel.instance.SaveXmlLevel(path);
            ELevel.instance.levelNum++;

            SceneManager.LoadScene("LevelSelectMenu");
        }
        else
        {
            warning.SetActive(true);
        }
    }

    public void CarregaXmlLevel(string path)
    {
        ELevel.instance.loadingLevelFromFile = true;
        string levelFileXML = LevelLoader.ReadXmlLevel(path);
        Debug.Log(levelFileXML);
        ABLevel levelFromFile = LevelLoader.LoadXmlLevel(levelFileXML);
        
        Command addObject = null;
        int index;
        foreach (BlockData block in levelFromFile.blocks)
        {
            index = typePrefabIndex[block.type];
            addObject = new AddBlockCommand(block.type, block.rotation, block.x, block.y, block.material, blocks[index]);
            commandManager.ExecuteCommand(addObject);
        }

        foreach (OBjData pig in levelFromFile.pigs)
        {
            index = typePrefabIndex[pig.type];
            addObject = new AddPigCommand(pig.type, pig.rotation, pig.x, pig.y, pigs[index]);
            commandManager.ExecuteCommand(addObject);
        }

        for (int i = 0; i < levelFromFile.birds.Count; i++)
        {
            addObject = new AddBirdCommand(levelFromFile.birds[i].type, birdControllers[levelFromFile.birds[i].type]);
            commandManager.ExecuteCommand(addObject);
        }
        
        ELevel.instance.loadingLevelFromFile = false;
    }
}