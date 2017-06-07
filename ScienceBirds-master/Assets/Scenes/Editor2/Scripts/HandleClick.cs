using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleClick : MonoBehaviour {

    public Button botao;
    public Button mat;
    public Transform bloco;
    public string tipo;
    public static int count = 0;
    public static string material = "wood";

    public Sprite wood;
    public Sprite ice;
    public Sprite stone;

	// Use this for initialization
	void Start () {
        if (botao != null)
        {
            Button btn = botao.GetComponent<Button>();
            btn.onClick.AddListener(acao);
        }
        if(mat != null)
        {
            Button btn = mat.GetComponent<Button>();
            btn.onClick.AddListener(changeMat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tipo.Contains("Basic") && botao != null)
        {
            if (material == "wood")
            {
                botao.GetComponent<Button>().image.sprite = wood;
            }
            else if (material == "ice")
            {
                botao.GetComponent<Button>().image.sprite = ice;
            }
            else if (material == "stone")
            {
                botao.GetComponent<Button>().image.sprite = stone;
            }
        }
    }

    void acao()
    {
        GameObject objetos = GameObject.Find("Objects");

        if(objetos != null)
        {
            //GameObject obj = new GameObject();
            Object obj = Instantiate(bloco, new Vector3(-8, 3, 2), Quaternion.identity,objetos.transform);
            obj.name = material + "_" + tipo + "_" + count.ToString();
            
            count++;

            Transform child = objetos.transform.GetChild(objetos.transform.childCount - 1);
            Rigidbody2D f = child.GetComponent<Collider2D>().attachedRigidbody;
            f.gravityScale = 0;
            f.freezeRotation = true;
            if (child.name.Contains("Basic"))
            {
                Destroy(child.GetComponent("ABPig"));
            }
            else
            {
                MATERIALS mat = (MATERIALS)System.Enum.Parse(typeof(MATERIALS), material);
                child.GetComponent<ABBlock>().SetMaterial(mat);
                Destroy(child.GetComponent("ABBlock"));
            }
            Destroy(child.GetComponent("ABParticleSystem"));
            child.GetComponent<Transform>().gameObject.AddComponent<Move>();
            child.GetComponent<Collider2D>().isTrigger = true;

        }

    }

   /* private void OnDestroy()
    {
        if(bloco != null)
        {
            bloco.GetComponent<Collider2D>().attachedRigidbody.gravityScale = 1;
            bloco.GetComponent<Collider2D>().attachedRigidbody.freezeRotation = false;
        }
    }*/

    void changeMat()
    {
        if(material == "wood")
        {
            material = "ice";
        }else if (material == "ice")
        {
            material = "stone";
        }
        else if (material == "stone")
        {
            material = "wood";
        }
    }
}
