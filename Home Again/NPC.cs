using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//script that holds npc stats that all npcs have

public class NPC : MonoBehaviour, ITakeDamage
{
    //statuses
    [System.NonSerialized] public bool isAlive;
    [System.NonSerialized] public bool isAttacking;
    [System.NonSerialized] public bool isNear;
    [System.NonSerialized] public bool isVeryNear;
    [System.NonSerialized] public bool isPlayingJumpscare;

    //stats
    public int health;
    //type determines which kind of AI it is
    [System.NonSerialized] public int type;
    public int aggroRange;

    // Use this for initialization
    void Start()
    {
        isVeryNear = false;
        isNear = false;
        health = 100; 
        isAlive = true;
        isAttacking = false;
        aggroRange = 15;
    }

    void Update()
    {
        if (health > 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            StartCoroutine(Die());
        }
    }

    //public void Hit(int dmg)
   // {
    //    health -= dmg;
    //}

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
