using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FinalDoorScript : MonoBehaviour
{
    public GameObject[] keys;
    [SerializeField] VladMovement playerScript;
    [SerializeField] GameObject needMoreKeys;
    [SerializeField] GameObject youDidIt;
    [SerializeField] GameObject doorOpen;
    private bool success = false;
    [SerializeField] SoundEffectManagerScript soundEffects;
    [SerializeField] AudioSource backgroundMusic;
    public AudioClip endSong;
    private Animator anim;
    public Material material;
    float materialOpacity = 1f;
    BoxCollider boxCollider;
    public MeshCollider doorMeshCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        keys = GameObject.FindGameObjectsWithTag("Key"); //locats all keys and puts them in array
        playerScript = FindObjectOfType<VladMovement>(); //ref to player script
        soundEffects = FindObjectOfType<SoundEffectManagerScript>(); //ref to sound manager 
        anim = GetComponent<Animator>();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") //If player touches the door
        {
            if (playerScript.keyCounter >= 5)
            {
                boxCollider.enabled = false;
                doorMeshCollider.enabled = false;
                soundEffects.FinalDoorBreak(); //from sound effect manager
                backgroundMusic.clip = endSong;
                backgroundMusic.Play();
                anim.SetTrigger("doorOpen");
                StartCoroutine(DoorOpen(3)); //sets unlocked door UI to active for 3 secs
                Destroy(gameObject, 5f); //makes the door disappear
                StartCoroutine(DoorFade(2.8f));
            }
            if (playerScript.keyCounter < 5)
            {
                soundEffects.FinalDoorCantOpen(); //from sound effect manager 
                StartCoroutine(NotEnoughKeys(3)); //sets not enough keys UI to active for 3 secs
            }
        }

    }
    IEnumerator NotEnoughKeys(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            needMoreKeys.SetActive(true); //displays UI 

            counter--;
        }
        needMoreKeys.SetActive(false); //turns it back off 
    }

    IEnumerator DoorOpen(float seconds)
    {
        float counter = seconds;

        while (counter > 0)

        {
            yield return new WaitForSeconds(1);
            doorOpen.SetActive(true); //displays UI 


            counter--;
        }




        doorOpen.SetActive(false); //turns it back off 
    }

    IEnumerator DoorFade(float delay)
    {
        yield return new WaitForSeconds(delay);
        DOTween.To(() => materialOpacity, x => materialOpacity = x, 0f, 2.2f).SetEase(Ease.Linear);
    }

    private void Update()
    {
        Debug.Log(materialOpacity);
        material.SetFloat("Vector1_b33ae070a08446a09975bc7e8bbc640c", materialOpacity);
    }
    /*public void CheckForSuccess() //I'll fix this later
    {
        if (playerScript.keyCounter == 5) success = true;
        StartCoroutine(All5Keys(3));
        success = false;
    }

    IEnumerator All5Keys(float seconds)
    {
        float counter = seconds;
        while (counter > 0 && success)
        {
            yield return new WaitForSeconds(1);
            youDidIt.SetActive(true); //displays UI 
            counter--;
        }
        success = false;
        youDidIt.SetActive(false); //turns it back off 
        
    }*/

}