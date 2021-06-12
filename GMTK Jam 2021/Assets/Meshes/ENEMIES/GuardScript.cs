using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class GuardScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject enemy;
    //private FirstPersonController fpc;
    private Rigidbody erb;
    public float hearingRange, spottingRange, spottingAngle, pushForce;
    private bool agro, attack;
    Animator guardAnim;

    void Start()
    {
        agro = attack = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemy = GameObject.FindGameObjectWithTag("Player");
        //fpc = enemy.GetComponent<FirstPersonController>();
        erb = enemy.GetComponent<Rigidbody>();
        guardAnim = GetComponent<Animator>();

    }

    void Update()
    {
        if (agro)
        {
            if (hearing() > hearingRange && !spotting()) //fpc.getCrouch() &&
            {
                agro = false;
                guardAnim.SetBool("agro", false);
                agent.SetDestination(transform.position);
            }
            else
            {
                agent.SetDestination(enemy.transform.position);
                if (hearing() <= 1)
                {
                    attack = true;
                }
            }
        }
        else
        {
            //if (fpc.getSprint())
            //{
                if (hearing() < (1.5 * hearingRange))
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
            //}
            //else if (fpc.getCrouch())
           // {
                if (hearing() < (0.5 * hearingRange))
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
           // }
            else
            {
                if (hearing() < hearingRange)
                {
                    agro = true;
                    guardAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
            }
            if (spotting() && hearing() < spottingRange)
            {
                agro = true;
                guardAnim.SetBool("agro", true);
                agent.SetDestination(enemy.transform.position);
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
        x = transform.position.x - enemy.transform.position.x;
        z = transform.position.z - enemy.transform.position.z;
        x = x * x;
        z = z * z;
        dist = Math.Sqrt(x + z);
        return dist;
    }

    bool spotting()
    {
        Vector3 targetDir = enemy.transform.position - transform.position;
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
        Vector3 targetDir = enemy.transform.position - transform.position;
        targetDir.y = 0;
        erb.AddForce(targetDir.normalized * pushForce, ForceMode.Impulse);

    }
}
