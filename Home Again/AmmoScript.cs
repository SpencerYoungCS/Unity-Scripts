using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour {
    Text text;
    PlayerCharacter player;
    CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = player.getAmmo().ToString();
        if(player.getAmmo() == 5)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = 1;
        }
	}
}
