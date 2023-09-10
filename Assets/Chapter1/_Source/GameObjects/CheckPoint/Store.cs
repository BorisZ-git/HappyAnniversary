using UnityEngine;

namespace Checkpoints
{
    [RequireComponent(typeof(AudioSource))]
    public class Store : MonoBehaviour
    {
        [Header("CheckPoint Conditions")]
        [SerializeField] private bool _isFinish;

        [Header("LED UI")]
        [SerializeField] private bool _setColorDefault;
        [SerializeField] private Color _colorLEDOn;
        [SerializeField] private Color _colorLEDOff;
        [SerializeField] private Color _colorUIOff;


        [Header("Links")]
        [SerializeField] private SpriteRenderer _uISprite;

        [Space(15)]
        [SerializeField] private GameManager.PlatformerManager _manager;
        [Space(15)]
        [SerializeField] private bool _isChecked;
        private StoreUIControls _storeUI;
        private AudioSource _audioSource;
        

        #region Property
        public bool IsChecked { get => _isChecked; set => _isChecked = value; }
        public bool IsFinish { get => _isFinish; set => _isFinish = value; }
        #endregion

        private void Start()
        {
            RequirementCheck();
            _storeUI = new StoreUIControls(_uISprite, _colorLEDOn, _colorLEDOff);
        }
        private void RequirementCheck()
        {
            _manager = FindObjectOfType<GameManager.PlatformerManager>();
            if (_setColorDefault)
            {
                _colorLEDOn = Color.white;
                _colorLEDOff = Color.grey;
            }
            if(_uISprite == null)
            {
                Debug.Log("No uiSprite");
            }
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_manager.PlayerUseZoneTag))
            {
                _storeUI.Check(_isChecked,true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_manager.PlayerUseZoneTag))
            {
                _storeUI.Check(_isChecked,false);
            }
        }
        public void StoreCheck()
        {
            _isChecked = true;
            _uISprite.enabled = false;
            _audioSource.Play();
        }
    }
}

