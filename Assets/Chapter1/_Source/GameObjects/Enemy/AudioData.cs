using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioData : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _acAttack;
    [SerializeField] private AudioClip _acHurt;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void AudioAttack()
    {
        _audioSource.clip = _acAttack;
        _audioSource.Play();
    }
    public void AudioHurt()
    {
        _audioSource.clip = _acHurt;
        _audioSource.Play();
    }
    public void AudioStop()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
    }

}
