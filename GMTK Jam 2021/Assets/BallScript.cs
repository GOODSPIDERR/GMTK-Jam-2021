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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void Update()
    {
        sparks.position = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
        sparks.rotation = Quaternion.LookRotation(rb.velocity);

        Debug.DrawRay(transform.position, Vector3.down * 1.4f, Color.yellow);

        if (rb.velocity.magnitude >= 5f)
        {
            scrapeTrigger.enabled = true;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 1.4f, out hit, 1.4f, layerMask))
            {
                Debug.Log(hit.transform.name);
                isSliding = true;
            }

            else
            {
                isSliding = false;
            }
        }


        else
        {
            isSliding = false;
            scrapeTrigger.enabled = false;
        }

        Debug.Log(isSliding);

        if (!isSliding)
        {
            sparksVFX.Play();
        }
    }

}
