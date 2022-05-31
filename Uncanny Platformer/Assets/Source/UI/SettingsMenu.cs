using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Source.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        Resolution[] resolutions;
        public Dropdown resolutionDropdown;
        
        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            var options = new List<string>();
            var currentIndex = 0;
            for (var i = 0; i < resolutions.Length; i++)
            {
                var res = resolutions[i];
                var option = res.width + " x " + res.height;
                options.Add(option);
                if (res.height == Screen.currentResolution.width &&
                    res.width == Screen.currentResolution.width)
                {
                    currentIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetVolume(float volume)
        {
            var value = Mathf.Pow(volume, 0.25f) * 100 - 80;
            audioMixer.SetFloat("volume", value);
        }
        
        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }

        public void SetResolution(int resIndex)
        {
            var resolution = resolutions[resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
