using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.EventSystems;

public class sHelper : MonoBehaviour {

    // Constantes de estado
    //Mouse sobre célula vazia
    private const int VOID = 0; // Mouse sobre célula vazia, Nenhum polígono selecionado
    private const int CELL = 10; // Mouse sobre célula vazia, Um polígono em edição
    //Mouse sobre vértice
    private const int SAME_VERTEX = 20; // Mouse sobre um vértice do polígono em edição
    private const int DIFF_VERTEX = 40; // Mouse sobre um vértice de outro polígono
    private const int NO_VERTEX = 60; // Mouse sobre um vértice, nenhum polígono em edição
    //Mouse sobre lnha
    private const int SAME_LINE = 30; // Mouse sobre uma linha do polígono em edição
    private const int DIFF_LINE = 50; // Mouse sobre uma linha de outro polígono
    private const int NO_LINE = 70; // Mouse sobre uma linha, nenhum polígono em edição

    public KeyCode LeftClick;
    public KeyCode RightClick;
    public KeyCode Ocos;
    public GameObject InfoBar;
    public GameObject Polygon;
    public GameObject Menu;
    public GameObject LinePrefab;
    public GameObject loading;
    public string path;

    // Posiçao do mouse no Grid
    private int MouseX;
    private int MouseY;
    private int MouseState;
    private bool _lock = false;

    // Conjunto de estruturas criadas
    private List<GameObject> listPolygon;
    
    // Isnstância do sHelper
    private static sHelper Instance;

    // Instância de polígono em edição
    private sPolygon workPoly;

    // Flag: Menu ativo?
    private bool ActiveMenu;

    // 4 linhas guia: horizontal, vertical, 45º e -45º
    private GameObject[] guideLines;


	// Use this for initialization
	void Start () {
        Instance = this;
        listPolygon = new List<GameObject>();
        workPoly = null;
        MouseState = VOID;
        ActiveMenu = false;
        guideLines = new GameObject[4];
        for (int n = 0; n < 4; n++) {
            guideLines[n] = Instantiate(LinePrefab, new Vector3(0f, 0f, -1f), LinePrefab.transform.rotation);
            guideLines[n].transform.localScale += new Vector3(499.0f, -0.9f, 0);
            guideLines[n].transform.Rotate(new Vector3(0, 0, n * 45));
            guideLines[n].GetComponent<SpriteRenderer>().color = new Color(130, 0, 205, 90);
            guideLines[n].SetActive(false);
        }
        path = Application.dataPath + "/StreamingAssets/Line2Blocks/";
    }

    public void Desselect() {
        for (int n = 0; n < 4; n++) {
            guideLines[n].SetActive(false);
        }
    }

    public void GuideLines(float X, float Y) {
        for (int n = 0; n < 4; n++) {
            guideLines[n].transform.position = new Vector3(X, Y, -1);
            guideLines[n].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!ActiveMenu) {
            if (Input.GetKeyDown(LeftClick)) {
                ResolveLeftClick();
            }

            if (Input.GetKeyDown(RightClick)) {
                ResolveRightClick();
            }
        }

        if (Input.GetKeyDown(Ocos)) {
            GetHollows();
        }
    }

    public static sHelper GetInstance() {
        return Instance;
    }

