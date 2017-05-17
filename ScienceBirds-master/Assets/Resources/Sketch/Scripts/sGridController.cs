using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sGridController : MonoBehaviour {

    private static sGridController Instance;

    public GameObject cell;
    public GameObject vertex;
    public GameObject line;

    public int maxHeight;
    public int maxWidth;

	// Use this for initialization
	void Start () {
        Instance = this;

        GameObject newCell;
        for (int row = 0; row < maxWidth; row++) {
            for (int column = 0; column < maxHeight; column++) {
                newCell = Instantiate(cell, new Vector3(column * 0.24f, row * 0.24f, 0), cell.transform.rotation);
                newCell.GetComponent<sCell>().X = column;
                newCell.GetComponent<sCell>().Y = row;
                newCell.transform.SetParent(this.transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static sGridController GetInstance () {
        return Instance;
    }

    public GameObject DrawVertex (Transform parent, Vector2 coordinate) {
        GameObject asset = Instantiate(vertex, new Vector3(coordinate.x * 0.24f, coordinate.y * 0.24f, -3), vertex.transform.rotation);
        asset.transform.SetParent(parent);
        asset.GetComponent<SpriteRenderer>().color = Color.red;
        return asset;
    }

    public GameObject DrawLine (Transform parent, Vector2 v1, Vector2 v2) {
        Vector2 halfPoint = (v2 + v1) / 2;
        Vector2 delta = v2 - v1;
        float rotation = Mathf.Rad2Deg * Mathf.Atan(delta.y / delta.x);
        float magnitude = delta.magnitude;
        GameObject asset = Instantiate(line, new Vector3(halfPoint.x * 0.24f, halfPoint.y * 0.24f, -1.5f), line.transform.rotation);
        asset.transform.SetParent(parent);
        asset.transform.localScale += new Vector3(magnitude - 0.2f, -0.25f, 0);
        asset.transform.Rotate(new Vector3(0, 0, rotation));
        return asset;
    }
}
