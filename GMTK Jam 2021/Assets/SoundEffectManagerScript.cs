using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManagerScript : MonoBehaviour
{
    AudioSource soundPlayer;
    public AudioClip[] soundEffects;
    public GameObject quitObject;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void EnemyHit()
    {
        soundPlayer.PlayOneShot(soundEffects[0]);
    }
    public void EnemyDies()
    {
        soundPlayer.PlayOneShot(soundEffects[1]);
    }
    public void Key()
    {
        soundPlayer.PlayOneShot(soundEffects[2]);
    }
    public void PlayerDies()
    {
        soundPlayer.PlayOneShot(soundEffects[3]);
    }
    public void FinalDoorCantOpen()
    {
        soundPlayer.PlayOneShot(soundEffects[4]);
    }
    public void FinalDoorBreak()
    {
        soundPlayer.PlayOneShot(soundEffects[5]);
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            quitObject.SetActive(true); //displays quit confirmation 
        }



    }
}
