using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace StartMenu
{
    public class SelectLevelUI : MonoBehaviour
    {
        [Header("Image Level")]
        [SerializeField] private Sprite[] _lvlImages;
        [Header("Links")]
        [SerializeField] private GameObject _panelSelectLvl;
        [SerializeField] private Image _imageLvl;
        [SerializeField] private TMP_Dropdown _lvlDropDown;
        [SerializeField] private Button _btnLoadLvl;
        [SerializeField] private StartMenuButtons _startMenu;
        private List<string> _temp;
        private int _selectedIndex;
        private string _sceneName;
        private string _buildName;
        private SaveLevel _saveLevel;
        public void PushReturnBtn()
        {
            _startMenu.OpenClosePanel(this.gameObject);
            _imageLvl.color = Color.black;
            _btnLoadLvl.interactable = false;
            _saveLevel = null;
        }
        // Set Complete Level from this
        public void SetDropDownComp()
        {
            _temp = new List<string>();
            _lvlDropDown.ClearOptions();

            for (int i = 0; i < _startMenu.SaveProgress.GetCount; i++)
            {
                _temp.Add(_startMenu.SaveProgress.GetName(i));
            }
            _lvlDropDown.AddOptions(_temp);
            _temp.Clear();
            UpdateLoadLvlProperty();
        }
        public void UpdateLoadLvlProperty()
        {
            _saveLevel = _startMenu.SaveProgress.GetContainLevel(_lvlDropDown.captionText.text);
            if(_saveLevel == null)
            {
                return;
            }
            else
            {
                _imageLvl.sprite = _lvlImages[_saveLevel.index];
                //_imageLvl.SetNativeSize();
                _imageLvl.color = Color.white;
                _btnLoadLvl.interactable = true;                
            }
            //if(int.TryParse(temp[1],out int index) && _lvlImages.Length >= index)
            //{
            //    _imageLvl.sprite = _lvlImages[--index];
            //    _imageLvl.SetNativeSize();
            //    _imageLvl.color = Color.white;
            //    _btnLoadLvl.interactable = true;
            //    _selectedIndex = index;
            //}
        }
        public void LoadSelectedLvl()
        {
            if(!string.IsNullOrEmpty(_saveLevel.buildName))
            {
                print(_saveLevel.buildName);
                SceneManager.LoadScene(_saveLevel.buildName);
            }
        }
    }
}

