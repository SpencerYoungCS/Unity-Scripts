using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour, ITakeDamage
{

    private NPC enemy;
	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<NPC>();
	}
    public void TakeDamage(int damage)
    {
        enemy.TakeDamage(50);
    }
}
