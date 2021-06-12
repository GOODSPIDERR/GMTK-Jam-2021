using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    
    // HP per second
    public float regenSpeed;
    
    public bool isAttached = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryHealing();
    }
    
    void TryHealing()
    {
        if (!isAttached && curHealth < maxHealth)
        {
            // increase health dependant on the time it takes to render a frame
            curHealth += regenSpeed * Time.deltaTime;
            if (curHealth > maxHealth) // don't overheal
            {
                curHealth = maxHealth;
            }
        }
    }
    
}
