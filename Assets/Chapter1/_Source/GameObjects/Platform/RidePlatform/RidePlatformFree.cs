using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
namespace BonusLevel.RidePlatform
{
    public class RidePlatformFree : RidePlatformGeneral
    {
        [Header("GameBorderBox")]
        [SerializeField]private DrawArea _moveArea;
        private BlockZones _block;
        protected override void Awake()
        {
            base.Awake();
            _moveArea = GetComponentInChildren<DrawArea>();
            _block = GetComponentInChildren<BlockZones>();
        }
        private void Start()
        {
            _platformMove = new PlatformMove(this.transform, _moveArea.AreaX[0], _moveArea.AreaX[1], _moveArea.AreaY[1], _moveArea.AreaY[0], _block);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _movingObjects))
            {

                    collision.transform.parent = gameObject.transform;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!Utils.IsInLayer(collision.gameObject.layer, _playerMask))
            {
                collision.transform.SetParent(null);
            }
        }
        public void OffsetMoveArea(float offsetLeft, float offsetRight)
        {
            _platformMove.OffsetBorders(offsetLeft, offsetRight);
        }
    }
}



