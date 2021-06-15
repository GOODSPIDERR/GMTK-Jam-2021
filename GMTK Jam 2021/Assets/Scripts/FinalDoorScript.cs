using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour
{
    public GameObject[] keys;
    [SerializeField] VladMovement playerScript;
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject needMoreKeys;
    [SerializeField] SoundEffectManagerScript soundEffects;

    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        playerScript = FindObjectOfType<VladMovement>();
        soundEffects = FindObjectOfType<SoundEffectManagerScript>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (playerScript.keyCounter >= 5)
            {
                soundEffects.FinalDoorBreak();
                StartCoroutine(DoorOpen(3));
                Destroy(gameObject, 3);
            }
            if (playerScript.keyCounter < 5)
            {
                soundEffects.FinalDoorCantOpen();
                StartCoroutine(NotEnoughKeys(3));
            }
        }

    }


    IEnumerator NotEnoughKeys(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            needMoreKeys.SetActive(true);

            counter--;
        }
        needMoreKeys.SetActive(false);
    }

    IEnumerator DoorOpen(float seconds)
    {
        float counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            doorOpen.SetActive(true);

            counter--;
        }
        doorOpen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}