using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerScript : MonoBehaviour
{
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //Dedicated 'Reset' Key in case of unforseen catasrophies 
            transform.localPosition = new Vector3(0, 10f, 0);


        if (transform.localPosition.y < -20f) //if you fall off
        {
            transform.localPosition = new Vector3(0, 10f, 0); //spawn back to beginning
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}