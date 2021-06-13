using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 100f;
    
    public float invlunTime;
    public float lastHit;
    
    public bool isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        lastHit = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void tryToDamage(float damage)
    {
        if (Time.time > lastHit + invlunTime)
        {
            lastHit = Time.time;
            if (Health - damage < 0)
            {
                isDead = true;
            } 
            else
            {
                Health -= damage;
            }
        }
    }
}
