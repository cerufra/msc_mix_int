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
    public Transform topPanel;
    public GameObject warning;
    //public Transform topPanel;

    // Menu de confirmação da pré-visualizção
    public GameObject ConfirmMenu;

    // Camera da cena
    public Camera EditorCamera;

    // Objetos da cena
    public GameObject SceneObjects;

    // Material atual
    private string currentMaterial = "wood";

    // Obj atual
    private string toInstantiate = null;

    // Command Manager
    public static CommandManager commandManager = new CommandManager();
    Dictionary<string, AddBirdCommand> addBirdRef = new Dictionary<string, AddBirdCommand>();

    // Trava os comandos do editor em modo de previsualização
    public static bool isPreview = false;

    // Caso true, permite carregar level xml para editor
    public static bool loadXMLFile = false;
    public static string pathXMLFile = @"/StreamingAssets/Line2Blocks/level-1.xml";

    // Gravidade toggle
    public Button gravityButton;
    public Sprite gravityOnSprite, gravityOffSprite;
    bool gravityOn = false;
    public GameObject gravityWarning;
    Dictionary<long,Vector3> positionBlocks = new Dictionary<long, Vector3>();
    Dictionary<long, Vector3> positionPigs = new Dictionary<long, Vector3>();

    // Blocks parent
    public GameObject blocksParent;

    private static LevelEditorManager instance;

    void Awake()
    {
        instance = this;

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

        // Se modo de previsaulização, esconde menus, e ignora atalhos
        if (isPreview)
        {
            EnableMenu(false);
            ConfirmMenu.SetActive(true);
        }

        AddBlockCommand.BlocksEditor = blocksParent;
    }

    public static LevelEditorManager Instance()
    {
        return instance;
    }

    public void EnableMenu(bool value)
    {
        topPanel.gameObject.SetActive(value);
        bottomPanel.gameObject.SetActive(value);
    }

    public void EnableObjects(bool value)
    {
        SceneObjects.SetActive(value);
    }

    public void EnableCamera(bool value)
    {
        EditorCamera.enabled = value;
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

            foreach (KeyValuePair<long,ELevel.EObject> block in ELevel.instance.blocksEditor)
            {
                if (block.Value.gameObject != null)
                {
                    block.Value.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    block.Value.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    block.Value.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    block.Value.gameObject.transform.rotation = Quaternion.identity;
                    if (positionBlocks.ContainsKey(block.Key))
                        block.Value.gameObject.transform.position = positionBlocks[block.Key];
                    else
                        Debug.Log("LevelEditor: gravity toggle error - blocks");
                    if (block.Value.rotated90Degree)
                        block.Value.gameObject.transform.Rotate(new Vector3(0, 0, 1), 90);
                }
            }

            foreach (KeyValuePair<long,ELevel.EObject> pig in ELevel.instance.pigsEditor)
            {
                if (pig.Value.gameObject != null)
                {
                    pig.Value.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    pig.Value.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    pig.Value.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    pig.Value.gameObject.transform.rotation = Quaternion.identity;
                    if (positionPigs.ContainsKey(pig.Key))
                        pig.Value.gameObject.transform.position = positionPigs[pig.Key];
                    else
                        Debug.Log("LevelEditor: gravity toggle error - pigs");
                    if (pig.Value.rotated90Degree)
                        pig.Value.gameObject.transform.Rotate(new Vector3(0, 0, 1), 90);
                }
            }
        }
        else
        {
            gravityButton.image.sprite = gravityOnSprite;
            gravityOn = true;

            foreach (KeyValuePair<long, ELevel.EObject> block in ELevel.instance.blocksEditor)
            {
                if (block.Value.gameObject != null)
                {
                    float x = block.Value.gameObject.transform.position.x;
                    float y = block.Value.gameObject.transform.position.y;
                    float z = block.Value.gameObject.transform.position.z;
                    positionBlocks[block.Key] = new Vector3(x, y, z);
                    block.Value.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }

            foreach (KeyValuePair<long,ELevel.EObject> pig in ELevel.instance.pigsEditor)
            {
                if (pig.Value.gameObject != null)
                {
                    float x = pig.Value.gameObject.transform.position.x;
                    float y = pig.Value.gameObject.transform.position.y;
                    float z = pig.Value.gameObject.transform.position.z;
                    positionPigs[pig.Key] = new Vector3(x, y, z);
                    pig.Value.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
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
       /* foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        */
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
        Debug.Log("Tempo criacao = " + Timer.instance.TempoCriacaoLevel());
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