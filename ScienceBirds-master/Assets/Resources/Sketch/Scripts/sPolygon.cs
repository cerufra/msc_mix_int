using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPolygon : MonoBehaviour {

    private List<sVertex> listVertex;
    private List<sLine> listLine;
    private sVertex workVertex;
    private bool selected;
    public bool Selected {
        get { return selected; }
    }

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
        Stability = -1;
        selected = true;
    }

    public void Draw() {
        foreach (sVertex v in listVertex) {
            v.Draw(this.transform, Stability);
        }
        foreach (sLine l in listLine) {
            l.Draw(this.transform);
        }
    }

    public string AsString() {
        string s = "";
        /*foreach (sLine l in listLine) {
            s += l.AsString();
            s += "\n";
        }*/
        for(int i=0;i<listLine.Count; i++)
        {
            s += listLine[i].AsString();
            if (i != listLine.Count - 1) s += "\n";
        }
        //s.Trim();
        //s.Remove(s.Length - 2);
        //Debug.Log(s);
        return s;
    }

    public bool NoVertexes() {
        return listVertex.Count == 0;
    }

    public int[] BoundingBox() {
        int[] b = { 1000, 1000, -10, -10 };
        foreach (sVertex v in listVertex) {
            if (v.Coordinate.x < b[0]) {
                b[0] = ( int )v.Coordinate.x;
            }
            if (v.Coordinate.x > b[2]) {
                b[2] = ( int )v.Coordinate.x;
            }
            if (v.Coordinate.y < b[1]) {
                b[1] = ( int )v.Coordinate.y;
            }
            if (v.Coordinate.y > b[3]) {
                b[3] = ( int )v.Coordinate.y;
            }
        }
        return b;
    }

    public string AsMatrix() {
        string s = "";
        int[] bBox = BoundingBox();
        s += bBox[0] + ", " + bBox[1] + "\n";
        int width = bBox[2] - bBox[0];
        int height = bBox[3] - bBox[1];
        // itera sobre as linhas
        int[,] m = new int[width, height];
        for (int X = bBox[0]; X <= bBox[2]; X++) {
            for (int Y = bBox[1]; Y <= bBox[3]; Y++) {
                if (ContainsLine(X, Y) != null) {
                    m[X, Y] = Stability;
                } else {
                    m[X, Y] = -1;
                }
            }
        }
        return s;
    }

    public bool IsHollow() {
        return Stability < 0;
    }

    public void SetHollow() {
        Stability = -1;
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
        sHelper.GetInstance().GuideLines(X * 0.24f, Y * 0.24f);
        selected = true;
        return v;
    }

    public bool SelectVertex(int X, int Y) {
        sVertex v = listVertex.Find(V => V.IsVertex(X, Y));
        if (v == null) {
            return false;
        }
        if (workVertex != null) {
            workVertex.SetInactive();
        }
        v.SetActive();
        workVertex = v;
        sHelper.GetInstance().GuideLines(v.GameObject().transform.position.x, v.GameObject().transform.position.y);
        selected = true;
        return true;
    }

    public void Desselect() {
        if (workVertex != null) {
            workVertex.SetInactive();
        }
        //workVertex = null;
        selected = false;
        sHelper.GetInstance().Desselect();
    }

    public bool RemoveVertex(sVertex vertex) {
        if (vertex == null)
        {
            return false;
        }
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
        switch (sHelper.GetInstance().newLineMaterial)
        {
            case 1:
                l.SetSoftHardness();
                break;

            case 2:
                l.SetMidHardness();
                break;

            case 3:
                l.SetHardHardness();
                break;
        }
        return l;
    }
}
