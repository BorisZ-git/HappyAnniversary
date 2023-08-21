using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioSource : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _acWalk;
    [SerializeField] private AudioClip _acMeleeAttack;
    [SerializeField] private AudioClip _acDistanceAttack;
    [SerializeField] private AudioClip _acHurt;
    [SerializeField] private AudioClip _acLoose;
    [SerializeField] private AudioClip _acJump;
    [SerializeField] private AudioClip _acUse;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void AudioWalk()
    {
        _audioSource.loop = true;
        _audioSource.volume = .65f;
        _audioSource.clip = _acWalk;
        _audioSource.Play();
    }
    public void AudioJump()
    {
        _audioSource.loop = false;
        if(_audioSource.clip != _acJump)
        {
            _audioSource.clip = _acJump;
            _audioSource.Play();
        }
    }
    public void AudioMeleeAttack()
    {
        _audioSource.clip = _acMeleeAttack;
        _audioSource.Play();
    }
    public void AudioDistanceAttack()
    {
        _audioSource.clip = _acDistanceAttack;
        _audioSource.Play();
    }
    public void AudioHurt()
    {
        _audioSource.clip = _acHurt;
        _audioSource.Play();
    }
    public void AudioUse()
    {
        _audioSource.clip = _acUse;
        _audioSource.Play();
    }
    public void AudioLoose()
    {
        if(_audioSource.clip != _acLoose)
        {
            _audioSource.clip = _acLoose;
            _audioSource.Play();
        }
    }
    public void AudioStop()
    {
        _audioSource.volume = 1f;
        _audioSource.clip = null;
        _audioSource.loop = false;
        _audioSource.Stop();
    }
}
