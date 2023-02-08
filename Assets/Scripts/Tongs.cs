using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tongs : XRSocketInteractor
{
    [SerializeField] private XRGrabInteractable tongsGrabbable;

    protected override void Awake()
    {
        base.Awake();
        tongsGrabbable = gameObject.GetComponent<XRGrabInteractable>();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && tongsGrabbable.isSelected;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return QuickSelect(interactable) && tongsGrabbable.isSelected;
    }

    private bool QuickSelect(IXRSelectInteractable interactable)
    {
        return !hasSelection || IsSelecting(interactable);
    }
}
