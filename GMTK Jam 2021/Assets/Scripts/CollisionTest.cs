using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("There's been a collision between " + this.name + " and " + other.collider.name);
	}
}
