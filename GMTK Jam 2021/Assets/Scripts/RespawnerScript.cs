using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerScript : MonoBehaviour
{
    public GameObject quitObject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            quitObject.SetActive(true); //displays quit confirmation 
        }

        if (transform.position.y < -100f) //if you fall off
        {
            transform.position = new Vector3(-72.5f, 120.4f, -98.8f); //spawn back to beginning
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}