using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health;

    public void Start()
    {
      

    }

    public void Update()
    {
  
        if(health <= 0)
        {
            Destroy(gameObject);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.GetDamage();
           
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
