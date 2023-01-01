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
    private MeshRenderer[] logRenderers;
    [SerializeField]
    private Material canPlaceMaterial;

    private int currLogIndex = 0;
    private Material logMaterial;
    private bool[] addedLogs;
    private bool isLogHovering = false;

    void Awake()
    {
        if (fireSound == null)
            gameObject.GetComponentInChildren<AudioSource>();

        logMaterial = logRenderers[0].material;
        addedLogs = new bool[logRenderers.Length];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (currLogIndex >= logRenderers.Length || !obj.CompareTag("Log"))
            return;

        // display log with green material
        logRenderers[currLogIndex].enabled = true;
        logRenderers[currLogIndex].material = canPlaceMaterial;
        Debug.Log("Log entered");

        isLogHovering = true;
        StartCoroutine("CheckLogDropped", other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        isLogHovering = false;
        logRenderers[currLogIndex].enabled = false;
        Debug.Log("Log left");
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
            isLogHovering = false;
            Debug.Log("Log placed at index: " + currLogIndex);

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
        Debug.Log("Fire started");
    }
}
