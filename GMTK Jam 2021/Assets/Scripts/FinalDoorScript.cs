using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorScript : MonoBehaviour
{
    public GameObject[] keys;
    [SerializeField] VladMovement playerScript;
    [SerializeField] GameObject needMoreKeys;

    void Start()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        playerScript = FindObjectOfType<VladMovement>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (playerScript.keyCounter == 5)
            {
                Destroy(gameObject);
            }
            if (playerScript.keyCounter < 5)
            {
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

    // Update is called once per frame
    void Update()
    {

    }
}