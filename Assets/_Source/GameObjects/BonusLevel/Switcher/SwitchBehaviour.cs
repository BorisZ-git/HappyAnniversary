using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class SwitchBehaviour : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BonusLevelManager _bonusLvlManager;
    [SerializeField] private GameManager.GameManager _gameManager;
    [Header("Sprite Data")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    private AudioSource _audioSource;
    private bool _isOn;
    public bool isOn { get => _isOn; }
    public bool isOff { get; set; }

    private void Awake()
    {
        if (_gameManager == null)
            _gameManager = FindObjectOfType<GameManager.GameManager>();
        if (_spriteRenderer == null)
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (_inputManager == null)
            _inputManager = FindObjectOfType<InputManager>();
        if (_bonusLvlManager == null && !_gameManager.IsBossLvl)
            _bonusLvlManager = FindObjectOfType<BonusLevelManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void Switch()
    {
        if (_bonusLvlManager)
        {
            if (!_bonusLvlManager.IsLevelMove && !_gameManager.IsBossLvl && !isOff)
            {
                _bonusLvlManager.LevelStartMove();
            }
        }
        if (_isOn && !isOff)
        {
            SwitchToPlayer();
            _isOn = false;
            _spriteRenderer.sprite = _off;
        }
        else if(!_isOn && !isOff) 
        {
            SwitchToObject();
            _isOn = true;
            _spriteRenderer.sprite = _on;
        }
        _audioSource.Play();
    }
    private void SwitchToPlayer()
    {
        _inputManager.ChangeToPlayer();
    }
    private void SwitchToObject()
    {
        _inputManager.ChangeToAnother();
    }
    public IEnumerator DeactivateSwitch(float time)
    {
        isOff = true;
        yield return new WaitForSeconds(time);
        isOff = false;
    }
}
