using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSo : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public AudioClip swapClip;
    public int bulletCapacity;
    public float attackDelay;
    public float reloadDelay;
    public Vector3 originPos;
    public Vector3 fineSightPos; // 정조준 시 위치
    public float range;
}
