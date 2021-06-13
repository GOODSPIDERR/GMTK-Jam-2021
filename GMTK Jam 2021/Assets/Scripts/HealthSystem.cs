using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    
    // HP per second
    public float regenSpeed = 2f;
    
    public bool shouldRegenHP = true;
    
    public float lastHit;
    public float invlunTime;
    
    public Image HPBar;
    
    CinemachineVirtualCamera mainCamera;

    public AudioSource ouch;
    public AudioSource dying;
    
    // Start is called before the first frame update
    void Start()
    {
        ouch = FindObjectOfType<AudioSource>(CompareTag("PlayerOuch"));
        dying = FindObjectOfType<AudioSource>(CompareTag("PlayerDying"));
        mainCamera = GameObject.FindGameObjectWithTag("MainCameraCM").GetComponent<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        TryHealing();
    }
    
    public void tryToDamage(float damage)
    {
        if (Time.time > lastHit + invlunTime)
        {
            lastHit = Time.time;
            if (curHealth - damage < 0)
            {
                Destroy(this);
            }
            else
            {
                curHealth -= damage;
            }
        }
    }
    
    void TryHealing()
    {
        if (Time.time > lastHit + invlunTime && curHealth < maxHealth)
        {
            // increase health dependant on the time it takes to render a frame
            curHealth += regenSpeed * Time.deltaTime;
            UpdateUIHealth();
            if (curHealth > maxHealth) // don't overheal
            {
                curHealth = maxHealth;
            }
        }
    }
    
    void UpdateUIHealth()
    {
        HPBar.fillAmount = curHealth / maxHealth;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Guard" && Time.time > lastHit + invlunTime)
        {
            Debug.Log("Player hit guard!");
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            curHealth -= 5;
            UpdateUIHealth();

            Vector3 direction = transform.position - other.transform.position;
            otherRb.AddForce(-direction * 30f, ForceMode.VelocityChange);

            ShakeCamera(otherRb.velocity.magnitude / 2, 0.5f);
        }
    }

    public void ShakeCamera(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        DOTween.To(() => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain, x => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x, 0f, timer);
    }
}
