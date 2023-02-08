using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class Guillotine : XRGrabInteractable
{
    [Header("Components")]
    [SerializeField] private GameObject guillotineBlade;
    [SerializeField] private Transform bladeEndPos;
    [SerializeField] private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private float angleToTrigger = -60.0f;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float returnSpeed;

    [Header("Sounds")]
    [SerializeField] private AudioClip fallSound;

    private bool dropBlade = false;
    private bool raiseBlade = false;
    private bool trackHandleRotation = false;
    private bool bladeActive = false;
    private Vector3 bladeStartPos;

    void Start()
    {
        bladeStartPos = guillotineBlade.transform.position;
        if (audioSource == null)
            audioSource = gameObject.GetComponentInParent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (bladeActive)
            CheckRaiseDropBlade();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        trackHandleRotation = true;
        StartCoroutine(CheckHandleRotation());
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        trackHandleRotation = false;
    }

    IEnumerator CheckHandleRotation()
    {
        while (trackHandleRotation)
        {
            if (transform.eulerAngles.x >= angleToTrigger)
            {
                trackHandleRotation = false;
                bladeActive = true;
                dropBlade = true;
                audioSource.PlayOneShot(fallSound);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }              
        }        
    }

    private void CheckRaiseDropBlade()
    {
        if (dropBlade)
        {
            if (guillotineBlade.transform.position.y > bladeEndPos.position.y)
            {
                guillotineBlade.transform.position = Vector3.MoveTowards(guillotineBlade.transform.position, bladeEndPos.position, fallSpeed * Time.deltaTime);
            }
            else
            {
                dropBlade = false;
                raiseBlade = true;
            }
        }
        else if (raiseBlade)
        {
            if (guillotineBlade.transform.position.y < bladeStartPos.y)
            {
                guillotineBlade.transform.position = Vector3.MoveTowards(guillotineBlade.transform.position, bladeStartPos, returnSpeed * Time.deltaTime);
            }
            else
            {
                raiseBlade = false;
                bladeActive = false;
                if (isSelected)
                {
                    trackHandleRotation = true;
                    StartCoroutine(CheckHandleRotation());
                }
            }
        }
    }
}
