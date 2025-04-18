using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int currentHealth;
    public int health;

    private void Awake()
    {
        currentHealth = health;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;    

        if (currentHealth < 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