    public void ResolveLeftClick () {

        //if (!EventSystem.current.IsPointerOverGameObject() return;

        if (MouseX < 0 || MouseY < 0 || _lock)
        {
            return;
        }
        GameObject poly;
        sVertex v1;
        sVertex v2;
        switch (MouseState) {
            case VOID:
                // Declara nova Estrutura e adiciona um vértice na posição do mouse
                poly = Instantiate(Polygon);
                poly.name = "Estrutura_" + (listPolygon.Count + 1).ToString();
                listPolygon.Add(poly);
                workPoly = poly.GetComponent<sPolygon>();
                workPoly.Init();
                workPoly.AddVertex(MouseX, MouseY);
                break;

            case CELL:
                // Declara novo vértice, traça linha entre novo vértice e vértice ativo da estrutura
                v1 = workPoly.GetActiveVertex();
                int deltaX = (int) v1.Coordinate.x - MouseX;
                int deltaY = ( int )v1.Coordinate.y - MouseY;
                if (deltaX == 0 || deltaY == 0) {
                    // mesma linha horizontal ou vertical
                    v2 = workPoly.AddVertex(MouseX, MouseY);
                } else if (Mathf.Abs(deltaX) == Mathf.Abs(deltaY)) {
                    // Ângulo de 45º
                    v2 = workPoly.AddVertex(MouseX, MouseY);
                } else {
                    if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) {
                        int coordY = (int)v1.Coordinate.y - Mathf.Abs(deltaX) * Mathf.Abs(deltaY) / deltaY;
                        if (coordY >= 0 && coordY < sGridController.GetInstance().maxHeight && Mathf.Abs(deltaY) > Mathf.Abs(deltaX) / 2) {
                            // Reta em ângulo
                            v2 = workPoly.AddVertex(MouseX, coordY);
                        } else {
                            // Reta em nível
                            v2 = workPoly.AddVertex(MouseX, ( int )v1.Coordinate.y);
                        }
                    } else {
                        int coordX = (int)v1.Coordinate.x - Mathf.Abs(deltaY) * Mathf.Abs(deltaX) / deltaX;
                        if (coordX >= 0 && coordX < sGridController.GetInstance().maxWidth && Mathf.Abs(deltaX) > Mathf.Abs(deltaY) / 2) {
                            // Reta em ângulo
                            v2 = workPoly.AddVertex(coordX, MouseY);
                        } else {
                            // Reta em nível
                            v2 = workPoly.AddVertex(( int )v1.Coordinate.x, MouseY);
                        }
                    }
                }
                workPoly.AddLine(v1, v2);
                break;

            case NO_VERTEX:
                // Seleciona Estrutura e vértice
                workPoly = listPolygon.Find(p => p.GetComponent<sPolygon>().SelectVertex(MouseX, MouseY)).GetComponent<sPolygon>();
                break;

            case SAME_VERTEX:
                // Cria linha entre os dois vértices (ativo e clicado)
                v1 = workPoly.GetActiveVertex();
                v2 = workPoly.GetVertex(MouseX, MouseY);
                workPoly.AddLine(v1, v2);
                workPoly.SelectVertex(MouseX, MouseY);
                break;

            case DIFF_VERTEX:
                // Desseleciona esrutura, seleciona nova esrutura e vértice
                workPoly.Desselect();
                workPoly = listPolygon.Find(p => p.GetComponent<sPolygon>().SelectVertex(MouseX, MouseY)).GetComponent<sPolygon>();
                break;

            case NO_LINE:
                // Seleciona estrutura e segmenta reta
                workPoly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsLine(MouseX, MouseY) != null).GetComponent<sPolygon>();
                workPoly.SegmentLine(MouseX, MouseY);
                break;

            case SAME_LINE:
                // Segmenta reta
                workPoly.SegmentLine(MouseX, MouseY);
                break;

