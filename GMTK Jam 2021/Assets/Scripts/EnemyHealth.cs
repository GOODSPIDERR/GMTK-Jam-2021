using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    public float Health = 100f;

    public float invlunTime;
    public float lastHit;

    public bool isDead;
    Animator animator;
    Rigidbody rigidbody;
    BoxCollider collider;
    NavMeshAgent navMesh;
    GuardScript guardScript;

    // Start is called before the first frame update
    void Start()
    {
        lastHit = 0f;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        navMesh = GetComponent<NavMeshAgent>();
        guardScript = GetComponent<GuardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0f)
        {
            Die();
        }
        // Debug.Log(Health);
    }


    public void tryToDamage(float damage)
    {
        if (Time.time > lastHit + invlunTime)
        {
            lastHit = Time.time;
            if (Health - damage < 0)
            {
                isDead = true;
                Destroy(this);
            }
            else
            {
                Health -= damage;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            Health -= otherRb.velocity.magnitude;

            Vector3 direction = transform.position - other.transform.position;
            otherRb.AddForce(-direction * 30f, ForceMode.VelocityChange);
        }
    }

    void Die()
    {
        rigidbody.isKinematic = false;
        animator.enabled = false;
        collider.enabled = false;
        navMesh.enabled = false;
        guardScript.enabled = false;
        Destroy(gameObject, 5);
    }
}
