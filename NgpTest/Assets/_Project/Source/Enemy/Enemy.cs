using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Action OnDeath;
    
    [SerializeField] private DamageFeedback _damageFeedback;
    [SerializeField] private int _hp;
    [SerializeField] private List<DropData> _drops = new();
    
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
        DropItem();
        _damageFeedback.Kill();
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }

    protected virtual void DropItem()
    {
        foreach (DropData itemDrop in _drops)
        {
            int randomValue = UnityEngine.Random.Range(0, 100);

            if (randomValue <= itemDrop.DropChance)
            {
                ItemDrop drop = ObjectPooler.Instance.SpawnFromPool(itemDrop.DropPrefab.gameObject).GetComponent<ItemDrop>();
                drop.SetItem(itemDrop.ItemData.Item);
                drop.transform.position = transform.position;
            }
        }
    }
}
