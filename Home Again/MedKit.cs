using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for picking up a medkit

public class MedKit : MonoBehaviour {
    private GameObject player;
    public int healAmt;
    public void Awake()
    {
        gameObject.name = "Medkit";
    }

    public void PickedUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCharacter>().Healed(healAmt);
            Destroy(this.gameObject);
    }
}
