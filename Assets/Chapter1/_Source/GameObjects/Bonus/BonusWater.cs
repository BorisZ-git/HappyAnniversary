using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bonus
{
    public class BonusWater : BonusItem
    {
        [Header("Values")]
        [SerializeField] private int _mp;
        protected override void Taked(GameObject player)
        {
            player.GetComponent<GameUI.PlayerStatus>().SetMP(_mp);
            base.Taked(player);
        }
    }
}

