using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitSound;
    [SerializeField] XRGrabInteractable interactable;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<XRGrabInteractable>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactable.isSelected && collision.gameObject.GetComponent<HitSound>() == null)
            audioSource.PlayOneShot(hitSound);
    }  
}
