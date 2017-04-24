using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sLine {

    // Vértices que definem a semi-reta
    private sVertex v1;
    public sVertex Vertex1 {
        get { return v1; }
        set { v1 = value; }
    }

    private sVertex v2;
    public sVertex Vertex2 {
        get { return v2; }
        set { v2 = value; }
    }

    // Identifica resistência da liha
    private int Hardness;

    // GameObject associado à linha
    private GameObject Asset;

    // Flag: Se a linha foi desenhada
    private bool Drawn;

    public sLine (sVertex point1, sVertex point2) {
        v1 = point1;
        v2 = point2;
        Hardness = 1;
    }

    public void Draw(Transform parent) {
        if (!Drawn) {
            Asset = sGridController.GetInstance().DrawLine(parent, v1.Coordinate, v2.Coordinate);
            Drawn = true;
        }
        if (Hardness == 0) {
            Asset.GetComponent<SpriteRenderer>().color = Color.cyan;
        } else if (Hardness == 1) {
            Asset.GetComponent<SpriteRenderer>().color = Color.blue;
        } else if (Hardness == 2) {
            Asset.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    public void SetSoftHardness() {
        Hardness = 0;
    }

    public void SetMidHardness() {
        Hardness = 1;
    }

    public void SetHardHardness() {
        Hardness = 2;
    }

    public bool IsLine(sVertex d1, sVertex d2) {
        return (d1 == v1 && d2 == v2) || (d1 == v2 && d2 == v1);
    }

}
