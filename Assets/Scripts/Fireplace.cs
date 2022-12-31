using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fireplace : XRSocketInteractor
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

    private int currLogIndex = 0;
    private Material logMaterial;
    private bool[] addedLogs;

    protected override void Awake()
    {
        base.Awake();

        if (fireSound == null)
            gameObject.GetComponentInChildren<AudioSource>();

        logMaterial = logRenderers[0].material;
        addedLogs = new bool[logRenderers.Length];
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (currLogIndex >= logRenderers.Length)
            return;

        if (args.interactorObject.transform.gameObject.CompareTag("Log") && logRenderers.Any(f => f.enabled == false))
        {
            logRenderers[currLogIndex].enabled = true;
            logRenderers[currLogIndex].material = interactableHoverMeshMaterial;
            Debug.Log("Log entered");
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (currLogIndex >= logRenderers.Length)
            return;


        if (args.interactorObject.transform.gameObject.CompareTag("Log") && !addedLogs[currLogIndex])
        {
            logRenderers[currLogIndex].enabled = false;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (currLogIndex >= logRenderers.Length)
            return;

        if (args.interactorObject.transform.gameObject.CompareTag("Log") && !addedLogs[currLogIndex])
        {
            logRenderers[currLogIndex].enabled = true;
            logRenderers[currLogIndex].material = logMaterial;
            addedLogs[currLogIndex] = true;
            currLogIndex++;
            Debug.Log("Log added, new index: " + currLogIndex);

            if (currLogIndex == logRenderers.Length - 1)
            {
                StartFire();
            }
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
