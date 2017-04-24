using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCameraController : MonoBehaviour {

    public KeyCode translateUp;
    public KeyCode translateLeft;
    public KeyCode translateDown;
    public KeyCode translateRight;
    public KeyCode mouseDrag;

    public float translateSpeed;
    private float dragStart;
    private Vector3 dragOrigin;
    private bool dragging;

    public KeyCode zoomPlus;
    public KeyCode zoomMinus;

    private float orthoSize;
    public float maxZoom;
    public float minZoom;
    public float zoomStep;
    public float zoomSpeed;

	// Use this for initialization
	void Start () {
        orthoSize = Camera.main.orthographicSize;
        dragging = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(translateUp)) {
            this.transform.Translate(0.0f, translateSpeed, 0.0f);
        }

        if (Input.GetKey(translateLeft)) {
            this.transform.Translate(-translateSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(translateDown)) {
            this.transform.Translate(0.0f, -translateSpeed, 0.0f);
        }

        if (Input.GetKey(translateRight)) {
            this.transform.Translate(translateSpeed, 0.0f, 0.0f);
        }

        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0) {
            orthoSize -= zoom * zoomStep;
            orthoSize = Mathf.Clamp(orthoSize, minZoom, maxZoom);
        }

        if (Input.GetKeyDown(zoomPlus)) {
            orthoSize -= zoomStep;
            orthoSize = Mathf.Clamp(orthoSize, minZoom, maxZoom);
        }

        if (Input.GetKeyDown(zoomMinus)) {
            orthoSize += zoomStep;
            orthoSize = Mathf.Clamp(orthoSize, minZoom, maxZoom);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, orthoSize, zoomSpeed * Time.deltaTime);

        if (Input.GetKey(mouseDrag)) {
            if (!dragging) {
                dragging = true;
                dragOrigin = Input.mousePosition;
                dragStart = Time.time;
            } else {
                dragging = false;
                Vector3 mouseSpeed = (Input.mousePosition - dragOrigin) / (Time.time - dragStart);
                this.transform.Translate(-mouseSpeed * translateSpeed * 0.01f);
            }
        }

        if (Input.GetKeyUp(mouseDrag)) {
            dragging = false;
        }

        /*
        if (Input.GetKey(mouseDrag) && dragging) {
            Vector3 mouseDelta = Input.mousePosition - dragOrigin;
            this.transform.Translate(mouseDelta * translateSpeed * 0.01f);
        }

        if (Input.GetKeyDown(mouseDrag)) {
            dragOrigin = Input.mousePosition;
            Input.mouse
            dragging = true;
        }

        if (Input.GetKeyUp(mouseDrag)) {
            dragging = false;
        }
        */
    }
}
