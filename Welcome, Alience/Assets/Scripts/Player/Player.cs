using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Main Parametrs")]
    public float speed;
    public float health;
    public float Mana;
    public float Shield;
    public float RecoveryRate = 1f;
    public float NextRecoveryTime;
    public float NextDashTime;
    public float DashRate;
    public float G_Radius;
    public int level;
    public float speedOfBoom;
    private Vector2 moveInputJoystick;
    private Vector2 moveVelocityJoystick;

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
    [SerializeField] private Joystick joystickRun;

    [Header("Bonus")]
    public float Coins = 0;
    public float Booms = 0;
    public float Cocktails = 0;

    [Header("Animation")]
    public Animator animator;

    private bool facingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    [SerializeField] private GameObject Rifle;
    [SerializeField] private GameObject GunPrefab;
    [SerializeField] private GameObject Katana;
    [SerializeField] private GameObject PP;
    [SerializeField] private GameObject Minigun;
    [SerializeField] private GameObject RocketGun;
    [SerializeField] private GameObject LaserGun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
        Death();
        ChangeBars();
    }

    private void Move()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInputJoystick = new Vector2(joystickRun.Horizontal, joystickRun.Vertical);
        moveVelocity = moveInput.normalized * speed;
        moveVelocityJoystick = moveInputJoystick * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + moveVelocityJoystick * Time.fixedDeltaTime);

        if ((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0))
        {
            animator.SetBool("IsMoving", false);
            /*animator.SetBool("Dash", false);*/
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }
    
    private void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(1);
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
        if (Loot.CompareTag("Boom"))
        {
            Booms++;
            Destroy(Loot.gameObject);
            BoomsText.text = Booms.ToString();
        }
        if (Loot.CompareTag("Healer"))
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
        if (Loot.CompareTag("Cocktail"))
        {
            Cocktails++;
            CocktailsText.text = Cocktails.ToString();
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("Shield"))
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
        if(Loot.CompareTag("Rifle"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(Rifle);
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("Katana"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(Katana);
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("PP"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(PP);
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("RocketGun"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(RocketGun);
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("Minigun"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(Minigun);
            Destroy(Loot.gameObject);
        }
        if (Loot.CompareTag("LaserGun"))
        {
            this.gameObject.GetComponent<WeaponSwitching>().AddWeapon(LaserGun);
            Destroy(Loot.gameObject);
        }
    }

    public void ChangeBars()
    {
        HealthBar.fillAmount = health / 20;
        HealthText.text = health + "/20";
        ShieldBar.fillAmount = Shield / 20;
        ShieldText.text = Shield + "/20";
    }

    private void Flip()
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, G_Radius);
    }
}
