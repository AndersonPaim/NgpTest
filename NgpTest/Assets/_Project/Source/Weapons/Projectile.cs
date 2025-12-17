using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private GameObject _explosionParticle;
    [SerializeField] private bool _isDestructible;

    protected WeaponData WeaponData;
    private Rigidbody _rb;

    public virtual void Launch(WeaponData weaponData)
    {
        StopCoroutine(DestroyDelay());
        WeaponData = weaponData;
        StartCoroutine(DestroyDelay());
    }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable()
    {
        _rb.linearVelocity = Vector3.zero;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(WeaponData.Damage);

            if (!_isDestructible)
            {
                DestroyProjectile();
            }
        }

        if (_isDestructible)
        {
            DestroyProjectile();
        }
    }

    protected virtual IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        GameObject explosionParticle = ObjectPooler.Instance.SpawnFromPool(_explosionParticle);
        explosionParticle.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}