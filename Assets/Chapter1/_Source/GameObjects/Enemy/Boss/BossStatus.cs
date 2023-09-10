using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;
[RequireComponent(typeof(BossBehaviour))]
public class BossStatus : Status
{
    [Header("Links")]
    [SerializeField] private BossUI _uiStatus;
    private BossBehaviour _bossBehaviour;
    private int _maxHP, _maxMP, _lifes;

    private void Awake()
    {
        if(_uiStatus == null)
        {
            _uiStatus = FindObjectOfType<BossUI>();
        }
        _bossBehaviour = GetComponent<BossBehaviour>();
        _maxHP = Hp;
        _maxMP = Mp;
        _lifes = _uiStatus.HeartsCount;
        Mp = 0;
    }
    protected override void Start()
    {
        _uiStatus.SetSlidersValue(_maxHP, _maxMP);
    }
    protected override void LooseHP()
    {
        _bossBehaviour.Dead();
    }
    public override void SetHP(int value)
    {
        if(value < 0)
        {
            Hurt();
        }
        Hp += value;
        if (Hp > _maxHP)
        {
            Hp = _maxHP;
        }
        else if(Hp <= 0)
        {
            CheckHeartStatus();
        }
        _uiStatus.UpdateHPBar(Hp);
    }
    public override void SetMP(int value)
    {
        Mp += value;
        if(Mp >= _maxMP)
        {
            if (_bossBehaviour.TryFullEnergyAttack(true))
            {
                Mp = 0;
            }
        }
        if(Mp < 0)
        {
            Mp = 0;
        }
        _uiStatus.UpdateMPBar(Mp);
    }
    public override void UpdateViewStatus(List<Transform> lines, int barValue) {}
    private void CheckHeartStatus()
    {
        if(_lifes > 0)
        {
            _lifes--;
            if (_lifes == 0)
            {
                _bossBehaviour.SetAngryMode();
            }
            _uiStatus.UpdateHeartsBar(_lifes);
            Hp = _maxHP;
            Mp = 0;
            StartCoroutine( _bossBehaviour.LooseLife());
        }
        else
        {
            LooseHP();
        }
    }

    protected override void Hurt()
    {
        _bossBehaviour.Hurt();
    }
}
