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

    public sLine(sVertex point1, sVertex point2, int hardness) {
        v1 = point1;
        v2 = point2;
        Hardness = hardness;
    }

    public GameObject GameObject() {
        return Asset;
    }

    public void Draw(Transform parent) {
        if (!Drawn) {
            Asset = sGridController.GetInstance().DrawLine(parent, v1.Coordinate, v2.Coordinate);
            Drawn = true;
        }
        if (Hardness == 0) {
            Asset.GetComponent<SpriteRenderer>().color = new Color(0f, 0.75f, 1f);
        } else if (Hardness == 1) {
            Asset.GetComponent<SpriteRenderer>().color = new Color(0f, 0.50f, 0.75f);
        } else if (Hardness == 2) {
            Asset.GetComponent<SpriteRenderer>().color = new Color(0f, 0.25f, 0.50f);
        }
    }

    public int GetHardness() {
        return Hardness;
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

    public bool IsEndPoint(sVertex vertex) {
        return v1 == vertex || v2 == vertex;
    }

    public bool IsLine(sVertex d1, sVertex d2) {
        return (d1 == v1 && d2 == v2) || (d1 == v2 && d2 == v1);
    }

    public bool Contains(int X, int Y) {
        float distance = DistanceTo(X, Y);
        if (distance > 0.45) {
            // Distância entre reta e ponto é significante
            return false;
        }
        Vector2 v3 = new Vector2(X, Y);
        float DistanceV2V1 = (v2.Coordinate - v1.Coordinate).magnitude;
        float DistanceV3V1 = (v3 - v1.Coordinate).magnitude;
        float DistanceV3V2 = (v3 - v2.Coordinate).magnitude;
        if (DistanceV3V1 > DistanceV2V1) {
            // Distância entre o ponto e extremidade 1 > comprimento do segmento
            return false;
        }
        if (DistanceV3V2 > DistanceV2V1) {
            // Distância entre o ponto e extremidade 1 > comprimento do segmento
            return false;
        }
        return true;
    }

    public float DistanceTo(int X, int Y) {
        Vector2 delta = v2.Coordinate - v1.Coordinate;
        return Mathf.Abs(delta.y * X - delta.x * Y + v2.Coordinate.x * v1.Coordinate.y - v2.Coordinate.y * v1.Coordinate.x) / delta.magnitude;
    }

}
