using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private LayerMask layer;
    WeaponDataSo data;
    public WeaponDataSo Data { get { return data; } }
    private int curCapacity;

    void Start()
    {
        data.bulletCapacity = 12;
        data.attackDelay = 0.75f;
        curCapacity = data.bulletCapacity;
        StartCoroutine(Shooting());
    }

    void Update()
    {

    }

    public override void LeftClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // hit.transform.GetComponent<IDamaged>();
        }
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(data.attackDelay);
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            LeftClick();
            curCapacity--;
            Debug.DrawRay(firePos.position, firePos.forward * 15, Color.yellow);
        }
    }

    public override void RightClick()
    {

    }

    public override void PressR()
    {

    }
}
