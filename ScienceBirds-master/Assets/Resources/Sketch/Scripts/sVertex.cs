using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sVertex {

    // Coordenada no vértice no grid
    private Vector2 coord;
    public Vector2 Coordinate {
        get { return coord; }
        set { coord = value; }
    }

    // GameObject representante do vértice
    private GameObject Asset;

    public GameObject GameObject() {
        return Asset;
    }

    // Flag: Vértice Selecionado
    private bool Active;

    // Flag: Se o vértice foi Desenhado
    private bool Drawn;

    // Construtor (int, int)
    public sVertex (int X, int Y) {
        coord.x = X;
        coord.y = Y;
        Drawn = false;
    }

    public void Draw (Transform parent, int hardness) {
        if (!Drawn) {
            Asset = sGridController.GetInstance().DrawVertex(parent, coord);
            Drawn = true;
        }
        if (Active) {
            Asset.GetComponent<SpriteRenderer>().color = Color.red;
        } else {
            if (hardness == 2) {
                Asset.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f);
            } else if (hardness == 1) {
                Asset.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            } else if (hardness == 0) {
                Asset.GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
            }
        }
    }

    // Define coordenada a partir de uma par de inteiros
    public Vector2 SetCoordinate (int X, int Y) {
        coord.x = X;
        coord.y = Y;
        return coord;
    }

    public void SetActive() {
        Active = true;
    }

    public void SetInactive() {
        Active = false;
    }

    public bool IsVertex(Vector2 coordinate) {
        return coord.x == coordinate.x && coord.y == coordinate.y;
    }

    public bool IsVertex(int X, int Y) {
        return ( int )coord.x == X && ( int )coord.y == Y;
    }
}
