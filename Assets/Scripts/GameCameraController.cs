using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    private float lookDistance = 0.2f;
    private float maxLook = 75.0f;
    private Camera cam;

    // Use this for initialization
    void Start () 
    {
        cam = Camera.main;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // update offset for mouse
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 look = (mousePos - transform.position) * lookDistance;
        if(look.magnitude > maxLook) {
            look = Vector3.Normalize(look) * maxLook;
        }
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 tmp = player.transform.position + offset + look;
        transform.position = tmp; 
    }
}
