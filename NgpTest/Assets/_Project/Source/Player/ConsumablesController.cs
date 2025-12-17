
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesController : MonoBehaviour
{
        [SerializeField] private List<ItemData> _consumableItems = new();
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameObject _healParticle;
        
        public void UseConsumable(ItemType itemType)
        {
                ItemData item =  _consumableItems.Find(item => item.Item.ItemType == itemType);

                if (item != null)
                {
                    if (item.Item.EffectType == EffectType.Healing)
                    {
                        _playerController.Heal(item.Item.EffectValue);
                        GameObject healParticle = ObjectPooler.Instance.SpawnFromPool(_healParticle);
                        healParticle.transform.position = transform.position;
                    }
                }
        }
}
