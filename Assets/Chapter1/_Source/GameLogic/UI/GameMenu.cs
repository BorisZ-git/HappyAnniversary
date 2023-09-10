using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager.LevelsManager;
using System;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenuPanel;
    [SerializeField] private AudioManager _audioMng;
    [SerializeField] private int _startMenuIndLvl = 0;
    [Header("UI Links")]
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _muteToggle;
    private void Start()
    {
        _volumeSlider.value = AudioPreset.volume;
        _musicSlider.value = AudioPreset.musicVolume;
        _sfxSlider.value = AudioPreset.sfxVolume;
        _muteToggle.isOn = AudioPreset.mute;
    }
    public void SetAudioManager(AudioManager audioMng)
    {
        _audioMng = audioMng;
    }
    public void BtnRestartLevel()
    {
        GamePause();
        LevelsManager.RestartLevel();
    }
    public void BtnMainMenu()
    {
        GamePause();
        LevelsManager.LoadLevel(_startMenuIndLvl);
    }
    public void BtnExitGame()
    {
        Application.Quit();
    }
    public void MuteSound(Toggle toggle)
    {
        AudioPreset.mute = toggle.isOn;
        _audioMng.SetMute(toggle.isOn);
    }
    public bool SetGameMenu(bool value)
    {
        if (!value)
        {
            _audioMng.FadeMusic(.01f);
            _gameMenuPanel.SetActive(true);
        }
        else
        {
            _audioMng.AppearMusic(1f);
            _gameMenuPanel.SetActive(false);
        }
        GamePause();
        return !value;
    }
    public void SetVolumeLvl(Slider slider)
    {
        AudioPreset.volume = slider.value;
        _audioMng.SetVolumeLvl(slider.value);
    }
    public void SetMusicVolLvl(Slider slider)
    {
        AudioPreset.musicVolume = slider.value;
        _audioMng.SetMusicVolLvl(slider.value);
    }
    public void SetSfxLvl(Slider slider)
    {
        AudioPreset.sfxVolume = slider.value;
        _audioMng.SetSfxLvl(slider.value);
    }
    public void GamePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1; 
    }
}
