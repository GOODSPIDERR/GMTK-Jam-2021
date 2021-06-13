using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestruction : MonoBehaviour
{
    public GameObject wallBreak;
    public float breakAngle = 135f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            Instantiate(wallBreak, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y - breakAngle, transform.rotation.z));
            Destroy(gameObject);
        }
    }
}
