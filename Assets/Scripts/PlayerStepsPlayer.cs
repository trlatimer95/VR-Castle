using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerStepsPlayer : MonoBehaviour
{
    [SerializeField]
    private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField]
    private AudioSource footstepSoundSource;


    void Start()
    {
        if (moveProvider == null)
            moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
    }

    private void Update()
    {
        if (moveProvider.enabled)
        {
            //if (moveProvider. moveProvider.locomotionPhase == LocomotionPhase.Moving && !footstepSoundSource.isPlaying)
            //{
            //    footstepSoundSource.Play();
            //}
            //else if ((moveProvider.locomotionPhase == LocomotionPhase.Done || moveProvider.locomotionPhase == LocomotionPhase.Idle) && footstepSoundSource.isPlaying)
            //{
            //    footstepSoundSource.Stop();
            //}         
        }
    }
}
