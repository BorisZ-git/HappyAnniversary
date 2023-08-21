using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameUI
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameMenu _gameMenu;
        [SerializeField] private AudioManager _audioMng;
        private UIController _uiController;
        private bool _isGameMenuOpen;

        private void Awake()
        {
            if(GameManager.LevelsManager.LevelsManager.uIController == null)
            {
                _uiController = new UIController(this);
                GameManager.LevelsManager.LevelsManager.uIController = _uiController;
            }
            else
            {
                _uiController = GameManager.LevelsManager.LevelsManager.uIController;
                _uiController.SetInput(this);
            }
            _gameMenu = FindObjectOfType<GameMenu>();
            _gameMenu.SetAudioManager(_audioMng);
            _gameMenu.gameObject.SetActive(_isGameMenuOpen);
        }
        public void EscPress()
        {
            _isGameMenuOpen = _gameMenu.SetGameMenu(_isGameMenuOpen);            
        }
    }
}

