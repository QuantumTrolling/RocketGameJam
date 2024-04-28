using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class settings : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;


    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutinIndex = 0;

        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].width + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutinIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutinIndex);
    
    }

    public void SetFullscreen(bool isFullScreen){
            Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ApplySettings(){
        PlayerPrefs.SetInt("ResoluitionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));

    }

    public void LoadSettings(int currentResolutinIndex){
        if (PlayerPrefs.HasKey("ResoluitionPreference")){
            resolutionDropdown.value = PlayerPrefs.GetInt("ResoluitionPreference");
        }
        else{
            resolutionDropdown.value = currentResolutinIndex;
        }
        if (PlayerPrefs.HasKey("FullscreenPreference")){
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        }
        else{
            Screen.fullScreen = true;
        }
    }
}
