using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsMenu;

    private ActionBasedSnapTurnProvider snapTurnProvider;
    private ActionBasedContinuousTurnProvider continuousTurnProvider;
    private ActionBasedContinuousMoveProvider continousMoveProvider;
    private TeleportationProvider teleportationProvider;

    private void Start()
    {
        GameObject playerRig = GameObject.FindGameObjectWithTag("Player");
        snapTurnProvider = playerRig.GetComponent<ActionBasedSnapTurnProvider>();
        continuousTurnProvider = playerRig.GetComponent<ActionBasedContinuousTurnProvider>();
        continousMoveProvider = playerRig.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportationProvider = playerRig.GetComponent<TeleportationProvider>();
    }

    public void ToggleSnapTurn(bool toggleState)
    {
        snapTurnProvider.enabled = toggleState;
        continuousTurnProvider.enabled = !toggleState;
    }

    public void ToggleTeleport(bool toggleState)
    {
        teleportationProvider.enabled = toggleState;
        continousMoveProvider.enabled = !toggleState;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if DEBUG
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
