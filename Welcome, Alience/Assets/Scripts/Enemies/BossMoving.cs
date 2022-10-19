using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private float ShootingRange;
    [SerializeField] private float MovingRange;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject shotPoint;
    [SerializeField] private GameObject shotPoint2;
    [SerializeField] private float NextFireTime;
    [SerializeField] private float FireRate;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        BossMove();
        BossDeath();
    }

    private void BossMove()
    {
        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer > ShootingRange && distanceFromPlayer < MovingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer < ShootingRange && NextFireTime < Time.time)
        {
            BossAttack();
        }
    }
    private void BossAttack()
    {
        Instantiate(rocket, shotPoint.transform.position, transform.rotation);
        Instantiate(rocket, shotPoint2.transform.position, transform.rotation);
        NextFireTime = Time.time + FireRate;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    private void BossDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
        Gizmos.DrawWireSphere(transform.position, MovingRange);
    }
}
