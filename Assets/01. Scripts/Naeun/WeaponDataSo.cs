using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSo : ScriptableObject
{
    public int bulletCapacity;
    public float attackDelay;
    public float range;
}
