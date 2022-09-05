using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePos;
    public abstract void LeftClick();
    public abstract void RightClick();
    public abstract void PressR();
}
