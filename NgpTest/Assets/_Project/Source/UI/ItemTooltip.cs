using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _typeText;
    
    public void SetItem(Item item)
    {
        _nameText.text = item.Name;
        _descriptionText.text = item.Description;
        _typeText.text = item.SlotType.ToString();
    }
    
}
