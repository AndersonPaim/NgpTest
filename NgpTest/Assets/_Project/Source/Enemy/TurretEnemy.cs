using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class TurretEnemy : Enemy
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _spawnPoints;

    private void Start()
    {
        TurretRotation();
        Shoot();
    }

    private void TurretRotation()
    {
        transform.DORotate(new Vector3(0, 360, 0), _rotationSpeed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
    }

    private async void Shoot()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(60f / _weaponData.FireRate));

        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        Vector3 startPos = transform.position;
        float angle = 0;
        float angleStep = 360 / _spawnPoints;

        for (int i = 0; i < _spawnPoints; i++)
        {
            float dirXPosition = startPos.x + Mathf.Sin(angle * Mathf.PI / 180);
            float dirYPosition = startPos.y + Mathf.Cos(angle * Mathf.PI / 180);
            Vector3 dirVector = new(dirXPosition, dirYPosition, 0);
            Vector3 moveDir = (dirVector - startPos).normalized * _weaponData.ShootSpeed;

            Projectile projectile = ObjectPooler.Instance.SpawnFromPool(_weaponData.Projectile).GetComponent<Projectile>();
            projectile.transform.position = startPos;
            projectile.transform.rotation = Quaternion.identity;

            if (projectile != null)
            {
                projectile.Launch(_weaponData);
            }
            
            Vector3 moveDirection = new Vector3(moveDir.x, 0, moveDir.y);
            Quaternion rotation = transform.rotation;
            Vector3 rotatedDirection = rotation * moveDirection;
            
            projectile.GetComponent<Rigidbody>().linearVelocity = rotatedDirection;

            angle += angleStep;
        }

        Shoot();
    }
}
