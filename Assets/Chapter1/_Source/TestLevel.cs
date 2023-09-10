using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TestLevel : MonoBehaviour
{
    [SerializeField] public TMP_InputField _inField;
    [SerializeField] public Button _btn;
    private int lvlIndex;
    private void Update()
    {
        if(_inField.text != null && int.TryParse(_inField.text ,out lvlIndex) && lvlIndex <= SceneManager.sceneCountInBuildSettings)
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void LoadTestLevel()
    {
        SceneManager.LoadScene(int.Parse(_inField.text));
    }
}
