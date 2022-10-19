using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.position, distance, whatIsSolid);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.CompareTag("SimpleRobot"))
            {
                collision.GetComponent<SimpleRobot>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Zombie"))
            {
                collision.GetComponent<Zombie>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.CompareTag("CircleRobot"))
            {
                collision.GetComponent<CircleRobot>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (collision.CompareTag("LaserRobot"))
            {
                collision.GetComponent<LaserRobot>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}