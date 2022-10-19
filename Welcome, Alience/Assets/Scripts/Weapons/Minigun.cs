using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Weapon
{
    [SerializeField] private Transform shotPoint2;
    [SerializeField] private Transform shotPoint3;
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

    protected override void Shoot()
    {
        if (timeBtwShots <= 0 && timeForReloading <= 0)
        {
            if (Input.GetMouseButton(0) && ShotsNumber < MaxShotsNumber)
            {
                Instantiate(Bullet, shotPoint.position, transform.rotation);
                Instantiate(Bullet, shotPoint2.position, transform.rotation);
                Instantiate(Bullet, shotPoint3.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                ShotSound.Play();
                ShotsNumber++;
            }
            else if (ShotsNumber >= MaxShotsNumber || Input.GetKeyDown("r"))
            {
                Reload();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            timeForReloading -= Time.deltaTime;
        }
    }
}
