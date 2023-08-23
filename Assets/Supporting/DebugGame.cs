using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This namespace for create some classes that can help with testing project
/// </summary>
namespace DebugCheatCods
{
    /// <summary>
    /// Put on object for prebuild testing project
    /// Use: L - nextScene ; K - backScene
    /// </summary>
    [DisallowMultipleComponent]
    public class DebugGame : MonoBehaviour
    {
        private int _sceneIndex;
        private void Start()
        {
            if(Singltone<DebugGame>.singletone != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Singltone<DebugGame>.SetSingltone(this);
                DontDestroyOnLoad(this);
            }
        }
        private void Update()
        {
            if (Debug.isDebugBuild)
                TakeInput();
        }
        private void LoadLvl(int value)
        {
            _sceneIndex = SceneManager.GetActiveScene().buildIndex + value;
            if (_sceneIndex > SceneManager.sceneCountInBuildSettings)
            {
                _sceneIndex--;
            }
            else if (_sceneIndex < 0)
            {
                _sceneIndex++;
            }
            SceneManager.LoadScene(_sceneIndex);
        }
        private void TakeInput()
        {
            if (Input.GetKey(KeyCode.L))
            {
                LoadLvl(1);
            }
            else if (Input.GetKey(KeyCode.K))
            {
                LoadLvl(-1);
            }
        }
    }
}

