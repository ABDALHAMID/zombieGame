using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : MonoBehaviour
{
    public GameObject[] weapons;

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }
    public void ChangeWeapon(int index)
    {
        for(int i=0; i < weapons.Length; i++)
        {
            if(i == index)
            {
                weapons[i].SetActive(true);
                weapons[i].GetComponent<WeaponScript>().ApplyValues();
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }
}
