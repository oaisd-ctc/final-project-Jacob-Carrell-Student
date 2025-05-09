using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public DurabilityBar durabilityBar;
    public AudioSource audioSource;

    public void TakeDamage(int damage)
    {
        health -= damage;
        durabilityBar.SetDur(health);

        audioSource.Play();

        if (health <= 0)
        {
            SceneManager.LoadScene("Death Screen");
        }
    }

    public void EatSteak()
    {
        health = health + 30;
        durabilityBar.SetDur(health);
    }
}
