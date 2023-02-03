using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [SerializeField] private float speed = 2000.0f;

    private Rigidbody rigidBody;
    private ArrowCaster caster;

    private bool launched = false;

    private RaycastHit hit;
    
    protected override void Awake()
    {
        base.Awake();
        rigidBody= GetComponent<Rigidbody>();
        caster = GetComponent<ArrowCaster>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactorObject is Notch notch)
        {
            if (notch.CanRelease)
                LaunchArrow(notch);
        }
    }

    private void LaunchArrow(Notch notch)
    {
        launched = true;
        ApplyForce(notch.Puller);
        StartCoroutine(LaunchRoutine());
    }

    private void ApplyForce(Puller puller)
    {
        rigidBody.AddForce(transform.forward * (puller.PullAmount * speed));
    }

    private IEnumerator LaunchRoutine()
    {
        // Set direction while flying
        while (!caster.CheckForCollision(out hit))
        {
            SetDirection();
            yield return null;
        }

        // Once arrow has stopped
        DisablePhysics();
        ChildArrow(hit);
        CheckForHittable(hit);
    }

    private void SetDirection()
    {
        if (rigidBody.velocity.z > 0.5f)
            transform.forward = rigidBody.velocity;
    }

    private void DisablePhysics()
    {
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
    }

    private void ChildArrow(RaycastHit hit)
    {
        transform.SetParent(hit.transform);
    }

    private void CheckForHittable(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IArrowHittable hittable))
            hittable.Hit(this);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !launched;
    }
}
