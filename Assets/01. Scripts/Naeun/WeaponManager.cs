using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    int curWeaponIndex = 0;

    void Start()
    {

    }

    void Update()
    {
        WeaponSwap();
    }

    protected virtual void WeaponSwap()
    {
        float mouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            int newIndex = curWeaponIndex + (mouseWheel > 0 ? 1 : -1);

            newIndex = newIndex % weapons.Count;

            if (newIndex < 0)
            {
                newIndex = weapons.Count - 1;
            }

            print($"Prev : {curWeaponIndex}  New : {newIndex}");
            ChangeWeapon(weapons[newIndex], weapons[curWeaponIndex]);
            curWeaponIndex = newIndex;
        }
    }

    private void ChangeWeapon(Weapon newWeapon, Weapon prevWeapon)
        //prevWeapon은 끄고 newWeapon은 활성화 하기
    {
        newWeapon.gameObject.SetActive(true);
        prevWeapon.gameObject.SetActive(false);
    }
}

