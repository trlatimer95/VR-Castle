using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestLock : XRGrabInteractable
{
    public float minAngleToUnlock;
    public GameObject lidObject;

    private bool trackLockRotation;
    private HingeJoint storedLidHingeJoint;
    private XRGrabInteractable storedLidGrabbable;

    private void Start()
    {
        storedLidGrabbable = lidObject.GetComponent<XRGrabInteractable>();
        storedLidHingeJoint = lidObject.GetComponent<HingeJoint>();

        Destroy(lidObject.GetComponent<XRGrabInteractable>());
        Destroy(lidObject.GetComponent<HingeJoint>());
    }

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
                HingeJoint newHingeJoint = lidObject.AddComponent<HingeJoint>();
                newHingeJoint.anchor = storedLidHingeJoint.anchor;
                newHingeJoint.limits = storedLidHingeJoint.limits;
                newHingeJoint.useLimits = storedLidHingeJoint.useLimits;
                newHingeJoint.axis = storedLidHingeJoint.axis;
                newHingeJoint.connectedAnchor = storedLidHingeJoint.connectedAnchor;
                newHingeJoint.useSpring = storedLidHingeJoint.useSpring;
                newHingeJoint.spring = storedLidHingeJoint.spring;

                XRGrabInteractable newGrabble = lidObject.AddComponent<XRGrabInteractable>();
                newGrabble.interactionManager = storedLidGrabbable.interactionManager;
                newGrabble.interactionLayerMask = storedLidGrabbable.interactionLayerMask;
                newGrabble.interactionLayers = storedLidGrabbable.interactionLayers;
                newGrabble.colliders.AddRange(storedLidGrabbable.colliders);
                newGrabble.movementType = storedLidGrabbable.movementType;
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
            HingeJoint hingeJoint = lidObject.GetComponent<HingeJoint> ();
            XRGrabInteractable grabbable = lidObject.GetComponent<XRGrabInteractable>();

            if (hingeJoint != null)
                Destroy(lidObject.GetComponent<HingeJoint>());
            if (grabbable != null)
                Destroy(lidObject.GetComponent<XRGrabInteractable>());
        }
    }
}
