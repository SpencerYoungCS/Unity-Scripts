using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy1 animator script

public class Enemy1AnimScript : MonoBehaviour {

    Animator _animator;
    NavMeshAgent _navMeshAgent;
    EnemyAI1 enemyai;
    NPC enemy;

    void Start()
    {
        enemyai = GetComponent<EnemyAI1>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<NPC>();
    }

	
	// Update is called once per frame
	void Update () {

        _animator.SetFloat("speed", _navMeshAgent.speed);
        _animator.SetBool("isAlive", enemy.isAlive);
        _animator.SetBool("isAttacking", enemy.isAttacking);
        _animator.SetBool("isStopped", _navMeshAgent.isStopped);
        _animator.SetBool("isWaiting", enemyai._waiting);
        _animator.SetBool("isFalling", _navMeshAgent.isOnOffMeshLink);
        _animator.SetBool("isLookingAround", enemyai.isLookingAround);
        _animator.SetBool("isHitting", enemyai.isHitting);

	}
}
