using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject SquareAttack;


    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    // Update is called once per frame
    private void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.position, distance, whatIsSolid);
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        /*        if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.CompareTag("Enemy"))
                    {
                        hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                        DestroyBullet();
                    }

                }*/

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
/*                collision.GetComponent<Enemy>().TakeDamage(damage);*/
                Instantiate(SquareAttack, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Zombie"))
            {
                collision.GetComponent<Zombie>().TakeDamage(damage);
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
        if (collision != null)
        {
            /*           if (collision.collider.CompareTag("Enemy"))
                       {
                           collision.collider.GetComponent<Enemy>().TakeDamage(damage);
                           Destroy(gameObject);
                       }*/

        }
        Destroy(gameObject);
    }

}
