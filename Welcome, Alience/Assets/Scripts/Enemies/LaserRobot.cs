using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRobot : Enemy
{
    private LineRenderer m_lineRenderer;
    Transform m_transform;
    private float defDistanceRay = 10f;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    public override void Attack()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= ShootingRange)
        {
            if (Physics2D.Raycast(m_transform.position, transform.right))
            {
                /*RaycastHit2D _hit = Physics2D.Raycast(ShotPoint.position, mousePos);*/
                Draw2DRay(ShootPosition.transform.position, player.position);
            }
            else
            {
                Draw2DRay(ShootPosition.transform.position, player.position * defDistanceRay);
            }
        }
        else
        {
            Draw2DRay(ShootPosition.transform.position, ShootPosition.transform.position);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}

