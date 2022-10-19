using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private int damage;
    public override void Attack()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < ShootingRange && NextFireTime < Time.time)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Player.gameObject.GetComponent<Player>().TakeDamage(damage);
            NextFireTime = Time.time + FireRate;
        }
    }
}
