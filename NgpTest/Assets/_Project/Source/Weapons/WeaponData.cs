
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject Projectile;
    public int Damage;
    public int ShootSpeed;
    public int FireRate;

    public WeaponTypes WeaponType;
    public ProjectilePattern ProjectilePattern;
}