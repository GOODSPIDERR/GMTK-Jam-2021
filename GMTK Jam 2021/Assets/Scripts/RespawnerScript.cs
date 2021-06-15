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
        if (Input.GetKeyDown(KeyCode.R) || transform.localPosition.y < -100f) //Dedicated 'Reset' Key in case of unforseen catasrophies 
            transform.localPosition = new Vector3(0, 10f, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}