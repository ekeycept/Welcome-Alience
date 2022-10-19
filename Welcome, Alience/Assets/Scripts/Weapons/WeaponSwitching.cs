using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int numberOfWeapon = 0;
    [SerializeField] private bool isChanged = false;
    void Update()
    {
        SwitchWeaponPC();

        if(isChanged == true)
        {
            for (int i = 0; i < weapons.Length; i++)
                weapons[i].SetActive(false);
            weapons[numberOfWeapon].SetActive(true);
            isChanged = false;
        }

    }

    private void SwitchWeaponPC()
    {
        if (Input.GetKeyDown("1"))
        {
            if (weapons.Length >= 1)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[0].SetActive(true);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (weapons.Length >= 2)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[1].SetActive(true);
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (weapons.Length >= 3)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[2].SetActive(true);
            }
        }
        if (Input.GetKeyDown("4"))
        {
            if (weapons.Length >= 4)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[3].SetActive(true);
            }
        }
        if (Input.GetKeyDown("5"))
        {
            if (weapons.Length >= 5)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[4].SetActive(true);
            }
        }
        if (Input.GetKeyDown("6"))
        {
            if (weapons.Length >= 6)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[5].SetActive(true);
            }
        }
        if (Input.GetKeyDown("7"))
        {
            if (weapons.Length >= 7)
            {
                for (int i = 0; i < weapons.Length; i++)
                    weapons[i].SetActive(false);
                weapons[6].SetActive(true);
            }
        }
    }

    public void ChooseNextWeapon()
    {
        /*        if(numberOfWeapon < 7)
                {
                    numberOfWeapon += 1;
                    weapons[numberOfWeapon - 2].SetActive(false);
                    weapons[numberOfWeapon - 1].SetActive(true);
                }
                if(numberOfWeapon == 7)
                {
                    weapons[numberOfWeapon - 1].SetActive(false);
                    numberOfWeapon = 1;
                }*/
        if(numberOfWeapon < 7)
        {
            numberOfWeapon++;
            isChanged = true;
        }
    }

    public void ChoosePreviousWeapon()
    {
/*        if (numberOfWeapon > 0)
        {
            numberOfWeapon -= 1;
            weapons[numberOfWeapon + 1].SetActive(false);
            weapons[numberOfWeapon - 1].SetActive(true);
        }
        if (numberOfWeapon == 0)
            numberOfWeapon = 7;*/
        if (numberOfWeapon > 0)
        {
            numberOfWeapon--;
            isChanged = true;
        }
    }

    public void AddWeapon(GameObject weaponPrefab)
    {
        Array.Resize(ref weapons, weapons.Length + 1);
        weapons[weapons.Length - 1] = weaponPrefab;
    }
}

