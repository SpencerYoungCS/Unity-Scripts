using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy2 animator script

public class Enemy2AnimScript : MonoBehaviour
{

    Animator _animator;
    Animation _animation;
    // Use this for initialization
    NavMeshAgent _navMeshAgent;
    EnemyAI2 enemyai;
    NPC enemy;

    // Use this for initialization
    void Start()
    {

        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        enemyai = GetComponent<EnemyAI2>();
        enemy = GetComponent<NPC>();
        //for fluidity
        _navMeshAgent.stoppingDistance = 0.3f;

    }

    // Update is called once per frame
    void Update()
    {



        _animator.SetFloat("speed", _navMeshAgent.speed);
        _animator.SetInteger("health", enemy.health);
        _animator.SetBool("isAlive", enemy.isAlive);
        _animator.SetBool("isAttacking", enemy.isAttacking);
        _animator.SetBool("isStopped", _navMeshAgent.isStopped);
        _animator.SetBool("isScreaming", enemyai.isScreaming);
        if(enemy.health < 60)
        {
            _animator.SetBool("isCrawling", true);
        }
        else
        {
            _animator.SetBool("isCrawling", false);
        }



//        _animator.SetBool("isFalling", _navMeshAgent.isOnOffMeshLink);





    }
}
