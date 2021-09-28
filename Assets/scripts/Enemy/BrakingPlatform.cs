using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrakingPlatform : MonoBehaviour
{
    public int health;

  

    public void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}