using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bonus
{
    public class BonusHearts : BonusItem
    {
        [Header("Values")]
        [SerializeField] private int _hp;
        protected override void Taked(GameObject player)
        {
            player.GetComponent<GameUI.PlayerStatus>().SetHP(_hp);
            base.Taked(player);
        }
    }
}

