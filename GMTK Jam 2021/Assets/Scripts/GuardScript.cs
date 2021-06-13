using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class GuardScript : MonoBehaviour
{
    private NavMeshAgent guard;
    private GameObject player;
    //private FirstPersonController fpc;
    private Rigidbody rb;
    public float hearingRange, spottingRange, spottingAngle, pushForce;
    private bool agro, attack;
    Animator guardAnim;

    void Start()
    {
        agro = attack = false;
        guard = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        guardAnim = GetComponent<Animator>();

    }

    void Update()
    {
        if (agro)
        {
            if (hearing() > hearingRange && !spotting()) 
            {
                agro = false;
                guardAnim.SetBool("agro", false);
                guard.SetDestination(transform.position);
            }
            else
            {
                guard.SetDestination(player.transform.position);
                if (hearing() <= 1)
                {
                    attack = true;
                }
            }
        }
        else
        {
  
                if (hearing() < (1.5 * hearingRange))
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    guard.SetDestination(player.transform.position);
                }

                if (hearing() < (0.5 * hearingRange))
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    guard.SetDestination(player.transform.position);
                }
           
            else
            {
                if (hearing() < hearingRange)
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    guard.SetDestination(player.transform.position);
                }
            }
            if (spotting() && hearing() < spottingRange)
            {
                agro = true;
                guardAnim.SetBool("agro", true);
                guard.SetDestination(player.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        if (attack)
        {
            knockback();
            attack = false;
        }
    }

    double hearing()
    {
        double x, z, dist;
        x = transform.position.x - player.transform.position.x;
        z = transform.position.z - player.transform.position.z;
        x = x * x;
        z = z * z;
        dist = Math.Sqrt(x + z);
        return dist;
    }

    bool spotting()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDir, out hit, Mathf.Infinity, layerMask))
        {
            return false;
        }

        if (angle < spottingAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void knockback()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir.y = 0;
        rb.AddForce(targetDir.normalized * pushForce, ForceMode.Impulse);

    }
}
