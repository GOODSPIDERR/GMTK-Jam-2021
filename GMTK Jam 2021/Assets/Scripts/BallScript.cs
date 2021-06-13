using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    public Transform sparks;
    public VisualEffect sparksVFX;
    bool isSliding = false;
    public SphereCollider scrapeTrigger;
    public LayerMask layerMask;
    public float dragDistance;
    public float activationSpeed = 5f;
    public AudioSource slideSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void Update()
    {
        sparks.position = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
        sparks.rotation = Quaternion.LookRotation(rb.velocity);

        Debug.DrawRay(transform.position, Vector3.down * dragDistance, Color.yellow);

        if (rb.velocity.magnitude >= activationSpeed)
        {
            scrapeTrigger.enabled = true;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * dragDistance, out hit, dragDistance, layerMask))
            {
                //Debug.Log(hit.transform.name);
                isSliding = true;
                slideSound.volume = 0.25f;

            }

            else
            {
                isSliding = false;
                slideSound.volume = 0.0f;
            }
        }


        else
        {
            isSliding = false;
            slideSound.volume = 0.0f;
            scrapeTrigger.enabled = false;
        }

        //Debug.Log(isSliding);

        if (!isSliding)
        {
            sparksVFX.Play();
        }
    }

}
