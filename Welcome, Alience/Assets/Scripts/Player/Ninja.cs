using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ninja : MonoBehaviour
{
    [Header("Main Parametrs")]
    public float speed;
    public float health;
    public float Mana;
    public float Shield;
    public int katana_damage;
    private float NextAttackTime;
    public float NextAttackRate = 1f;
    public float RecoveryRate = 1f;
    public float NextRecoveryTime;
    public float NextDashTime;
    public float DashRate;
    public float G_Radius;
    public int level;
    public float speedOfBoom;
    public float attackRange;

    [Header("Bars")]
    public Image HealthBar;
    public Text HealthText;
    //public Image ManaBar;
    public Image ShieldBar;
    public Text ShieldText;

    [Header("GameObj and Text")]
    public GameObject coin;
    public Text CoinsText;
    public GameObject Healer;
    public GameObject Boom;
    public Text BoomsText;
    public GameObject Cocktail;
    public Text CocktailsText;
    public GameObject Shielder;
    public GameObject Gun;
    public Transform BoomPoint;
    public Transform attackpoint;
    public LayerMask enemyLayers;

    [Header("Bonus")]
    public float Coins = 0;
    public float Booms = 0;
    public float Cocktails = 0;

    [Header("Anim and Sound")]
    public Animator animator;
    public AudioSource KatanaSound;

    private bool facingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        Attack();
        Flipping();
        PlayerDeath();
        BarsChanging();
        BoomShooting();
        ShieldRecovery();
        DashMoving();
        /*Teleport();
          DashMoving();*/
    }

    private void BasicMovement()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("IsMoving", false);
            /*animator.SetBool("Dash", false);*/
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0) && NextAttackTime < Time.time)
        {
            animator.SetTrigger("Attack");
            KatanaSound.Play();
            NextAttackTime = Time.time + NextAttackRate;

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemy)
            {
                if (enemy.CompareTag("Enemy"))
                {
                   enemy.GetComponent<Enemy>().TakeDamage(katana_damage);
                }
                if (enemy.CompareTag("SimpleRobot"))
                {
                    enemy.GetComponent<SimpleRobot>().TakeDamage(katana_damage);
                }
                if (enemy.CompareTag("Zombie"))
                {
                    enemy.GetComponent<Zombie>().TakeDamage(katana_damage);
                }
                if (enemy.CompareTag("CircleRobot"))
                {
                    enemy.GetComponent<CircleRobot>().TakeDamage(katana_damage);
                }
                if (enemy.CompareTag("LaserRobot"))
                {
                    enemy.GetComponent<LaserRobot>().TakeDamage(katana_damage);
                }
            }
        }
    }

    private void PlayerDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
    }

    private void DashMoving()
    {
        if (Input.GetKeyDown("space"))
        {
            float dashdist = 0.5f;
            if (NextDashTime < Time.time)
            {
                rb.position += moveVelocity * dashdist;
                NextDashTime = Time.time + DashRate;
                /*                animator.SetBool("Dash", true);*/
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D Loot)
    {
        if (Loot.CompareTag("Coin"))
        {
            Coins++;
            Destroy(Loot.gameObject);
            CoinsText.text = Coins.ToString();
        }
        else if (Loot.CompareTag("Boom"))
        {
            Booms++;
            Destroy(Loot.gameObject);
            BoomsText.text = Booms.ToString();
        }
        else if (Loot.CompareTag("Healer"))
        {
            if (health < 20)
            {
                health = health + 5;
                Destroy(Loot.gameObject);

                if (health > 20)
                {
                    float healthDifference = health - 20;
                    health -= healthDifference;
                }
            }
        }
        else if (Loot.CompareTag("Cocktail"))
        {
            Cocktails++;
            CocktailsText.text = Cocktails.ToString();
            Destroy(Loot.gameObject);
        }
        else if (Loot.CompareTag("Shield"))
        {
            if (Shield < 20)
            {
                Shield = Shield + 10;
                Destroy(Loot.gameObject);

                if (Shield > 20)
                {
                    float ShieldDifference = Shield - 20;
                    Shield -= ShieldDifference;

                }
            }
        }
    }

    public void BarsChanging()
    {
        HealthBar.fillAmount = health / 20;
        HealthText.text = health + "/20";
        ShieldBar.fillAmount = Shield / 20;
        ShieldText.text = Shield + "/20";
    }

    private void Flipping()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!facingRight && mousePos.x > transform.position.x || facingRight && mousePos.x < transform.position.x)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (Shield > 0)
        {
            Shield -= damage;
        }
        else if (Shield <= 0)
        {
            Shield = 0;
            health -= damage;
        }
    }

    private void ShieldRecovery()
    {
        if (Shield < 0 && NextRecoveryTime < Time.time)
        {
            Shield = 0;
            Shield += 1;
            NextRecoveryTime = Time.time + RecoveryRate;
        }
        else if (Shield > 0 && Shield < 10 && NextRecoveryTime < Time.time)
        {
            Shield += 1;
            NextRecoveryTime = Time.time + RecoveryRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, G_Radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }

    public void SavePlayer()
    {
        /*SaveSystem.SavePlayer(gameObject);*/
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    public void BoomShooting()
    {
        if (Input.GetKeyDown("v"))
        {
            Instantiate(Boom, BoomPoint.position, transform.rotation);
            Boom.GetComponent<BoomShooting>().BoomAttack(speedOfBoom);
        }

    }

    public void Teleport()
    {
        if (Input.GetKeyDown("x"))
        {
            Vector3 tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.position = tp;
        }

    }
}
