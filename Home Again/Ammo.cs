using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for ammo getting picked up
public class Ammo : MonoBehaviour
{
    private GameObject player;
    public int ammoAmt;

    //i have to stop it from naming itself clone in order for me to pick it up. (checks by name)
    public void Awake()
    {
        gameObject.name = "Ammo";
    }

    public void PickedUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCharacter>().foundAmmo(ammoAmt);
        Destroy(this.gameObject);
    }
}

