using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    private void Start()
    {
        Shoot();
    }

    public async void Shoot()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(60f / _weaponData.FireRate));

        if (!transform.parent.gameObject.activeSelf)
        {
            return;
        }

        Projectile projectile = ObjectPooler.Instance.SpawnFromPool(_weaponData.Projectile).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.transform.eulerAngles = transform.eulerAngles;

        if (projectile != null)
        {
            projectile.Launch(_weaponData);
        }

        Shoot();
    }
}