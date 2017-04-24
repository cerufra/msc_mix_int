using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPolygon : MonoBehaviour {

    private List<sVertex> listVertex;
    private List<sLine> listLine;
    private sVertex workVertex;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Draw();
	}

    public void Init() {
        listVertex = new List<sVertex>();
        listLine = new List<sLine>();
    }

    public void Draw() {
        foreach (sVertex v in listVertex) {
            v.Draw(this.transform);
        }
        foreach (sLine l in listLine) {
            l.Draw(this.transform);
        }
    }

    public sVertex AddVertex(int X, int Y) {
        if (listVertex.Exists(V => V.IsVertex(X, Y))) {
            // Vértice já existe na estrutura
            SelectVertex(X, Y);
            return null;
        }
        sVertex v = new sVertex(X, Y);
        listVertex.Add(v);
        v.SetActive();
        if (workVertex != null) {
            workVertex.SetInactive();
        }
        workVertex = v;
        return v;
    }

    public bool SelectVertex(int X, int Y) {
        sVertex v = listVertex.Find(V => V.IsVertex(X, Y));
        if (v == null) {
            return false;
        }
        v.SetActive();
        if (workVertex != null) {
            workVertex.SetInactive();
        }
        workVertex = v;
        return true;
    }

    public void Desselect() {
        if (workVertex != null) {
            workVertex.SetInactive();
        }
    }

    public sVertex GetActiveVertex() {
        return workVertex;
    }

    public sVertex GetVertex(int X, int Y) {
        return listVertex.Find(v => v.IsVertex(X, Y));
    }

    public bool ContainsVertex (sVertex vertex) {
        return listVertex.Exists(v => v.IsVertex(vertex.Coordinate));
    }

    public sVertex ContainsVertex (Vector2 position) {
        return listVertex.Find(v => v.IsVertex(position));
    }

    public sVertex ContainsVertex (int X, int Y) {
        return listVertex.Find(v => v.IsVertex(X, Y));
    }

    public sLine AddLine (sVertex point1, sVertex point2) {
        if (listLine.Exists(L => L.IsLine(point1, point2))) {
            // Linha já existe na estrutura
            return null;
        }
        sLine l = new sLine(point1, point2);
        listLine.Add(l);
        l.SetMidHardness();
        return l;
    }
}
