using System.Collections.Generic;
using UnityEngine;
using Platformer.MessageEventUI;
namespace Player
{
    public class PlayerUse : MonoBehaviour
    {
        [SerializeField] private Vector2 _useZone;
        [SerializeField] private string _usableObjectTag;
        private GameManager.PlatformerManager _levelManager;
        private MessageLvlController _msgLvlController;
        private List<GameObject> _usableObjects;
        private GameObject _useObject;
        private bool _IsUseTrue;
        private void Start()
        {
            _usableObjects = new List<GameObject>();
            _levelManager = FindObjectOfType<GameManager.PlatformerManager>();
            _msgLvlController = _levelManager.GetComponent<MessageLvlController>();
            if (string.IsNullOrEmpty(_usableObjectTag))
            {
                Debug.Log("Not set _usableObjectTag it will set default name: UsableObject");
                _usableObjectTag = "UsableObject";
            }
        }
        public void Using()
        {
            if (_usableObjects.Count > 0)
            {
                if(_usableObjects.Count > 1)
                {
                    _useObject = UseClosedObj();
                }
                else
                {
                    _useObject = _usableObjects[0];
                }
            }
            if(_useObject != null)
            {
                TryUse();
            }
            else
            {
                print("useObject empty");
            }
        }
        private void TryUse()
        {
            _msgLvlController.PlayerTryUse(_useObject);
            if (_useObject.GetComponent<Checkpoints.Store>())
            {
                _IsUseTrue = UseCheckPoint();
            }
            else if (_useObject.GetComponent<InteractibleObj.ButtonFallObjs>())
            {
                _IsUseTrue = _useObject.GetComponent<InteractibleObj.ButtonFallObjs>().Use();
            }
            else if (_useObject.GetComponent<InteractibleObj.Button>())
            {
                _IsUseTrue = _useObject.GetComponent<InteractibleObj.Button>().PushButton();
            }
            else if (_useObject.GetComponent<SwitchBehaviour>())
            {
                _useObject.GetComponent<SwitchBehaviour>().Switch();
                _IsUseTrue = true;
            }
            if (!_IsUseTrue)
            {
                GetComponentInParent<PlayerAudioSource>().AudioUse();
            }
        }

        private bool UseCheckPoint()
        {
            Checkpoints.Store tmp = _useObject.GetComponent<Checkpoints.Store>();
            _useObject = null;
            if (!tmp.IsChecked)
            {
                if (tmp.IsFinish)
                {
                    _levelManager.PlayerFindStore();
                }
                else
                {
                    tmp.StoreCheck();
                    _levelManager.CheckGoal();
                }
                return true;
            }
            return false;
        }

        private GameObject UseClosedObj()
        {
            int a = 0;
            for (int i = 1; i < _usableObjects.Count; i++)
            {
                if(Vector2.Distance(gameObject.transform.position, _usableObjects[i].transform.position) < 
                    Vector2.Distance(gameObject.transform.position, _usableObjects[a].transform.position))
                {
                    a = i;
                }
            }
            return _usableObjects[a];
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(_usableObjectTag))
            {
                _usableObjects.Add(collision.gameObject);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_usableObjectTag))
            {
                _usableObjects.Remove(collision.gameObject);
                if(_useObject != null)
                {
                    _useObject = null;
                }
            }
        }
    }
}
