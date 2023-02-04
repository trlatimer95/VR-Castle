using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    [SerializeField] private Collider mainCollider;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        mainCollider.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        mainCollider.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
