

    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "ItemSpritesList", menuName = "ScriptableObjects/ItemSpritesList")]
    public class ItemSpritesList : ScriptableObject
    {
        public List<ItemSprites> Sprites = new();
    }
