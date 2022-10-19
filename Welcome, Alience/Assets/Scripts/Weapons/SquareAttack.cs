using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAttack : MonoBehaviour
{
    public float RadiusAttack;
    private float InvokeTime = 0.5f;
    public int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyRocket", InvokeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, RadiusAttack);

        foreach (Collider2D Enemy in hitEnemies)
        {
            Enemy.GetComponent<Enemy>().TakeDamage(damage);
            Enemy.GetComponent<Zombie>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void DestroyRocket()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusAttack);
    }
}
