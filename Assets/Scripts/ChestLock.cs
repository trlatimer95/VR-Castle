using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestLock : XRGrabInteractable
{
    public float minAngleToUnlock;
    public GameObject lidObject;

    private bool trackLockRotation;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        trackLockRotation = true;
        StartCoroutine(CheckLockRotation());
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        trackLockRotation = false;
        CheckLock();
    }

    IEnumerator CheckLockRotation()
    {
        while (trackLockRotation)
        {
            if (transform.eulerAngles.y <= minAngleToUnlock)
            {
                trackLockRotation = false;
                lidObject.AddComponent<Rigidbody>();
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void CheckLock()
    {
        if (transform.eulerAngles.y >= minAngleToUnlock)
        {
            Destroy(lidObject.GetComponent<Rigidbody>());
        }
    }
}
