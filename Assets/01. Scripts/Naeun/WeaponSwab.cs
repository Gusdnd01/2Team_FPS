using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwab : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        Swab();
    }

    void Swab()
    {

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            GameObject _weapon = weapons[0];
            for (int i = 0; i < weapons.Count - 1; i++)
            {
                Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
                weapons[i] = weapons[i + 1];
            }
            weapons[weapons.Count - 1] = _weapon;
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            GameObject _weapon = weapons[0];
            for (int i = weapons.Count - 1; i > 0; i--)
            {
                weapons[i - 1] = weapons[i];
            }
            weapons[weapons.Count - 1] = _weapon;
        }
    }
}
