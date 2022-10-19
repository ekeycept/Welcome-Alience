using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShooting : MonoBehaviour
{
    GameObject target;
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    Rigidbody2D stoneRB;


    private void Start()
    {
        stoneRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        stoneRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, -moveDir, distance, whatIsSolid);
    }

    public void OnCollisionEnter2D(Collision2D bull)
    {
        if (bull != null)
        {
            if (bull.collider.CompareTag("Player"))
            {
                bull.collider.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if(!bull.collider.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
