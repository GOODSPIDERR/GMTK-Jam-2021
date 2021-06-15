using System.Collections;
using UnityEngine;

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

    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key"); //locats all keys and puts them in array
        playerScript = FindObjectOfType<VladMovement>(); //ref to player script
        soundEffects = FindObjectOfType<SoundEffectManagerScript>(); //ref to sound manager 

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") //If player touches the door
        {
            if (playerScript.keyCounter >= 5) 
            {
                soundEffects.FinalDoorBreak(); //from sound effect manager
                backgroundMusic.clip = endSong;
                backgroundMusic.Play();
                StartCoroutine(DoorOpen(3)); //sets unlocked door UI to active for 3 secs
                Destroy(gameObject, 3); //makes the door disappear
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