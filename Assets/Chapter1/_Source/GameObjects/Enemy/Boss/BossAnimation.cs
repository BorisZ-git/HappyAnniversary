using UnityEngine;

public class BossAnimation : MonoBehaviour
{

    [Header("Hash Animation")]
    [SerializeField] private string _hashAnimAttack;
    [SerializeField] private string _hashAnimIsAngry;
    [SerializeField] private string _hashAnimEnergyAttack;
    [SerializeField] private string _hashAnimLostLife;
    [SerializeField] private string _hashAnimDead;
    [Header("Values")]
    [SerializeField] private float _hurtSpeed;
    private Animator _animator;
    private bool _isHurt;
    private SpriteRenderer _spriteRender;
    private HurtAnimation _hurtAnim;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
        _hurtAnim = new HurtAnimation(_spriteRender, _hurtSpeed);
    }
    private void Update()
    {
        if (_isHurt)
        {
            _isHurt = _hurtAnim.HurtAnim();
        }
    }
    public void SetAngry()
    {
        _animator.SetBool(_hashAnimIsAngry, true);
    }
    public void MeleeAttack(bool value)
    {
        _animator.SetBool(_hashAnimAttack, value);
    }
    public void EnergyAttack(bool value)
    {
        _animator.SetBool(_hashAnimEnergyAttack, value);
    }
    public void Hurted()
    {
        _isHurt = _hurtAnim.ResetHurtAnim();
    }
    public void LostLife()
    {
        MeleeAttack(false);
        EnergyAttack(false);
        _animator.SetTrigger(_hashAnimLostLife);
    }
    public void Dead()
    {
        _animator.SetTrigger(_hashAnimDead);
    }
}
