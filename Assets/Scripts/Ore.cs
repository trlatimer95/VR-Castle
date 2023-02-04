using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private GameObject outputItem;
    [SerializeField] private float smeltTime = 5.0f;
    [SerializeField] private AudioSource smeltAudio;
    [SerializeField] private AudioClip smeltFinishSound;

    private bool isSmelting = false;
    private DateTime completeTime;

    private void Start()
    {
        if (smeltAudio == null)
            smeltAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Forge"))
        {
            isSmelting = true;
            StartCoroutine("SmeltOre", smeltTime);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Forge"))
            isSmelting = false;
    }

    private IEnumerator SmeltOre(float smeltTime)
    {
        smeltAudio.Play();
        isSmelting = true;
        completeTime = DateTime.Now.AddSeconds(smeltTime);

        while (isSmelting && DateTime.Compare(DateTime.Now, completeTime) < 0)
        {
            yield return new WaitForSeconds(1);
        }

        if (!isSmelting) // Player removed ore before smelted
        {
            smeltAudio.Stop();
            yield return null;
        }
        else
        {
            // Spawn new ingot and destroy ore
            Instantiate(outputItem, transform.position, Quaternion.identity);
            smeltAudio.Stop();
            smeltAudio.PlayOneShot(smeltFinishSound);
            Destroy(gameObject);
        }      
    }
}
