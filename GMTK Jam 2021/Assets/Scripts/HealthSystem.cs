using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;

    // HP per second
    public float regenSpeed = 1f;

    public bool shouldRegenHP = true;

    public float lastHit;
    public float invlunTime;

    //public Image HPBar;
    public GameObject sliderObject;
    Slider slider;
    CinemachineVirtualCamera mainCamera;

    public AudioSource metalCling;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCameraCM").GetComponent<CinemachineVirtualCamera>();
        slider = sliderObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        TryHealing();
        if (curHealth <= 0f)
        {
            //soundEffects.PlayerDies();
            SceneManager.LoadScene("Master_Scene");
        }
    }

    /*public void tryToDamage(float damage)
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
    }*/


    void TryHealing()
    {
        if (Time.time > lastHit + invlunTime && curHealth < maxHealth)
        {
            // increase health dependant on the time it takes to render a frame
            curHealth += regenSpeed * Time.deltaTime;
            slider.value = curHealth;
            //UpdateUIHealth();

            if (curHealth > maxHealth) // don't overheal
            {
                curHealth = maxHealth;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Guard")
        {

            curHealth -= 10;
            slider.value = curHealth;
            //Debug.Log("hitGuard");
        }
    }
     /*   void UpdateUIHealth()
    {
        HPBar.fillAmount = curHealth / maxHealth;
    }*/


    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit soemthing!");
        if (other.transform.tag == "Guard" && Time.time > lastHit + invlunTime)
        {
            Debug.Log("Player hit guard!");
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            curHealth -= 5;
            //UpdateUIHealth();

            Vector3 direction = transform.position - other.transform.position;
            otherRb.AddForce(-direction * 30f, ForceMode.VelocityChange);

            ShakeCamera(otherRb.velocity.magnitude / 2, 0.5f);
            lastHit = Time.time;

            metalCling.pitch = Random.Range(0.95f, 1.05f);
            metalCling.Play();
        }
    }*/

    public void ShakeCamera(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        DOTween.To(() => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain, x => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x, 0f, timer);
    }
}
