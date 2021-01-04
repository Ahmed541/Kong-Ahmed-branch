using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem_es;

    public GameObject mainMenu_go;
    public GameObject mainMenuFirstSelected_go;

    public GameObject setting_go;
    public GameObject settingsFirstSelected_go;

    public Slider music_slid;
    public Slider sfx_slid;

    //  TODO: Change this to the actual game scene name in build or add the index.
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        mainMenu_go.SetActive(false);
        setting_go.SetActive(true);
        eventSystem_es.SetSelectedGameObject(settingsFirstSelected_go);

        music_slid.value = PlayerPrefs.GetFloat("MusicVolume");
        sfx_slid.value = PlayerPrefs.GetFloat("SfxVolume");
    }

    public void CloseSettings()
    {
        setting_go.SetActive(false);
        mainMenu_go.SetActive(true);
        eventSystem_es.SetSelectedGameObject(mainMenuFirstSelected_go);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", music_slid.value);
        PlayerPrefs.SetFloat("SfxVolume", sfx_slid.value);
    }
}
