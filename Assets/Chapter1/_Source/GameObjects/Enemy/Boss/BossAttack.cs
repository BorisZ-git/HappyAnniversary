using System.Collections;
using UnityEngine;
using Supporting;
using GameCamera;
[RequireComponent(typeof(Collider2D))]
public class BossAttack : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _timeAttack;
    [SerializeField] private float _timeEnergyAttack;
    [SerializeField] private float _attackRestore;
    [SerializeField] private float _shakeTime;
    [Header("Links")]
    [SerializeField] private BossBehaviour _bossBehaviour;
    [Header("Spawn Properties")]
    [SerializeField] private PointSpawnFallenObj[] _points;
    [Header("Damage Layer")]
    [SerializeField] private LayerMask _playerLayer;

    private GameUI.Status _dmgObj;
    private int _dmg;
    private CameraMove2D _camera;
    private BossAttackSpawn _attackSpawn;
    private bool _meleeAttack, _energyAttack;

    private void Awake()
    {
        if(_bossBehaviour == null)
        {
            _bossBehaviour = GetComponentInParent<BossBehaviour>();
        }
        _dmg = _bossBehaviour.Dmg;
        _camera = FindFirstObjectByType<Camera>().GetComponent<CameraMove2D>();
        _attackSpawn = new BossAttackSpawn(GetComponentInChildren<PoolFallenObjs>(), _points);
    }
    private void FixedUpdate()
    {
        if(_dmgObj != null && !_bossBehaviour.IsDead)
        {
            _bossBehaviour.TryAttack(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _playerLayer))
        {
            _dmgObj = collision.gameObject.GetComponent<GameUI.Status>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _playerLayer))
        {
            _dmgObj = null;
        }
    }
    private void SetDamage()
    {
        if(_dmgObj!= null)
        {
            _dmgObj.SetHP(-_dmg);
        }
    }
    public void FullEnergyAttack(bool value)
    {
        StopAllCoroutines();
        if (value)
        {
            _energyAttack = true;
            StartCoroutine(WaitAttackAnim(_timeEnergyAttack));
        }
        else
        {
            _camera.Shake(_shakeTime);
            StartCoroutine(StateSpawn());
        }
    }
    public void MeleeAtack(bool value)
    {
        StopAllCoroutines();
        if (value)
        {
            _meleeAttack = true;
            StartCoroutine(WaitAttackAnim(_timeAttack));
        }
        else
        {
            StartCoroutine(RestoreAfterAttack());
        }
    }
    IEnumerator WaitAttackAnim(float time)
    {
        _bossBehaviour.IsAttack = true;
        yield return new WaitForSeconds(time);
        SetDamage();
        if (_meleeAttack)
        {
            _bossBehaviour.TryAttack(false);
        }
        if (_energyAttack)
        {
            _bossBehaviour.TryFullEnergyAttack(false);
        }
    }
    IEnumerator RestoreAfterAttack()
    {
        yield return new WaitForSeconds(_attackRestore);
        _meleeAttack = false;
        _energyAttack = false;
        _bossBehaviour.IsAttack = false;
    }
    IEnumerator StateSpawn()
    {
        _attackSpawn.SpawnObj();
        StartCoroutine(RestoreAfterAttack());
        yield return new WaitForSeconds(_attackRestore);
        if (_bossBehaviour.IsAngry)
        {
            _attackSpawn.SpawnEnemy();
        }
    }
    public void ResetAttack()
    {
        StopAllCoroutines();
        _meleeAttack = false;
        _energyAttack = false;
    }
}
