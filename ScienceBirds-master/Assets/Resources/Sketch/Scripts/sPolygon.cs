﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPolygon : MonoBehaviour {

    private List<sVertex> listVertex;
    private List<sLine> listLine;
    private sVertex workVertex;

    private int Stability;

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
        Stability = 1;
    }

    public void Draw() {
        foreach (sVertex v in listVertex) {
            v.Draw(this.transform, Stability);
        }
        foreach (sLine l in listLine) {
            l.Draw(this.transform);
        }
    }

    public void SetUnstable() {
        Stability = 0;
    }

    public void SetStandart() {
        Stability = 1;
    }

    public void SetStable() {
        Stability = 2;
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

    public bool RemoveVertex(sVertex vertex) {
        List<sLine> toRemove = listLine.FindAll(l => l.IsEndPoint(vertex));
        foreach (sLine l in toRemove) {
            RemoveLine(l);
        }
        listVertex.Remove(vertex);
        Destroy(vertex.GameObject());
        // Se último vértice foi excluído, auto-destrói (retorna true)
        if (listVertex.Count == 0) {
            return true;
        }
        return false;
    }

    public void RemoveLine(sLine line) {
        if (listLine.Remove(line)) {
            Destroy(line.GameObject());
        } else {
            throw (new UnityException("RemoveLine() -> Objeto inexistente"));
        }
    }

    public void SegmentLine(int X, int Y) {
        sLine original = listLine.Find(l => l.Contains(X, Y));
        sVertex newPoint = new sVertex(X, Y);
        listLine.Add(new sLine(original.Vertex1, newPoint, original.GetHardness()));
        listLine.Add(new sLine(newPoint, original.Vertex2, original.GetHardness()));
        Destroy(original.GameObject());
        listLine.Remove(original);
        AddVertex(X, Y);
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

    public sLine ContainsLine (int X, int Y) {
        return listLine.Find(l => l.Contains(X, Y));
    }

    public bool ContainsLine(sLine line) {
        return listLine.Exists(l => l.IsLine(line.Vertex1, line.Vertex2));
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