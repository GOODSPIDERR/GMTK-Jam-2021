using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestruction : MonoBehaviour
{
    public GameObject wallBreak;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            Instantiate(wallBreak, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
