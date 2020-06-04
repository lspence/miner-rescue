using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    List<string> displayResolutions = new List<string>() { "Select display resolution", "1366x768", "1440x900", "1600x900", "1680x1050", "1920x1080" };

    int currentResolutionWidth;
    int currentResolutionHeight;
    static int currentSelectedResolution;

    public Dropdown resolutionDropDown;
    public Toggle musicToggle;

    public void Dropdown_IndexChanged(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(currentResolutionWidth, currentResolutionHeight, FullScreenMode.MaximizedWindow);
                break;
            case 1:
                Screen.SetResolution(1366, 768, FullScreenMode.MaximizedWindow);
                currentSelectedResolution = index;
                break;
            case 2:
                Screen.SetResolution(1440, 900, FullScreenMode.MaximizedWindow);
                currentSelectedResolution = index;
                break;
            case 3:
                Screen.SetResolution(1600, 900, FullScreenMode.MaximizedWindow);
                currentSelectedResolution = index;
                break;
            case 4:
                Screen.SetResolution(1680, 1050, FullScreenMode.MaximizedWindow);
                currentSelectedResolution = index;
                break;
            case 5:
                Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
                currentSelectedResolution = index;
                break;
            default:
                Screen.SetResolution(currentResolutionWidth, currentResolutionHeight, FullScreenMode.MaximizedWindow);
                break;
        }
    }

    public void Toggle_IndexChanged(bool check)
    {
        if (check)
        {
            if (!MusicController.instance.GetComponent<AudioSource>().isPlaying)
            {
                MusicController.instance.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            MusicController.instance.GetComponent<AudioSource>().Stop();
        }
    }

    
    private void Start()
    {
        currentResolutionWidth = Screen.currentResolution.width;
        currentResolutionHeight = Screen.currentResolution.height;

        CheckIsMusicPlaying();
        PopulateDropDown();

        string currentResolution = currentResolutionWidth + "x" + currentResolutionHeight;
        
        if (displayResolutions.Contains(currentResolution))
        {
            resolutionDropDown.value = currentSelectedResolution;
        }
    }

   
    private void PopulateDropDown()
    {
        resolutionDropDown.AddOptions(displayResolutions);
    }


    private void CheckIsMusicPlaying()
    {
        if (MusicController.instance.GetComponent<AudioSource>().isPlaying)
        {
            musicToggle.isOn = true;
        }
    }
}
