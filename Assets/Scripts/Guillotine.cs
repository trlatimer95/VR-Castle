using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class Guillotine : XRGrabInteractable
{
    public GameObject guillotineBlade;
    public Transform bladeEndPos;
    public float angleToTrigger = -60.0f;
    public float fallSpeed;
    public float returnSpeed;

    private bool dropBlade = false;
    private bool raiseBlade = false;
    private bool trackHandleRotation = false;
    private Vector3 bladeStartPos;
    

    void Start()
    {
        bladeStartPos = guillotineBlade.transform.position;
    }

    void Update()
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
            if (guillotineBlade.transform.position.x < bladeStartPos.x)
            {
                guillotineBlade.transform.position = Vector3.MoveTowards(guillotineBlade.transform.position, bladeStartPos, returnSpeed * Time.deltaTime);
            }
            else
            {
                raiseBlade = false;
                if (isSelected)
                {
                    trackHandleRotation = true;
                    StartCoroutine(CheckHandleRotation());
                }
            }
        }
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
                dropBlade = true;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }              
        }        
    }
}
