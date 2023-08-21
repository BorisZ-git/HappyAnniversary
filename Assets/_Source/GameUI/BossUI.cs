using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [Header("HP Bar")]
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Image[] _hearts;
    [Header("MP Bar")]
    [SerializeField] private Slider _mpSlider;
    [Header("Sprites")]
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private Sprite _fillHeart;
    public int HeartsCount { get => _hearts.Length; }
    public void SetSlidersValue(int maxHp, int maxMp)
    {
        _hpSlider.maxValue = maxHp;
        _mpSlider.maxValue = maxMp;
        UpdateHPBar(maxHp);
        //UpdateMPBar(maxMp);
    }
    public void UpdateMPBar(int value)
    {
        _mpSlider.value = value;
    }
    public void UpdateHPBar(int value)
    {
        _hpSlider.value = value;
    }
    public void UpdateHeartsBar(int value)
    {
        foreach (var item in _hearts)
        {
            item.sprite = _emptyHeart;
        }
        for (int i = 0; i < value; i++)
        {
            _hearts[i].sprite = _fillHeart; 
        }
    }
}
