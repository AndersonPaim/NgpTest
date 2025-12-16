using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MortarEnemy : Enemy
{
    [SerializeField] private GameObject _aoeAttackPrefab;
    [SerializeField] private float _attackInterval;
    [SerializeField] private float _bombsPerAttack;
    [SerializeField] private float _explosionDelay;
    [SerializeField] private float _explosionRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _explosionDamage;

    private void Start()
    {
        Shoot();
    }

    private async void Shoot()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_attackInterval));

        if (!gameObject.activeSelf)
        {
            return;
        }

        for (int i = 0; i < _bombsPerAttack; i++)
        {

            float randomXPos = UnityEngine.Random.Range(-_attackRange, _attackRange);
            float randomZPos = UnityEngine.Random.Range(-_attackRange, _attackRange);
            Vector3 spawnPosition = transform.position + new Vector3(randomXPos, 0, randomZPos);
            spawnPosition.y = 1;
            AoeAttack aoeAttack = ObjectPooler.Instance.SpawnFromPool(_aoeAttackPrefab).GetComponent<AoeAttack>();
            aoeAttack.transform.DOScale(Vector3.one, 0.3f);
            aoeAttack.transform.position = spawnPosition;
            aoeAttack.transform.eulerAngles = transform.eulerAngles;

            if (aoeAttack != null)
            {
                aoeAttack.Explode(_explosionDelay, _explosionRange, _explosionDamage);
            }
        }

        Shoot();
    }
}
