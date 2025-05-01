using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    public GameObject player;
    public Animator animator;
    NavMeshAgent agent;
    float timePassed;
    float newDestinationCD = 0.5f;
    public GameObject ragdoll;

    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (timePassed >= attackCD)
        {
            animator.SetTrigger("Attack");
            timePassed = 0;
        }

        timePassed += Time.deltaTime;  

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }

        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("Damage");

        if (currentHealth < 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
        Instantiate(ragdoll, transform.position, transform.rotation);
        //ragdoll.AddForce(Impulse);
    }
}
