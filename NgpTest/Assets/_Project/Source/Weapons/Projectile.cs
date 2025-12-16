using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    protected WeaponData WeaponData;
    private Rigidbody _rb;

    public virtual void Launch(WeaponData weaponData)
    {
        WeaponData = weaponData;
        //_spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //_rb.AddForce(transform.forward * weaponData.ShootForce);
        DestroyDelay();
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
        }

        DestroyProjectile();
    }

    protected virtual async void DestroyDelay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime));

        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    /*private void Update()
    {
        if (_hasInitialized)
            BulletRotation();
    }

    private void BulletRotation()
    {
        _timer += Time.deltaTime;

        float x = _timer * WeaponData.CurveSpeed * transform.right.x;
        float y = _timer * WeaponData.CurveSpeed * transform.right.y;
        Vector3 newPos = new(x + _spawnPoint.x, transform.eulerAngles.y, y + _spawnPoint.y);
        transform.position = newPos;
    }*/
}