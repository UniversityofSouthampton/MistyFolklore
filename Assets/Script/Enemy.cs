using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Rigidbody[] limpsRigidbodies;
    private Collider[] limpsColliders;
    //public GameObject hips;
    public GameObject thisguy;
    private BoxCollider bigCollider;
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    // Attacking
    public float TimeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject arrow;
    public Transform spawnPoint;
    public float shootForce , upForce;
    //States
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;
    bool dead = false;

    public Animator thisguyAnimator;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bigCollider = GetComponent<BoxCollider>();
        GetRagdollBits();
        RagdollModeOff();
    }
    // Update is called once per frame
    void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (dead == false)
        {
           if (!playerInAttackRange)
           {
               ChasePlayer();
           }
           else {
               AttackPlayer();
           }
            
        }
       
       
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if (PlayerController.timeChange == true)
        {
            transform.LookAt(player);
        }
        
        if (!alreadyAttacked)
        {
            // Attack code
            Rigidbody rb_arrow = Instantiate(arrow, spawnPoint.position, spawnPoint.rotation).GetComponent<Rigidbody>();
            rb_arrow.AddForce(transform.forward * shootForce * 1/Time.timeScale, ForceMode.Impulse);
            rb_arrow.AddForce(transform.up * upForce * 1/Time.timeScale, ForceMode.Impulse);

            alreadyAttacked = true;
            StartCoroutine(ResetAttack());
        }

    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds (TimeBetweenAttacks);
        alreadyAttacked = false;
    }

    void GetRagdollBits()
    {
        limpsRigidbodies = thisguy.GetComponentsInChildren<Rigidbody>();
        limpsColliders = thisguy.GetComponentsInChildren<Collider>();
    }

    public void RagdollModeOff()
    {
        foreach (var rigid in limpsRigidbodies)
        {
            rigid.isKinematic = true;
        }

        foreach (var col in limpsColliders)
        {
            col.enabled = false;
        }

        thisguyAnimator.enabled = true;
        bigCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void RagdollModeOn()
    {
        foreach (var rigid in limpsRigidbodies)
        {
            rigid.isKinematic = false;
        }

        foreach (var col in limpsColliders)
        {
            col.enabled = true;
        }

        thisguyAnimator.enabled = false;
        bigCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        dead = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BluntObj" || other.gameObject.tag == "Enemy")
        {
            RagdollModeOn();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Arrow")
        {
            other.gameObject.transform.SetParent(this.gameObject.transform);
            RagdollModeOn();
        }
    }
  

    public Rigidbody GetHitRigid(Vector3 hitpoint)
    {
        Rigidbody hitRigidbody = limpsRigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitpoint)).First();
        return hitRigidbody;
    }
   
}
