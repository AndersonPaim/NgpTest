using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageFeedback _damageFeedback;
    [SerializeField] private int _hp;

    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        _damageFeedback.Damage();

        if (_hp <= 0)
        {
            Kill();
        }
    }

    protected virtual void Kill()
    {
        _damageFeedback.Kill();
        gameObject.SetActive(false);
    }
}
