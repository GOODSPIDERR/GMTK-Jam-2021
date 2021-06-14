using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerScript : MonoBehaviour
{
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (startPosition.y < -10f) //if you fall off
        {
            transform.position = new Vector3(0, 0, 0); //spawn back to beginning
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}