using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBirdCommand : UndoableCommand {

    private Transform birdPanel = null;
    private BirdData item;
    //private int index;
    public static Dictionary<string, int> birdCount = new Dictionary<string, int>();
    public static int birdCountTotal = 0;

	public AddBirdCommand(string type, Transform birdPanel = null)
    {
        item = new BirdData(type);
        InitializeCount(type);
        this.birdPanel = birdPanel;
    }

    public AddBirdCommand(BirdData bird, Transform birdPanel = null)
    {
        item = bird;
        InitializeCount(bird.type);
    }

    void InitializeCount(string type)
    {
        if (!birdCount.ContainsKey(type))
            birdCount.Add(type, 0);
    }

    public override void Execute()
    {
        ELevel.instance.level.birds.Add(item);
        birdCount[item.type]++;
        birdCountTotal++;
        if(birdPanel != null)
        {
            birdPanel.transform.GetChild(3).GetComponent<Text>().text = birdCount[item.type].ToString();
        }
    }

    public override void Undo()
    {
        ELevel.instance.level.birds.Remove(item);
        birdCount[item.type]--;
        birdCountTotal--;
        if (birdPanel != null)
        {
            birdPanel.transform.GetChild(3).GetComponent<Text>().text = birdCount[item.type].ToString();
        }
    }
}
