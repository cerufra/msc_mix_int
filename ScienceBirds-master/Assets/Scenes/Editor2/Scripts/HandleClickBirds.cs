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
        if (total < 5)
        {
            black++;
            total++;
        }
        else
        {
            total -= black;
            black = 0;
        }
        if (blackBirds != null) { 
            GetComponent<Transform>().GetChild(0).GetComponentInChildren<Text>().text = black.ToString();
            blackBirds.transform.GetChild(0).name = black.ToString();   
        }
    }
    void birdBlue()
    {
        if (total < 5)
        {
            blue++;
            total++;
        }else
        {
            total -= blue;
            blue = 0;
        }
        if (blueBirds != null)
        {
            GetComponent<Transform>().GetChild(1).GetComponentInChildren<Text>().text = blue.ToString();
            blueBirds.transform.GetChild(0).name = blue.ToString();
        }

    }
    void birdRed()
    {
        if (total < 5)
        {
            red++;
            total++;
        }else
        {
            total -= red;
            red = 0;
        }
        if (redBirds != null)
        {
            GetComponent<Transform>().GetChild(2).GetComponentInChildren<Text>().text = red.ToString();
            redBirds.transform.GetChild(0).name = red.ToString();
        }
    }
}
