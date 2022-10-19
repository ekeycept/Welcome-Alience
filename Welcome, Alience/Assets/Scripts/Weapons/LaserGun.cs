using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform ShotPoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics2D.Raycast(m_transform.position, transform.right))
            {
                /*RaycastHit2D _hit = Physics2D.Raycast(ShotPoint.position, mousePos);*/
                Draw2DRay(ShotPoint.position, mousePos);
            }
            else
            {
                Draw2DRay(ShotPoint.position, mousePos * defDistanceRay);
            }
        }
        else
        {
            Draw2DRay(ShotPoint.position, ShotPoint.position);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

}
