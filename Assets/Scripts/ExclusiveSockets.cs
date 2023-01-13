using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExclusiveSockets : MonoBehaviour
{
    // Indexes must match corresponding socket that would be enabled/disabled
    public XRSocketInteractor[] item1Sockets;
    public XRSocketInteractor[] item2Sockets;

    private void Start()
    {
        foreach (var socket in item1Sockets)
        {
            socket.selectEntered.AddListener(DisableSocket);
            socket.selectExited.AddListener(EnableSocket);
        }

        foreach (var socket in item2Sockets)
        {
            socket.selectEntered.AddListener(DisableSocket);
            socket.selectExited.AddListener(EnableSocket);
        }
    }

    private void DisableSocket(SelectEnterEventArgs args)
    {
        XRSocketInteractor interactedSocket = (XRSocketInteractor)args.interactorObject;
        if (interactedSocket != null)
        {
            int socketIndex = Array.IndexOf(item1Sockets, interactedSocket);
            if (socketIndex != -1)
            {
                item2Sockets[socketIndex].enabled = false;
            }
            else
            {
                socketIndex = Array.IndexOf(item2Sockets, interactedSocket);
                if (socketIndex != -1)
                {
                    item1Sockets[socketIndex].enabled = false;
                }
            }
        }
    }

    private void EnableSocket(SelectExitEventArgs args)
    {
        XRSocketInteractor interactedSocket = (XRSocketInteractor)args.interactorObject;
        if (interactedSocket != null)
        {
            int socketIndex = Array.IndexOf(item1Sockets, interactedSocket);
            if (socketIndex != -1)
            {
                item2Sockets[socketIndex].enabled = true;
            }
            else
            {
                socketIndex = Array.IndexOf(item2Sockets, interactedSocket);
                if (socketIndex != -1)
                {
                    item1Sockets[socketIndex].enabled = true;
                }
            }
        }
    }
}