using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Guillotine : XRGrabInteractable
{
    public GameObject guillotineBlade;
    public Vector3 startPos;
    public Vector3 endPos;
    public float angleToTrigger;
    public float fallSpeed;
    public float returnSpeed;

    private bool isFalling = false;
    private bool isReturning = false;
    private bool trackHandleRotation = false;
    

    void Start()
    {
        startPos = guillotineBlade.transform.position;
    }

    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        trackHandleRotation = true;
    }

    IEnumerator CheckHandleRotation()
    {
        yield return new WaitForSeconds(1);
    }
}
