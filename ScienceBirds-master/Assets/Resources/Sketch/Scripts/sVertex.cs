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

    public void Draw (Transform parent) {
        if (!Drawn) {
            Asset = sGridController.GetInstance().DrawVertex(parent, coord);
            Drawn = true;
        }
        if (Active) {
            Asset.GetComponent<SpriteRenderer>().color = Color.green;
        } else {
            Asset.GetComponent<SpriteRenderer>().color = Color.gray;
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
