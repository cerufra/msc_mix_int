using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveBirdCommand : UndoableCommand
{

    private Transform birdPanel = null;
    private BirdData item;
    private bool executado = false;

    public RemoveBirdCommand(string type, Transform birdPanel = null)
    {
        item = new BirdData(type);
        InitializeCount(type);
        this.birdPanel = birdPanel;
    }

    public RemoveBirdCommand(BirdData bird)
    {
        item = bird;
        InitializeCount(bird.type);
    }

    void InitializeCount(string type)
    {
        if (!AddBirdCommand.birdCount.ContainsKey(type))
            AddBirdCommand.birdCount.Add(type, 0);
    }

    public override void Undo()
    {
        if (executado)
        {
            ELevel.instance.level.birds.Add(item);
            AddBirdCommand.birdCount[item.type]++;
            AddBirdCommand.birdCountTotal++;
            if (birdPanel != null)
            {
                birdPanel.transform.GetChild(3).GetComponent<Text>().text = AddBirdCommand.birdCount[item.type].ToString();
            }
        }
    }

    public override void Execute()
    {
        if(AddBirdCommand.birdCount[item.type] > 0)
        {
            ELevel.instance.level.birds.Remove(item);
            AddBirdCommand.birdCount[item.type]--;
            AddBirdCommand.birdCountTotal--;
            if (birdPanel != null)
            {
                birdPanel.transform.GetChild(3).GetComponent<Text>().text = AddBirdCommand.birdCount[item.type].ToString();
            }
            executado = true;
        }else
        {
            executado = false;
        }
    }
}
