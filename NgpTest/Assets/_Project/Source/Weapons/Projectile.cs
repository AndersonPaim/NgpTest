using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private WeaponData _weaponData;
    private Rigidbody _rb;

    public void Launch(WeaponData weaponData)
    {
        _weaponData = weaponData;
        _rb.AddForce(transform.forward * weaponData.ShootForce);
        DestroyDelay();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.linearVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_weaponData.Damage);
        }

        DestroyProjectile();
    }

    private async void DestroyDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        //TODO PLAY PARTICLE
        gameObject.SetActive(false);
    }
}