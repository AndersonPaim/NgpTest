using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<WeaponData> _weapons = new();
    [SerializeField] private Transform _shootPosition;

    private WeaponData _weaponData;

    private bool _canShoot = true;

    public void Shoot()
    {
        if (!_canShoot)
        {
            return;
        }

        Projectile projectile = ObjectPooler.Instance.SpawnFromPool(_weaponData.Projectile).GetComponent<Projectile>();
        projectile.transform.position = _shootPosition.position;
        projectile.transform.eulerAngles = _shootPosition.eulerAngles;

        if (projectile != null)
        {
            Vector3 projectileDir = _shootPosition.forward * _weaponData.ShootSpeed;
            projectile.GetComponent<Rigidbody>().linearVelocity = projectileDir;
            projectile.Launch(_weaponData);
        }

        ShootCooldown();
    }

    private void Start()
    {
        if (_weaponData == null && _weapons.Count > 0)
        {
            _weaponData = _weapons[0];
        }
    }

    private async void ShootCooldown()
    {
        _canShoot = false;
        await UniTask.Delay(TimeSpan.FromSeconds(60f / _weaponData.FireRate));
        _canShoot = true;
    }
}