            case DIFF_LINE:
                // Desseleciona estrutura, seleciona nova estrutura e segmenta a linha
                workPoly.Desselect();
                workPoly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsLine(MouseX, MouseY) != null).GetComponent<sPolygon>();
                workPoly.SegmentLine(MouseX, MouseY);
                break;

        }
    }

    public void ResolveRightClick () {
        switch (MouseState) {
            case VOID:
                // Sem ação
                break;

            case CELL:
                // Desseleciona estrutura
                workPoly.Desselect();
                workPoly = null;
                break;

            case NO_VERTEX:
                // Definir resistência interna da estrutura
                OpenMenu(true);
                break;

            case SAME_VERTEX:
                workPoly.SelectVertex(MouseX, MouseY);
                break;

            case DIFF_VERTEX:
                // Definir resistência interna de outra estrutura
                OpenMenu(true);
                break;

            case NO_LINE:
                // Definir resistência da linha
                OpenMenu(false);
                break;

            case SAME_LINE:
                // Definir resistência da linha
                OpenMenu(false);
                break;

            case DIFF_LINE:
                // Definir resistência da linha (de outra estrutura)
                OpenMenu(false);
                break;

        }
    }

    public void OpenMenu(bool VertexMenu) {
        ActiveMenu = true;
        Menu.SetActive(true);
        sMenu m = Menu.GetComponent<sMenu>();
        m.Init(MouseX, MouseY, VertexMenu);
    }

    public void ResolveMenu(int X, int Y, int option) {
        ActiveMenu = false;
        Menu.SetActive(false);
        // Operar sobre elemento
        switch (option) {
            case -1:
                // Deixar oco
                SetHollow(X, Y);
                break;

            case 0:
                // Remover elemento
                RemoveAt(X, Y);
                break;

            case 1:
                // Tornar frágil
                SetEasy(X, Y);
                break;

            case 2:
                // Tornar padrão
                SetStandart(X, Y);
                break;

            case 3:
                // Tornar resistente;
                SetHard(X, Y);
                break;
        }
    }

    public void SetHollow(int X, int Y) {
        object target = ObjectAt(X, Y);
        sPolygon poly;
        if (target is sVertex) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsVertex(target as sVertex)).GetComponent<sPolygon>();
            poly.SetHollow();
        }
    }

    public void SetEasy(int X, int Y) {
        object target = ObjectAt(X, Y);
        sPolygon poly;
        if (target is sLine) {
            (target as sLine).SetSoftHardness();
        } else if (target is sVertex) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsVertex(target as sVertex)).GetComponent<sPolygon>();
            poly.SetUnstable();
        }
    }

    public void SetStandart(int X, int Y) {
        object target = ObjectAt(X, Y);
        sPolygon poly;
        if (target is sLine) {
            (target as sLine).SetMidHardness();
        } else if (target is sVertex) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsVertex(target as sVertex)).GetComponent<sPolygon>();
            poly.SetStandart();
        }
    }

    public void SetHard(int X, int Y) {
        object target = ObjectAt(X, Y);
        sPolygon poly;
        if (target is sLine) {
            (target as sLine).SetHardHardness();
        } else if (target is sVertex) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsVertex(target as sVertex)).GetComponent<sPolygon>();
            poly.SetStable();
        }
    }

    public void RemoveAt(int X, int Y) {
        object target = ObjectAt(X, Y);
        sPolygon poly;
        if (target is sLine) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsLine(target as sLine)).GetComponent<sPolygon>();
            poly.RemoveLine(target as sLine);
        } else if (target is sVertex) {
            poly = listPolygon.Find(p => p.GetComponent<sPolygon>().ContainsVertex(target as sVertex)).GetComponent<sPolygon>();
            if (poly.RemoveVertex(target as sVertex)) {
                listPolygon.Remove(poly.gameObject);
                Destroy(poly.gameObject);
            }
        }
    }

    public void UpdateMouseState () {

        // Identifica polígono sob o cursor do mouse
        object hovering = ObjectAt(MouseX, MouseY);

        if (hovering == null) {
            //Mouse sobre um célula vazia
            if (workPoly == null) {
                // Nenhum polígono em edição
                MouseState = VOID;
            } else if (workPoly != null) {
                // Um polígono em edição
                MouseState = CELL;
            }
        } else {
            if (hovering is sVertex) {
                // Mouse sobre um vértice
                if (workPoly == null) {
                    // Nenhum polígono em edição
                    MouseState = NO_VERTEX;
                } else {
                    if (workPoly.ContainsVertex(hovering as sVertex)) {
                        // Vértice pertence ao polígono em edição
                        MouseState = SAME_VERTEX;
                    } else {
                        // Vértice NÃO pertence ao polígono em edição
                        MouseState = DIFF_VERTEX;
                    }
                }

            } else if (hovering is sLine) {
                // Mouse sobre uma linha
                if (workPoly == null) {
                    // Nenhum polígono em edição
                    MouseState = NO_LINE;
                } else {
                    if (workPoly.ContainsLine(hovering as sLine)) {
                        // Linha pertence ao polígono em edição
                        MouseState = SAME_LINE;
                    } else {
                        // Linha NÃO pertence ao polígono em edição
                        MouseState = DIFF_LINE;
                    }
                }

            }
        }

    }

    public void LockMouse()
    {
        _lock = true;
        UpdateMousePosition(null, null);
    }

    public void UnlockMouse()
    {
        _lock = false;
    }

    public void UpdateMousePosition (int? X = null, int? Y = null) {
       if (X == null || Y == null || _lock) {
            MouseX = -1;
            MouseY = -1;
       } else {
            MouseX = (int)X;
            MouseY = (int)Y;
       }
       UpdateMouseState();
       UpdateMouseInfo();
    }

    public void UpdateMouseInfo() {
        InfoBar.SendMessage("UpdatePointerPosition", new Vector2(MouseX, MouseY));

        switch (MouseState) {
            case VOID:
                InfoBar.SendMessage("UpdateLeftClick", "Nova Estrutura; Novo vértice");
                InfoBar.SendMessage("UpdateRightClick", " ");
                break;

            case CELL:
                InfoBar.SendMessage("UpdateLeftClick", "Nova linha");
                InfoBar.SendMessage("UpdateRightClick", "Desselecionar estrutura");
                break;

            case NO_VERTEX:
                InfoBar.SendMessage("UpdateLeftClick", "Editar estrutura; Selecionar vértice");
                InfoBar.SendMessage("UpdateRightClick", "Definir resistência (interna)");
                break;

            case SAME_VERTEX:
                InfoBar.SendMessage("UpdateLeftClick", "Fechar loop;");
                InfoBar.SendMessage("UpdateRightClick", "Selecionar vértice");
                break;

            case DIFF_VERTEX:
                InfoBar.SendMessage("UpdateLeftClick", "Editar estrutura; Selecionar vértice");
                InfoBar.SendMessage("UpdateRightClick", "Definir resistência (interna)");
                break;

            case NO_LINE:
                InfoBar.SendMessage("UpdateLeftClick", "Editar estrutura; Segmentar reta");
                InfoBar.SendMessage("UpdateRightClick", "Definir resistência (casca)");
                break;

            case SAME_LINE:
                InfoBar.SendMessage("UpdateLeftClick", "Segmentar reta");
                InfoBar.SendMessage("UpdateRightClick", "Definir resistência (casca)");
                break;

            case DIFF_LINE:
                InfoBar.SendMessage("UpdateLeftClick", "Editar estrutura; Segmentar reta");
                InfoBar.SendMessage("UpdateRightClick", "Definir resistência (casca)");
                break;

        }
    }

    public object ObjectAt (int X, int Y) {
        object target;
        foreach (GameObject poly in listPolygon) {
            target = poly.GetComponent<sPolygon>().ContainsVertex(X, Y);
            if (target != null) {
                return target;
            }
            target = poly.GetComponent<sPolygon>().ContainsLine(X, Y);
            if (target != null) {
                return target;
            }
        }
        return null;
    }

    public string[] GetHollows() {
        List<string> r = new List<string>();
        sPolygon p;
        foreach (GameObject g in listPolygon) {
            p = g.GetComponent<sPolygon>();
            if (p.IsHollow()) {
                r.Add(p.AsString());
            }
        }
        return r.ToArray();
    }

    public void WritePoints() {
        loading.SetActive(true);
        string[] points = GetHollows();
        System.IO.File.WriteAllLines(path + "pontos", points);

        ProcessStartInfo startInfo = new ProcessStartInfo("java", "-jar " + path + "Lines2Blocks.jar");
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.CreateNoWindow = true;
        startInfo.WorkingDirectory = path;
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        Process p = Process.Start(startInfo);

        //p.WaitForExit(45000);

        /*while (!p.StandardOutput.EndOfStream)
        {
            string line = p.StandardOutput.ReadLine();
            // do something with line
            UnityEngine.Debug.Log(line);
        }*/

        StartCoroutine("Wait");
        StartCoroutine("LoadingText");
    }

    IEnumerator LoadingText()
    {
        while (true)
        {
            yield return new WaitForSeconds(01);
            loading.transform.GetChild(1).GetComponent<Text>().text += " .";
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(04);
        SceneManager.LoadScene("Editor2", LoadSceneMode.Single);
    }

    public string[] GetFilled() {
        List<string> s = new List<string>();
        sPolygon p;
        foreach (GameObject g in listPolygon) {
            p = g.GetComponent<sPolygon>();
            if (!p.IsHollow()) {
                s.Add(p.AsMatrix());
            }
        }
        return s.ToArray();
    }
}


