using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    // magnitude and direction in here
    public Vector3 velocity;
    public Rigidbody rigidBody;
    
    // the amount of damage (speed) needed to destory an object
    public float thresholdForBreakingObjects;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        // we immediatly squre the threshold because we are using Vector3 sqrMagnitude to compare
        // because it's faster
        thresholdForBreakingObjects = thresholdForBreakingObjects * thresholdForBreakingObjects;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rigidBody.velocity;
    }
    
    void OnCollisionEnter(Collision other)
    {
        int numContacts = other.contactCount;
        Debug.Log("there are " + numContacts + " contact points!");
        Collider objectCollider, ballCollider;
        float ballVelocitySqr;
        
        for (int i = 0; i < numContacts; i++) {
            objectCollider = other.GetContact(i).thisCollider;
            ballCollider = other.GetContact(i).thisCollider;
            ballVelocitySqr = ballCollider.attachedRigidbody.velocity.sqrMagnitude;
            
            Debug.Log("objectCollider.tag: " + objectCollider.tag);
            Debug.Log("ballCollider.tag: " + ballCollider.tag);
            
            if (objectCollider.tag == "BreakableObject") // check for breakable object
            {
                if (ballVelocitySqr > thresholdForBreakingObjects) 
                {
                    objectCollider.GetComponent<Break>().isBroken = true;
                }
            }
            else if (objectCollider.tag == "Enemy") // check for enemy
            {
                objectCollider.GetComponent<EnemyHealth>().tryToDamage(Mathf.Sqrt(ballVelocitySqr));
            }
            else if (objectCollider.tag == "Bullet") // check for bullets
            {
                
            }
            else if (objectCollider.tag == "Player") // check for the player
            {
                
            }
        }
    }
}
