using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy 3 bystander animator script

public class Enemy3AnimScript : MonoBehaviour {

    Animator _animator;
    // Use this for initialization
    NavMeshAgent _navMeshAgent;
    NPC enemy;
    //EnemyAI1 enemyai;
	// Use this for initialization
	void Start () {

        enemy = GetComponent<NPC>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {

        _animator.SetFloat("speed", _navMeshAgent.speed);
        _animator.SetBool("isAlive", enemy.isAlive);
		
	}
}
