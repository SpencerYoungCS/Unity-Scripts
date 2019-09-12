using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredPulsingOverlay : MonoBehaviour {

    PlayerCharacter player;
    CanvasGroup canvasGroup;
    private float health;
    private float constant;
    private float slope;
    private bool keepGoing;


    // Grow parameters
    public float approachSpeed = 0.05f;
    public float growthBound = 0.3f;
    public float shrinkBound = 0.1f;
    private float currentRatio = 0;

    private bool pulsing;

    // Use this for initialization
    void Start () {
        keepGoing = true;
        canvasGroup = GetComponent<CanvasGroup>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        constant = (float)0.6;
        slope = (float)0.004;
        growthBound = 0.6f;
        pulsing = false;
	}

	
	// Update is called once per frame
	void Update () {
        health = player.getHealth();
        growthBound = ((-1 * (slope * health)) + constant);
        shrinkBound = 0.1f;

        if (health < 30)
        {
            if (!pulsing)
            {
                pulsing = true;
                StartCoroutine(Pulse());
            }
        }
        else
        {
            canvasGroup.alpha = 0;
        }
        //canvasGroup.alpha = ((-1*(slope*health)) + constant);
		
	}

    public IEnumerator Pulse()
    {
        while (keepGoing)
        {
            if(health > 30)
            {
                pulsing = false;
                yield break;
            }
            // Get bigger for a few seconds
            while (currentRatio < growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);
                canvasGroup.alpha = currentRatio;
            yield return new WaitForSeconds(0.1f);
            }

            //if player dies, keep it at the brightest red
            if (health == 0)
            {
                pulsing = false;
                yield break;
            }

            // Shrink for a few seconds
            while (currentRatio > shrinkBound)
            {
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);
                canvasGroup.alpha = currentRatio;
            yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
