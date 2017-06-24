using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBirdCommand : Command {

    private Transform birdPanel = null;
    private string type;

	public AddBirdCommand(string type, Transform birdPanel = null)
    {
        if (!ELevel.instance.birdCount.ContainsKey(type))
            ELevel.instance.birdCount.Add(type, 0);
        this.birdPanel = birdPanel;
        this.type = type;
    }

    public override void Execute()
    {
        if (ELevel.instance.birdCount[type] < 9)
            ELevel.instance.birdCount[type]++;

        if (birdPanel != null)
        {
            birdPanel.transform.GetChild(3).GetComponent<Text>().text = ELevel.instance.birdCount[type].ToString();
        }
    }
}
