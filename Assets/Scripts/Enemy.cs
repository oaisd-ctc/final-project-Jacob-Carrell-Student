using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float aggroRange = 10f;
    public float attackRadius = 1f;
    public Vector3 attackOffset;

    public LayerMask mask;

    public GameObject player;
    public Animator animator;
    NavMeshAgent agent;
    float timePassed;
    float newDestinationCD = 0.5f;
    public GameObject ragdoll;
    public AudioSource audioSource;

    public int currentHealth;
    public int maxHealth;
    public float ragdollForce = 120f;
    public int damage = 25;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (timePassed >= attackCD && Vector3.Distance(player.transform.position, transform.position) <= attackRange)
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
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);
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

    void DealDamage()
    {
        if (Physics.CheckSphere(transform.position + transform.rotation * attackOffset, attackRadius, mask))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);

        }
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
        GameObject instantatedRagdoll = Instantiate(ragdoll, transform.position, transform.rotation);
        instantatedRagdoll.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * ragdollForce, ForceMode.Impulse);

        //Camera.main.transform.forward
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.rotation * attackOffset, attackRadius);
    }

    void PlayStepSound()
    {
        audioSource.Play();
    }
}
