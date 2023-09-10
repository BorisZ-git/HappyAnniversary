using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void Awake()
    {
        LoadStartMenu();
    }
    private void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
