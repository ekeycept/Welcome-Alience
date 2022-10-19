using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int health;
    [SerializeField] protected float LineOfSite;
    [SerializeField] protected float ShootingRange;
    [SerializeField] protected float FireRate = 1f;
    [SerializeField] protected float NextFireTime;
    [SerializeField] protected bool facingright;
    [SerializeField] protected GameObject Bullet;
    [SerializeField] protected GameObject ShootPosition;
    [SerializeField] protected GameObject Healer;
    [SerializeField] protected GameObject LootPosition;
    [SerializeField] protected GameObject Shield;
    [SerializeField] protected GameObject Microchip;
    [SerializeField] protected GameObject Coin;
    [SerializeField] protected GameObject Boom;
    [SerializeField] protected GameObject Cocktail;
    protected Transform player;

    public Animator enemyanim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Move();
        Attack();
        Flip();
        DamageAnim();
        Death();
    }

    protected void Move()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < LineOfSite && distanceFromPlayer > ShootingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            enemyanim.SetBool("IsMoving", true);
        }
    }

    public virtual void Attack()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= ShootingRange && NextFireTime < Time.time)
        {
            Instantiate(Bullet, ShootPosition.transform.position, Quaternion.identity);
            NextFireTime = Time.time + FireRate;

            enemyanim.SetBool("IsMoving", false);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }

    protected void Flip()
    {
        if ((transform.position.x < player.position.x && !facingright) || (transform.position.x > player.position.x && facingright))
        {
            facingright = !facingright;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    protected void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            LootDrop();
        }
    }

    protected void LootDrop()
    {
        Instantiate(Healer, LootPosition.transform.position, transform.rotation);
        Instantiate(Shield, LootPosition.transform.position, transform.rotation);
        Instantiate(Microchip, LootPosition.transform.position, transform.rotation);
        Instantiate(Coin, LootPosition.transform.position, transform.rotation);
        Instantiate(Coin, LootPosition.transform.position, transform.rotation);
        Instantiate(Coin, LootPosition.transform.position, transform.rotation);
        Instantiate(Boom, LootPosition.transform.position, transform.rotation);
        Instantiate(Cocktail, LootPosition.transform.position, transform.rotation);
    }

    protected void DamageAnim()
    {
        if (health < 10)
        {
            enemyanim.SetBool("IsDamaged", true);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

