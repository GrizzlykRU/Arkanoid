using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;

    public Dropdown quality;

    public GameObject mainMenuUI;

    public GameObject settingsUI;

    public void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0;i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "x" + resolutions[i].height);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        quality.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetGraphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void BackToMainMenu()
    {
        mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
    }


}
