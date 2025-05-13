using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fists;
    public Camera cam;
    public AudioSource source;
    public AudioClip audio;
    public Animator animator;
    public LayerMask attackLayer;
    public GameObject hitEffect;
    public DurabilityBar durabilityBar;

    bool attacking = false;
    bool readyToAttack = true;

    public float attackRange = 3;
    public float knockback = 0;

    int attackCount;
    public int damage;
    public float currentDurability;
    public float maxDurability;
    
    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability; 
        durabilityBar.SetMaxDur(maxDurability);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        source.Play();
        
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;

        if (currentDurability <= 0)
        {
            gameObject.SetActive(false);
            durabilityBar.gameObject.SetActive(false);
            fists.SetActive(true);
        }
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackRange, attackLayer))
        {
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<Enemy>(out Enemy T))
            {
                T.TakeDamage(damage);
            }

            if (hit.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(cam.transform.forward * knockback, ForceMode.Impulse);
            }
        }
    }

    void HitTarget(Vector3 pos)
    {
        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        AudioSource sound = GO.transform.Find("Hit Sound").GetComponent<AudioSource>();
        sound.pitch = Random.Range(0.9f, 1.1f);
        Destroy(GO, 20);

        currentDurability--;
        durabilityBar.SetDur(currentDurability);
    }

    public void ResetDurability()
    {
        currentDurability = maxDurability;
        durabilityBar.SetDur(currentDurability);
    }
}
