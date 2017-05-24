using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour {

    public Button botao;
    public Transform bloco;
    public string tipo;
    public static int count = 0;
	// Use this for initialization
	void Start () {
        Button btn = botao.GetComponent<Button>();
        btn.onClick.AddListener(acao);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void acao()
    {
        GameObject objetos = GameObject.Find("Objects");

        if(objetos != null)
        {
            //GameObject obj = new GameObject();
            Object obj = Instantiate(bloco, new Vector3(-8, 3, 2), Quaternion.identity,objetos.transform);
            obj.name = tipo + "_" + count.ToString();
            
            count++;

            Transform child = objetos.transform.GetChild(objetos.transform.childCount - 1);
            Rigidbody2D f = child.GetComponent<Collider2D>().attachedRigidbody;
            f.gravityScale = 0;
            f.freezeRotation = true;
            if (child.name.Contains("Pig"))
            {
                Destroy(child.GetComponent("ABPig"));
            }
            else
            {
                Destroy(child.GetComponent("ABBlock"));
            }
            Destroy(child.GetComponent("ABParticleSystem"));
            child.GetComponent<Transform>().gameObject.AddComponent<Move>();
            child.GetComponent<Collider2D>().isTrigger = true;

        }

    }

    private void OnDestroy()
    {
        bloco.GetComponent<Collider2D>().attachedRigidbody.gravityScale = 1;
        bloco.GetComponent<Collider2D>().attachedRigidbody.freezeRotation = false;
    }
}
