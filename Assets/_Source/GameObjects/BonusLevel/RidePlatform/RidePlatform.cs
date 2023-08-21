using UnityEngine;
using Supporting;
using Enemy;
using System.Collections;

namespace BonusLevel.RidePlatform
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class RidePlatform : RidePlatformGeneral
    {
        [Header("Stopping Platform Property")]
        [SerializeField] private string _flashAnimName;
        [SerializeField] private float _timeToTransfer;
        [Header("Links")]
        [SerializeField] private BonusLevelManager _level;
        [Header("GameBorderBox")]
        [SerializeField] protected float _x;
        [SerializeField] protected float _y;

        private Animator _platformAnimator;
        private Transform _pointStable;
        private bool _isPlatformStopping;
        private bool _isEndPath;
        override protected void Awake()
        {
            base.Awake();
            _platformMove = new PlatformMove(GetComponent<Transform>(), _x, _y);
            _platformAnimator = GetComponent<Animator>();
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _movingObjects))
            {
                if (!_isEndPath)
                {
                    collision.transform.parent = gameObject.transform;
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!Utils.IsInLayer(collision.gameObject.layer, _playerMask))
            {
                collision.transform.GetComponent<Collider2D>().isTrigger = false;
                if (_level)
                {
                    collision.transform.SetParent(_level.transform);
                }
                else
                {
                    collision.transform.SetParent(null);

                }
            }
        }
        private void Update()
        {
            if (_isPlatformStopping && _pointStable != null)
            {
                _platformMove.MoveTo(_speed * Time.deltaTime, _pointStable.position.x, _pointStable.position.y);
            }
        }
        public void TurnEngine()
        {
            _audioSource.clip = _acEngineOn;
            _audioSource.Play();
            _platformAnimator.Play(_flashAnimName);
            _isEndPath = false;
        }
        public void StoppingPlatform(Transform pointStable)
        {
            _audioSource.clip = _acEngineOff;
            _audioSource.Play();
            _pointStable = pointStable;
            _isPlatformStopping = true;
            foreach (var item in GetComponentsInChildren<Rigidbody2D>())
            {
                StopLogic(item.gameObject,false);
            }
        }
        private void StopLogic(GameObject obj,bool isPlayerExclusive)
        {
            if (Utils.IsInLayer(obj.layer,_movingObjects) && !Utils.IsInLayer(obj.layer, _playerMask))
            {
                obj.GetComponent<Collider2D>().isTrigger = true;
            }
            if(Utils.IsInLayer(obj.layer, _movingObjects) && !Utils.IsInLayer(obj.layer,_playerMask) || Utils.IsInLayer(obj.layer, _movingObjects) && isPlayerExclusive)
            {
                obj.transform.SetParent(null);
            }
        }
        public IEnumerator EndRide(Transform point)
        {
            _isEndPath = true;
            _isPlatformStopping = false;
            foreach (var item in GetComponentsInChildren<Rigidbody2D>())
            {
                StopLogic(item.gameObject, true);
            }
            yield return new WaitForSeconds(_timeToTransfer);
            gameObject.transform.position = point.position;
            _platformMove.OffsetBorders(gameObject.transform.position.x - _x, gameObject.transform.position.x + _x);
        }
    }
}

