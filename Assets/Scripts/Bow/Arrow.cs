using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [SerializeField] private float speed = 2000.0f;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float arrowLifeAfterLaunch = 5.0f;
    [SerializeField] private float forceAmount = 5.0f;

    private new Rigidbody rigidbody;
    private ArrowCaster caster;
    private AudioSource audioSource;

    private bool launched = false;

    private RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        caster = GetComponent<ArrowCaster>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactorObject is Notch notch && notch.CanRelease)
        {
            LaunchArrow(notch);
            Destroy(this, arrowLifeAfterLaunch);             
        }    
    }

    private void LaunchArrow(Notch notch)
    {
        ApplyForce(notch.PullMeasurer);
        StartCoroutine(LaunchRoutine());
    }

    private void ApplyForce(PullMeasurer pullMeasurer)
    {
        rigidbody.AddForce(transform.forward * (pullMeasurer.PullAmount * speed));
    }

    private IEnumerator LaunchRoutine()
    {
        launched = true;

        // Set direction while flying
        while (!caster.CheckForCollision(out hit))
        {
            SetDirection();
            yield return null;
        }

        // Once the arrow has stopped flying
        DisablePhysics();
        ChildArrow(hit);
        ApplyForceToHit(hit);
        PlayHitSound();
    }

    private void SetDirection()
    {
        if (rigidbody.velocity.z < -0.5f)
            transform.forward = rigidbody.velocity;
    }

    private void DisablePhysics()
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void ApplyForceToHit(RaycastHit hit)
    {
        hit.rigidbody?.AddForce(transform.forward * forceAmount);
    }

    private void ChildArrow(RaycastHit hit)
    {
        transform.SetParent(hit.transform);
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !launched;
    }
}
