using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;
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
    CinemachineVirtualCamera mainCamera;
    public GameObject sliderObject;
    Slider slider;
    public AudioSource ouch;
    public AudioSource dying;

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

        dying = FindObjectOfType<AudioSource>(CompareTag("GuardSound"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0f)
        {
            dying.Play();
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
                //ouch.Play();
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
            slider.value = Health;

            Vector3 direction = transform.position - other.transform.position;
            otherRb.AddForce(-direction * 30f, ForceMode.VelocityChange);

            //Vector3 currentCameraPosition = mainCamera.localPosition;
            //Sequence shakeSequence = DOTween.Sequence();
            //shakeSequence.Append(mainCamera.DOShakePosition(otherRb.velocity.magnitude / 10, otherRb.velocity.magnitude / 5, 5, 10f, false, true));
            //shakeSequence.Append(mainCamera.DOLocalMove(currentCameraPosition, 0.5f, false));
            //shakeSequence.Play();

            ShakeCamera(otherRb.velocity.magnitude / 3, 0.5f);
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
    }
}
