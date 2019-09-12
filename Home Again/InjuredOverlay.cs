using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredOverlay : MonoBehaviour {

    PlayerCharacter player;
    CanvasGroup canvasGroup;
    //private float health;
	// Use this for initialization
	void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        canvasGroup.alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.getHealth() < 50)
            canvasGroup.alpha = 0.5f;
        else
            canvasGroup.alpha = 0f;
		
	}
}
