using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public Toggle snapTurnToggle;
    public Toggle teleportToggle;
    public GameObject rightHandUIRay;
    public GameObject rightHandTeleportRay;

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

        LoadSettings();
        snapTurnToggle.isOn = snapTurnProvider.enabled;
        teleportToggle.isOn = teleportationProvider.enabled;
    }

    public void ToggleSnapTurn(bool toggleState)
    {
        snapTurnProvider.enabled = toggleState;
        continuousTurnProvider.enabled = !toggleState;
    }

    public void ToggleTeleport(bool toggleState)
    {
        teleportationProvider.enabled = toggleState;
        rightHandTeleportRay.SetActive(toggleState);
        continousMoveProvider.enabled = !toggleState;
        rightHandUIRay.SetActive(!toggleState);
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


    [System.Serializable]
    class SettingsData
    {
        public bool UseSnapTurn;
        public bool UseTeleport;
    }

    public void SaveSettings()
    {
        SettingsData settings = new SettingsData();
        settings.UseSnapTurn = snapTurnToggle.isOn;
        settings.UseTeleport = teleportToggle.isOn;

        string json = JsonUtility.ToJson(settings);

        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsData settings = JsonUtility.FromJson<SettingsData>(json);

            ToggleSnapTurn(settings.UseSnapTurn);
            ToggleTeleport(settings.UseTeleport);
        }
    }
}


