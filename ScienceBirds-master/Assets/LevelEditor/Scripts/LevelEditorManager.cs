using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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

    // Material atual
    private string currentMaterial = "wood";

    // Obj atual
    private string toInstantiate = null;

    // Command Manager
    public static CommandManager commandManager = new CommandManager();
    Dictionary<string, AddBirdCommand> addBirdRef = new Dictionary<string, AddBirdCommand>();

    // Caso true, permite carregar level xml para editor
    public static bool loadXMLFile = false;
    public static string pathXMLFile = @"/StreamingAssets/Line2Blocks/level-1.xml";

    // Gravidade toggle
    public Button gravityButton;
    public Sprite gravityOnSprite, gravityOffSprite;
    bool gravityOn = false;
    public GameObject gravityWarning;
    Queue<Vector3> positionBlocks = new Queue<Vector3>();
    Queue<Vector3> positionPigs = new Queue<Vector3>();
    

    void Awake()
    {
        if (Timer.instance == null)
            Timer.instance = new Timer();
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
            addBirdRef.Add(birds[i].name, addBird);
            Button addButton = birdController.transform.GetChild(1).GetComponent<Button>();
            addButton.onClick.AddListener(delegate 
                                            {
                                                commandManager.ExecuteCommand(addBird);
                                            });
            // Remove Button
            RemoveBirdCommand removeBird = new RemoveBirdCommand(birds[i].name, birdController.transform);
            Button removeButton = birdController.transform.GetChild(2).GetComponent<Button>();
            removeButton.onClick.AddListener(delegate { commandManager.ExecuteCommand(removeBird); });
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
    }

    void Start()
    {
        if (loadXMLFile)
        {
            CarregaXmlLevel(Application.dataPath + pathXMLFile);
            loadXMLFile = false;
        }

        Timer.instance.InicioLevelEditor();

        gravityButton.onClick.AddListener(GravityButtonAction);
    }

    void GravityButtonAction()
    {
        gravityButton.enabled = false;
        if (gravityOn)
        {
            gravityButton.image.sprite = gravityOffSprite;
            gravityOn = false;

            foreach (ELevel.EObject block in ELevel.instance.blocksEditor.Values)
            {
                if (block.gameObject != null)
                {
                    block.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    block.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    block.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    block.gameObject.transform.position = positionBlocks.Dequeue();
                    if (block.rotated90Degree)
                        block.gameObject.GetComponent<Rigidbody2D>().rotation = 90;
                    else
                        block.gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                }
            }

            foreach (ELevel.EObject pig in ELevel.instance.pigsEditor.Values)
            {
                if (pig.gameObject != null)
                {
                    pig.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    pig.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    pig.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    pig.gameObject.transform.position = positionPigs.Dequeue();
                    if (pig.rotated90Degree)
                        pig.gameObject.GetComponent<Rigidbody2D>().rotation = 90;
                    else
                        pig.gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                }
            }
        }
        else
        {
            gravityButton.image.sprite = gravityOnSprite;
            gravityOn = true;

            foreach (ELevel.EObject block in ELevel.instance.blocksEditor.Values)
            {
                if (block.gameObject != null)
                {
                    block.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    positionBlocks.Enqueue(block.gameObject.transform.position);
                }
            }

            foreach (ELevel.EObject pig in ELevel.instance.pigsEditor.Values)
            {
                if (pig.gameObject != null)
                {
                    pig.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    positionPigs.Enqueue(pig.gameObject.transform.position);
                }
            }
        }
        gravityWarning.SetActive(gravityOn);
        ELevel.instance.isGravityEditorOn = gravityOn;
        gravityButton.enabled = true;
    }

    public void clickInstatiateObj(string type)
    {
        if (toInstantiate != null && !type.Equals(toInstantiate))
            CancelCreatingObject();

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
            }
            else if (Input.GetKey(KeyCode.M))
            {
                changeCurrentMaterial();
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                clickInstatiateObj("Circle");
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                clickInstatiateObj("CircleSmall");
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                clickInstatiateObj("RectBig");
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                clickInstatiateObj("RectFat");
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                clickInstatiateObj("RectMedium");
            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                clickInstatiateObj("RectSmall");
            }
            else if (Input.GetKey(KeyCode.Alpha7))
            {
                clickInstatiateObj("RectTiny");
            }
            else if (Input.GetKey(KeyCode.Alpha8))
            {
                clickInstatiateObj("SquareHole");
            }
            else if (Input.GetKey(KeyCode.Alpha9))
            {
                clickInstatiateObj("SquareSmall");
            }
            else if (Input.GetKey(KeyCode.W))
            {
                clickInstatiateObj("SquareTiny");
            }
            else if (Input.GetKey(KeyCode.E))
            {
                clickInstatiateObj("Triangle");
            }
            else if (Input.GetKey(KeyCode.R))
            {
                clickInstatiateObj("TriangleHole");
            }
            else if (Input.GetKey(KeyCode.T))
            {
                clickInstatiateObj("BasicBig");
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                clickInstatiateObj("BasicMedium");
            }
            else if (Input.GetKey(KeyCode.U))
            {
                clickInstatiateObj("BasicSmall");
            }else if (Input.GetKey(KeyCode.Escape))
            {
                CancelCreatingObject();
            }
        }
    }
    
    void Update()
    {
        if (ELevel.instance.creatingObject)
        {
            createInstance();
            ELevel.instance.creatingObject = false;
        }
    }

    void CancelCreatingObject()
    {
        long indexObject = ELevel.instance.objNum - 1;
        Command cancel = new RemoveObjectCommand(indexObject);
        commandManager.ExecuteCommand(cancel);
        //commandManager.Undo();
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
        ELevel.instance.PrepareForSaving();
        SceneManager.LoadScene("MainMenu");
    }

    public void CreateLevelButton()
    {
        // Limpa levels criados ateriormente
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Levels/");
        if (!di.Exists)
            di.Create();
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }

        if (ELevel.instance.PrepareForSaving()) 
        {
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
        Timer.instance.CriandoLevel();
    }

    public void CarregaXmlLevel(string path)
    {
        string levelFileXML = LevelLoader.ReadXmlLevel(path);

        ABLevel levelFromFile = LevelLoader.LoadXmlLevel(levelFileXML);

        ELevel.instance.loadingLevelFromFile = true;
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
        
        foreach(BirdData bird in levelFromFile.birds)
        {
            if (addBirdRef.ContainsKey(bird.type))
            {
                commandManager.ExecuteCommand(addBirdRef[bird.type]);
            }
        }

        ELevel.instance.loadingLevelFromFile = false;
    }
}