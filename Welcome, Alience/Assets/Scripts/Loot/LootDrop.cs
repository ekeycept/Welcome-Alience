using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootDrop : MonoBehaviour
{
    Vector3 newPosition;

    public float G_Radius;


    // Start is called before the first frame update
    void Start()
    {

        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        newPosition = new Vector3(transform.position.x + randX, transform.position.y + randY, 0);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnDrawGizmosSelected() // строит сферу заданным радиусом и цветом
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, G_Radius);
    }
}
