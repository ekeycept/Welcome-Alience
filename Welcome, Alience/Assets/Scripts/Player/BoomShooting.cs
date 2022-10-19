using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoomShooting : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BoomAttack(float speedOfBoom)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePos, speedOfBoom * Time.deltaTime);
    }

}
