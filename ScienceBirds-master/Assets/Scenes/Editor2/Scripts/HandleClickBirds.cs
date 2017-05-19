using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TODO
 * Colocar avisos 
     */

public class HandleClickBirds : MonoBehaviour
{
    public Button addBlackBird;
    public Button addRedBird;
    public Button addBlueBird;
    private int black = 0, blue = 0, red = 0, total = 0;
    private GameObject blackBirds, blueBirds, redBirds;

    // Use this for initialization
    void Start()
    {
        Button btn = addBlackBird.GetComponent<Button>();
        btn.onClick.AddListener(birdBlack);

        Button btn2 = addRedBird.GetComponent<Button>();
        btn2.onClick.AddListener(birdRed);

        Button btn3 = addBlueBird.GetComponent<Button>();
        btn3.onClick.AddListener(birdBlue);

        blackBirds = GameObject.Find("BlackBirds");
        blueBirds = GameObject.Find("BlueBirds");
        redBirds = GameObject.Find("RedBirds");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void birdBlack()
    {
        Debug.Log("add black bird");
        if (blackBirds != null && total < 5)
        {
            black++;
            blackBirds.transform.GetChild(0).name = black.ToString();
            Debug.Log("done");
            total++;
        }
    }
    void birdRed()
    {
        if (redBirds != null && total < 5)
        {
            red++;
            redBirds.transform.GetChild(0).name = red.ToString();
            total++;
        }
    }
    void birdBlue()
    {
        if (blueBirds != null && total < 5)
        {
            blue++;
            blueBirds.transform.GetChild(0).name = blue.ToString();
            total++;
        }
    }
}
