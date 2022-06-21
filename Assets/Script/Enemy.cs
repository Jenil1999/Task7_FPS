using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;

    //public Transform player;
    

    public float StartDistance;

    public float StopDistance;

    public float EnemyDamage;

    float LastAttackTime = 0;
    float AttackCoolDown = 2f;

    NavMeshAgent Agent;

    public Animator ZombieAnimation;


    GameObject Target;


    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        ZombieAnimation = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, Target.transform.position);

        if (Distance < StartDistance && Distance > StopDistance)
        {
            ZombieAnimation.SetBool("IsRunning", true);
            MoveTowardPlayer();
        }

        else
        {
            ZombieAnimation.SetBool("IsRunning", false);
            StopEnemy();
        }


        if(Distance<StopDistance)
        {
            if(Time.time - LastAttackTime > AttackCoolDown)
            {
                ZombieAnimation.SetBool("IsAttacking", true);
                LastAttackTime = Time.time;
                Target.GetComponent<PlayerMovement>().TakeDamage(EnemyDamage);
            }   
        }
        else
        {
                ZombieAnimation.SetBool("IsAttacking", false);
        }
    }


   

    void StopEnemy()
    {
        Agent.isStopped = true;
    }


    void MoveTowardPlayer()
    {
        Agent.isStopped = false;
        Agent.SetDestination(Target.transform.position);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            Die();
        }
              
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
