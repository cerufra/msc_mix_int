using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateObject : MonoBehaviour {

    private Vector3 mousePosition;

    void Awake()
    {
        if (ELevel.instance.loadingLevelFromFile)
        {
            gameObject.AddComponent<MoveObject>();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (ELevel.instance.loadingLevelFromFile)
            return;

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        mousePosition.z = -5;

        transform.position = mousePosition;
    }

    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            gameObject.AddComponent<MoveObject>();
    }
}
