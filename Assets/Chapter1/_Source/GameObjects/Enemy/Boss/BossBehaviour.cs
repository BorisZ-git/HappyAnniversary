using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class BossBehaviour : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private int _mpPerTime;
    [SerializeField] private float _timeMPDelay;
    [SerializeField] private int _attackMPCost;
    [SerializeField] private int _dmg;
    [SerializeField] private float _timeToRestoreLife;
    [Header("Layers")]
    [SerializeField] private LayerMask _playerLayer;
    [Header("Links")]
    [SerializeField] private Checkpoints.ExitPoint _exit;

    private Collider2D _collider;
    private BossAnimation _bossAnim;
    private BossAttack _attackBehaviour;
    private BossStatus _status;
    private bool _isDead;
    
    private float _timeCount;
    public bool IsAngry { get; set; }
    public bool IsAttack { get; set; }
    public bool IsDead { get => _isDead; }
    public int Dmg { get => _dmg; } 
    public LayerMask PlayerLayer { get => _playerLayer; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _attackBehaviour = GetComponentInChildren<BossAttack>();
        _status = GetComponent<BossStatus>();
        _bossAnim = GetComponent<BossAnimation>();
        _exit = FindObjectOfType<Checkpoints.ExitPoint>();
    }
    private void Update()
    {
        if (!IsAttack && !_isDead)
        {
            _timeCount += Time.deltaTime;
            if (_timeCount > _timeMPDelay)
            {
                _status.SetMP(_mpPerTime);
                _timeCount = 0;
            }
        }
    }
    public bool TryFullEnergyAttack(bool value)
    {
        if (!IsAttack || !value)
        {
            _attackBehaviour.FullEnergyAttack(value);
            _bossAnim.EnergyAttack(value);
            return true;
        }
        else
        {
            return false;
        }

    }
    public void TryAttack(bool value)
    {
        if (!IsAttack || !value)
        {
            _bossAnim.MeleeAttack(value);
            _attackBehaviour.MeleeAtack(value);
            if (value)
            {
                _status.SetMP(-_attackMPCost);
            }
        }
    }
    public void SetAngryMode()
    {
        _timeMPDelay = _timeMPDelay / 2;
        _attackMPCost = _attackMPCost * 2;
        _bossAnim.SetAngry();
        IsAngry = true;
    }
    public void Hurt()
    {
        _bossAnim.Hurted();
    }
    public IEnumerator LooseLife()
    {
        _attackBehaviour.ResetAttack();
        _bossAnim.LostLife();
        IsAttack = true;
        yield return new WaitForSeconds(_timeToRestoreLife);
        IsAttack = false;
    }
    public void Dead()
    {
        _isDead = true;
        _attackBehaviour.ResetAttack();
        _collider.enabled = false;
        _bossAnim.Dead();
        _exit.ActivateExit();
    }
}
