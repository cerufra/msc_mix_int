using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour {

    private Vector3 mousePosition;

	// Update is called once per frame
	void FixedUpdate () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        mousePosition.z = -5;

        transform.position = mousePosition;
    }

    void OnMouseDown()
    {
        gameObject.AddComponent<MoveObject>();
    }
}
