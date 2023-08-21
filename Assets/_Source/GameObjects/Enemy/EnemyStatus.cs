using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class EnemyStatus : Status
    {
        [Header("Links")]
        [SerializeField] private Enemy.EnemyBehaviour _enemyBehaviour;


        protected override void Start()
        {
            if(HpBar == null)
            {
                HpBar = transform.Find("HPBar").gameObject;
            }
            base.Start();
            _enemyBehaviour = GetComponent<Enemy.EnemyBehaviour>();
        }
        public override void SetHP(int value)
        {
            base.SetHP(value);
            if (value < 0 && Hp > 0)
            {
                Hurt();
            }
            if(Hp <= 0)
            {
                LooseHP();
            }
        }
        protected override void LooseHP()
        {
            _enemyBehaviour.Dead();
        }
        protected override void Hurt()
        {
            _enemyBehaviour._audioData.AudioHurt();
            _enemyBehaviour.EnemyAnim.Hurted();
        }
    }
}

