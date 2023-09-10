using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

namespace StartMenu
{
    [RequireComponent(typeof(AudioSource))]
    public class StartMenuButtons : MonoBehaviour
    {
        [Header("Save Path")]
        [SerializeField] private string _path;
        [Header("Links")]
        [SerializeField] private GameObject _menuBtnPanel;
        [SerializeField] private Button _btnSelectLvl;
        private AudioSource _as;
        private SaveProgress _saveProgress;
        public SaveProgress SaveProgress { get => _saveProgress; }
        private void Awake()
        {
            _as = GetComponent<AudioSource>();
            ReadLvlProgress();
            CheckLvlProgress();
        }
        private void CheckLvlProgress()
        {
            _btnSelectLvl.interactable = SaveProgress?.saveProgress.Count > 0 ? true : false;
        }
        private void ReadLvlProgress()
        {
            if (string.IsNullOrEmpty(_path))
                return;
            if (File.Exists(_path))
            {
                _saveProgress = LevelProgress.WriteProgress();
            }
        }
        public void StartPlatformerGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void OpenClosePanel(GameObject panel)
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
                _menuBtnPanel.SetActive(true);
                _as.Play();
            }
            else
            {
                panel.SetActive(true);
                _menuBtnPanel.SetActive(false);
                _as.Stop();
                if (panel.GetComponent<SelectLevelUI>())
                {
                    panel.GetComponent<SelectLevelUI>().SetDropDownComp();
                }
            }
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

