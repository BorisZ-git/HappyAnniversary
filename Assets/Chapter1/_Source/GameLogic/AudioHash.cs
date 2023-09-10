using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHash : MonoBehaviour
{
    [Header("Level Logic")]
    [SerializeField] private bool _isGameLevel;
    [SerializeField] private bool _isBonusLevel;
    [SerializeField] private bool _isBossLevel;
    [Header("Audio Clip Hash")]
    [SerializeField] private AudioClip[] _gameMusic;
    [SerializeField] private AudioClip[] _bonusMusic;
    [SerializeField] private AudioClip[] _bossMusic;
    [SerializeField] private AudioSource _music;
    private int _currentInd;
    private AudioClip _currentClip;
    private AudioClip[] _lvlHash;
    private float _clipTimer;
    private void Start()
    {
        SetClips();
        SetAudioSourceClip();
    }
    private void Update()
    {
        _clipTimer -= Time.deltaTime;
        if(_clipTimer < 0)
        {
            TakeClip(_lvlHash);
            SetAudioSourceClip();
        }
    }
    public void SetAudioLevel( bool isBonus, bool isBoss)
    {
        _isBonusLevel = isBonus;
        _isBossLevel = isBoss;
        if(!_isBonusLevel && !_isBossLevel)
        {
            _isGameLevel = true;
        }
        else
        {
            _isGameLevel = false;
        }
    }
    private void SetClips()
    {
        if (_isGameLevel)
        {
            _lvlHash = _gameMusic;
        }
        else if (_isBonusLevel)
        {
            _lvlHash = _bonusMusic;
        }
        else if (_isBossLevel)
        {
            _lvlHash = _bossMusic;
        }
        TakeClip(_lvlHash);
    }
    private void TakeClip(AudioClip[] clips)
    {
        int i = Random.Range(0, clips.Length);
        if (i == _currentInd && clips.Length > 1)
        {
            TakeClip(clips);
        }
        else
        {
            _currentInd = i;
            _currentClip = clips[_currentInd];
        }
    }
    private void SetAudioSourceClip()
    {
        _music.clip = _currentClip;
        _clipTimer = _music.clip.length;
        if(_music.gameObject.activeInHierarchy)
            _music.Play();
    }
}
