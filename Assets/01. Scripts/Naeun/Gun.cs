using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private LayerMask layer;
    WeaponDataSo data;

    void Start()
    {

    }

    void Update()
    {

    }

    public override void LeftClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hit.transform.GetComponent<IDamaged>();
        }
    }
    public override void RightClick()
    {

    }
    public override void PressR()
    {

    }
}
