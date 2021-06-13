using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour
{
    public GameObject[] keys;
    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
