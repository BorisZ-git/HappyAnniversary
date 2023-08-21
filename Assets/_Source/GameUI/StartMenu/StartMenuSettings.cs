using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace StartMenu
{
    public class StartMenuSettings : MonoBehaviour
    {
        [Header("Audio Hash")]
        [SerializeField] private string _volumeHash;
        [SerializeField] private string _musicHash;
        [SerializeField] private string _sfxHash;
        [Header("Links")]
        [SerializeField] private Slider _mainVolSlider;
        [SerializeField] private Slider _musicVolSlider;
        [SerializeField] private Slider _sfxVolSlider;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private AudioMixer _mainMixer;
        [SerializeField] private StartMenuButtons _startMenu;
        private void Start()
        {
            _mainVolSlider.value = AudioPreset.volume;
            _musicVolSlider.value = AudioPreset.musicVolume;
            _sfxVolSlider.value = AudioPreset.sfxVolume;
            _muteToggle.isOn = AudioPreset.mute;
        }
        public void SetMute(Toggle toggle)
        {
            AudioPreset.mute = toggle.isOn;
        }
        public void SetVolumeLvl(Slider slider)
        {
            AudioPreset.volume = slider.value;
            _mainMixer.SetFloat(_volumeHash, slider.value);
        }
        public void SetMusicVolLvl(Slider slider)
        {
            AudioPreset.musicVolume = slider.value;
            _mainMixer.SetFloat(_musicHash, slider.value);
        }
        public void SetSfxLvl(Slider slider)
        {
            AudioPreset.sfxVolume = slider.value;
            _mainMixer.SetFloat(_sfxHash, slider.value);
        }
        public void PushReturnBtn()
        {
            _startMenu.OpenClosePanel(this.gameObject);
        }
    }
}

