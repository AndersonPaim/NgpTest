using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    [SerializeField] private GameObject _aoeWarningParticle;
    [SerializeField] private GameObject _aoeExplosionParticle;
    [SerializeField] private LayerMask _damageLayer;
    [SerializeField] private float _explosionHitAnimDuration;
    [SerializeField] private float _explosionExitAnimDuration;

    public async void Explode(float delay, float range, int damage)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay));

        _aoeExplosionParticle.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_explosionHitAnimDuration));

        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, _damageLayer);

        foreach (Collider collider in collidersInRange)
        {
            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        await UniTask.Delay(TimeSpan.FromSeconds(_explosionExitAnimDuration));

        transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        transform.DOScale(Vector3.zero, 0);
        _aoeExplosionParticle.SetActive(false);
    }
}