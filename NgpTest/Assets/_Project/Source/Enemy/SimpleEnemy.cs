using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class SimpleEnemy : Enemy
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private float _rotationSpeed;

    private bool _canSeeThreat = false;
    
    private void Start()
    {
        Shoot();
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private async void Shoot()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(60f / _weaponData.FireRate));

        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        
        Projectile projectile = ObjectPooler.Instance.SpawnFromPool(_weaponData.Projectile).GetComponent<Projectile>();
        projectile.transform.position = _shootPosition.position;
        projectile.transform.eulerAngles = _shootPosition.eulerAngles;

        if (projectile != null)
        {
            projectile.Launch(_weaponData);
        }

        projectile.GetComponent<Rigidbody>().linearVelocity = _shootPosition.forward * _weaponData.ShootSpeed;
        
        Shoot();
    }


    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        directionToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}