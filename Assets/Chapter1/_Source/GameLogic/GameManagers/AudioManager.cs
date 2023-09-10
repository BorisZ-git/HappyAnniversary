using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Mixers")]
    [SerializeField] private AudioMixer _mainMixer;
    [Header("Mixer Hash")]
    [SerializeField] private string _volumeHash;
    [SerializeField] private string _musicHash;
    [SerializeField] private string _sfxHash;
    [Header("Snapshots")]
    [SerializeField] private AudioMixerSnapshot _fadeSounds;
    [SerializeField] private AudioMixerSnapshot _unFadeSounds;
    [SerializeField] private AudioMixerSnapshot _muteSounds;
    private bool _isMute;
    private void Start()
    {
        SetMute(AudioPreset.mute);
        SetVolumeLvl(AudioPreset.volume);
        SetSfxLvl(AudioPreset.sfxVolume);
    }
    private void Update()
    {
        if (_isMute)
        {
            _muteSounds.TransitionTo(01f);
        }
    }
    public void AppearMusic(float timeToUnFade)
    {
        if (!_isMute)
        {
            _unFadeSounds.TransitionTo(timeToUnFade);
        }
    }
    public void FadeMusic(float timeToFade)
    {
        if (!_isMute)
            _fadeSounds.TransitionTo(timeToFade);
    }
    public void SetMute(bool value)
    {
        _isMute = value;
    }
    public void SetVolumeLvl(float value)
    {
        _mainMixer.SetFloat(_volumeHash, value);
    }
    public void SetMusicVolLvl(float value)
    {
        _mainMixer.SetFloat(_musicHash, value);
    }
    public void SetSfxLvl(float value)
    {
        _mainMixer.SetFloat(_sfxHash, value);
    }
}
