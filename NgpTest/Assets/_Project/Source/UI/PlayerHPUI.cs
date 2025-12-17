using UnityEngine;
using UnityEngine.UI;

class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    
    public void UpdateHpBar(int hp, int totalHp)
    {
        _hpBar.fillAmount = (float)hp / totalHp;
    }
}
