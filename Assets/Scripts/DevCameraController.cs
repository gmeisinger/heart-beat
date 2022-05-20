using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCameraController : MonoBehaviour
{
    private Camera cam;
    private Vector3 lastPos;

    public float mouseSensitivity = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // LateUpdate is called after Update each frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            lastPos = Input.mousePosition;//cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;//cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 delta = mousePos - lastPos;
            transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, 0);
            lastPos = Input.mousePosition;
        }
    }
}
