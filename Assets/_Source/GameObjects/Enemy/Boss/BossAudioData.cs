using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BossAudioData : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _acAttack;
    [SerializeField] private AudioClip _acFillEnergy;
    [SerializeField] private AudioClip _acReleaseEnergy;
    [SerializeField] private AudioClip _acHurt;
    [SerializeField] private AudioClip _acStun;
    [SerializeField] private AudioClip _acDead;
    private AudioSource _asBoss;
    private void Awake()
    {
        _asBoss = GetComponent<AudioSource>();
    }
    public void AudioAttack()
    {
        _asBoss.clip = _acAttack;
        _asBoss.Play();
    }
    public void AudioFill()
    {
        _asBoss.clip = _acFillEnergy;
        _asBoss.Play();
    }
    public void AudioReleaseEnergy()
    {
        _asBoss.clip = _acReleaseEnergy;
        _asBoss.Play();
    }
    public void AudioHurt()
    {
        _asBoss.clip = _acHurt;
        _asBoss.Play();
    }
    public void AudioStun()
    {
        _asBoss.clip = _acStun;
        _asBoss.Play();
    }
    public void AudioDead()
    {
        _asBoss.clip = _acDead;
        _asBoss.Play();
    }
    public void AudioStop()
    {
        _asBoss.clip = null;
        _asBoss.Stop();
    }
}
