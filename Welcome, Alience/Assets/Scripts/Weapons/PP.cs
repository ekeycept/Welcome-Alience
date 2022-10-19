using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP : Weapon
{
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateWeapon();
        Shoot();
        FlipWeapon();
    }
}
