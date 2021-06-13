using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestruction : MonoBehaviour
{
    //public GameObject wallBreak;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            //Instantiate(wallBreak, transform.position, Quaternion.Euler(0, 135f, 0));
            Destroy(gameObject);
        }
    }
}
