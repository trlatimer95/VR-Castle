using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketPlaySound : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private AudioSource soundSource;

    void Start()
    {
        if (socket == null)
            socket = GetComponent<XRSocketInteractor>();
        if (soundSource == null)
            soundSource = GetComponent<AudioSource>();

        socket.selectEntered.AddListener(PlaySound);
        socket.selectExited.AddListener(n => soundSource.Stop());
    }

    private void PlaySound(SelectEnterEventArgs args)
    {
        if (!soundSource.isPlaying)
            soundSource.Play();
    }

    //private void StopSound(SelectExitEventArgs args)
    //{
    //    soundSource.Stop();
    //}
}
