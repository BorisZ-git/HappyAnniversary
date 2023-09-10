using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
using Bullet;
using GameUI;

public class PlayerDistanceAttack
{
    private ObjectPool _bulletPool;
    private bool _isAttack;
    private GameObject bullet;
    public PlayerDistanceAttack(GameObject prefab, int objectsCount, int damage, Transform parent)
    {
        _bulletPool = new ObjectPool(prefab, objectsCount, parent);
    }
    public bool TrySetFlyKiss(PlayerStatus status)
    {
        if (_isAttack || status.Mp == 0)
        {
            return false;
        }
        else
        {
            _isAttack = true;
            bullet = _bulletPool.GetInactiveObject();
            status.SetMP(-1);
            return true;
        }
    }
    public void InitFlyKiss(Vector2 spawnPosition, bool directionForward, int damage)
    {
        bullet.GetComponent<BulletBehaviour>().Init(spawnPosition, directionForward, damage);
    }
    public void StartFlyKiss(bool directionForward)
    {
        bullet.GetComponent<BulletBehaviour>().Blow(directionForward);
    }
    public void FinishDistanceAttack()
    {
        _isAttack = false;
    }
}
