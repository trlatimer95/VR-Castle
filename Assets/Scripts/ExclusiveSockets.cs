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
        // Add event listeners for enabling/disabling respective sockets, check for starting interactable
        for (int i = 0; i < item1Sockets.Length; i++)
        {
            item1Sockets[i].selectEntered.AddListener(DisableSocket);
            item1Sockets[i].selectExited.AddListener(EnableSocket);
            if (item1Sockets[i].startingSelectedInteractable != null)
                item2Sockets[i].enabled = false;
        }

        for (int i = 0; i < item2Sockets.Length; i++)
        {
            item2Sockets[i].selectEntered.AddListener(DisableSocket);
            item2Sockets[i].selectExited.AddListener(EnableSocket);
            if (item2Sockets[i].startingSelectedInteractable != null)
                item1Sockets[i].enabled = false;
        }
    }

    /// <summary>
    /// Disable socket sharing location with the socket interacted with. Shared sockets determined by matching array indices.
    /// </summary>
    /// <param name="args"></param>
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

    /// <summary>
    /// Enable socket sharing location with the socket interacted with. Shared sockets determined by matching array indices.
    /// </summary>
    /// <param name="args"></param>
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