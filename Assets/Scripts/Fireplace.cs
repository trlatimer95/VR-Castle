using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fireplace : MonoBehaviour
{
    [SerializeField]
    private GameObject fireGameObject;
    [SerializeField]
    private ParticleSystem fireParticles;
    [SerializeField]
    private ParticleSystem emberParticles;
    [SerializeField]
    private AudioSource fireSound;
    [SerializeField]
    private AudioClip logPlacedSound;
    [SerializeField]
    private MeshRenderer[] logRenderers;
    [SerializeField]
    private Material canPlaceMaterial;

    private int currLogIndex = 0;
    private Material logMaterial;
    private bool isLogHovering = false;

    void Awake()
    {
        if (fireSound == null)
            gameObject.GetComponentInChildren<AudioSource>();

        logMaterial = logRenderers[0].material;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (currLogIndex >= logRenderers.Length || !obj.CompareTag("Log"))
            return;

        // display log with green material
        logRenderers[currLogIndex].enabled = true;
        logRenderers[currLogIndex].material = canPlaceMaterial;

        isLogHovering = true;
        StartCoroutine("CheckLogDropped", other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (currLogIndex < logRenderers.Length)
        {
            isLogHovering = false;
            logRenderers[currLogIndex].enabled = false;
        }    
    }

    private IEnumerator CheckLogDropped(GameObject log)
    {
        bool isLogHeld = log.GetComponentInChildren<XRGrabInteractable>().isSelected;
        if (isLogHovering && isLogHeld)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else if (isLogHovering && !isLogHeld)
        {
            logRenderers[currLogIndex].material = logMaterial;
            fireSound.PlayOneShot(logPlacedSound);
            isLogHovering = false;
            Destroy(log);

            if (currLogIndex == logRenderers.Length - 1)
                StartFire();

            currLogIndex++;
        }      
    }

    private void StartFire()
    {
        fireGameObject.SetActive(true);
        fireParticles.Play();
        emberParticles.Play();
        fireSound.Play();
    }
}
