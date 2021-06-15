using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnemyHealth : MonoBehaviour
{
    public float Health = 100f;

    public float invlunTime;
    public float lastHit;
    private bool hasPlayed = false;
    public bool isDead;
    Animator animator;
    Rigidbody rigidbody;
    BoxCollider collider;
    NavMeshAgent navMesh;
    GuardScript guardScript;
    CinemachineVirtualCamera mainCamera;
    public GameObject sliderObject;
    Slider slider;
    public bool finalBoss;
    SoundEffectManagerScript soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        lastHit = 0f;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        navMesh = GetComponent<NavMeshAgent>();
        guardScript = GetComponent<GuardScript>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCameraCM").GetComponent<CinemachineVirtualCamera>();
        slider = sliderObject.GetComponent<Slider>();
        soundEffects = FindObjectOfType<SoundEffectManagerScript>();

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
                //soundEffects.EnemyHit();
                Health -= damage;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            soundEffects.PlaySound(6, 0.8f);

            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            Health -= otherRb.velocity.magnitude / 3f;
            slider.value = Health;

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

    void Die()
    {
        rigidbody.isKinematic = false;
        animator.enabled = false;
        collider.enabled = false;
        navMesh.enabled = false;
        guardScript.enabled = false;
        sliderObject.SetActive(false);
        Destroy(gameObject, 5);

        if (!hasPlayed)
        {
            if (finalBoss)
            {
                soundEffects.PlaySound(7, 1f);
                StartCoroutine("SceneMigration");
            }

            else
            {
                soundEffects.EnemyDies();
            }

            hasPlayed = true;
        }



    }

    IEnumerator SceneMigration()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
