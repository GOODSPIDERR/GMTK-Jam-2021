using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float rotationSpeed = 0.0f;
    private void Start() //Makes sure the mouse is unlocked when you enter the main menu
    {
        Cursor.lockState = CursorLockMode.None;
    }

    //Slowly rotates the camera. Not needed anymore now that it's just an image
    /*
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0);
    }
    */
}
