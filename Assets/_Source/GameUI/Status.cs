using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    abstract public class Status : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private int _hp;
        [SerializeField] private int _mp;
        [SerializeField] private GameObject _hpBar;
        [SerializeField] private GameObject _mpBar;
        [SerializeField] private bool _haveMpBar;
        List<Transform> _hpLines;
        List<Transform> _mpLines;

        public int Hp { get => _hp; set => _hp = value; }
        public int Mp { get => _mp; set => _mp = value; }
        public GameObject HpBar { get => _hpBar; set => _hpBar = value; }
        public GameObject MpBar { get => _mpBar; set => _mpBar = value; }


        virtual protected void Start()
        {
            _hpLines = GetBarLines(HpBar);
            UpdateViewStatus(_hpLines, Hp);
            if (_haveMpBar && MpBar != null)
            {
                _mpLines = GetBarLines(MpBar);
                UpdateViewStatus(_mpLines, Mp);
            }
        }
        virtual public void SetHP(int value)
        {
            Hp += value;
            if (value < 0)
            {
                Hurt();
            }
            UpdateViewStatus(_hpLines, Hp);
        }

        virtual public void SetMP(int value)
        {
            Mp += value;
            UpdateViewStatus(_mpLines, Mp);
        }
        abstract protected void LooseHP();
        abstract protected void Hurt();
        virtual public void UpdateViewStatus(List<Transform> lines, int barValue)
        {
            foreach (var item in lines)
            {
                item.gameObject.SetActive(false);
            }
            for (int i = 0; i < barValue; i++)
            {
                lines[i].gameObject.SetActive(true);
            }
        }
        public List<Transform> GetBarLines(GameObject bar)
        {
            List<Transform> temp = new List<Transform>();
            for (int i = 0; i < bar.transform.childCount; i++)
            {
                temp.Add(bar.transform.GetChild(i));
            }
            return temp;            
        }
    }
}

