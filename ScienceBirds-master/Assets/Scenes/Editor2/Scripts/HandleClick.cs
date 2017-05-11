using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour {

    public Button botao;
    public Transform bloco;
    public static int count = 0;
	// Use this for initialization
	void Start () {
        Button btn = botao.GetComponent<Button>();
        btn.onClick.AddListener(acao);

        bloco.GetComponent<Collider2D>().attachedRigidbody.gravityScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void acao()
    {
        Instantiate(bloco, new Vector3(0, 0, 2), Quaternion.identity).name = count.ToString();
        count++;
    }
}
