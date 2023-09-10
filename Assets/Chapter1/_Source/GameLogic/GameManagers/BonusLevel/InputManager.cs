using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BonusLevel.RidePlatform;

public class InputManager : MonoBehaviour
{
    [Header("Controlled Objects Link")]
    [SerializeField] private Player.Player _player;
    [SerializeField] private RidePlatformGeneral _ridePlatform;

    private void Awake()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player.Player>();
        }
        if (_ridePlatform == null)
        {
            _ridePlatform = FindObjectOfType<RidePlatformGeneral>();
        }
    }
    public void SetGameInputOff()
    {
        AnotherOff();
        PlayerOff();
    }
    public void ChangeToPlayer()
    {
        AnotherOff();
        PlayerOn();
    }
    public void ChangeToAnother()
    {
        PlayerOff();
        AnotherOn();
    }
    private void PlayerOn()
    {
        _player.SetActive(true);
    }
    private void PlayerOff()
    {
        _player.SetActive(false);
    }
    private void AnotherOn()
    {
        _ridePlatform.SetActive(true);
    }
    private void AnotherOff()
    {
        _ridePlatform.SetActive(false);
    }
}
