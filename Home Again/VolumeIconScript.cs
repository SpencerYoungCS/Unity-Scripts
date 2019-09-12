using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeIconScript : MonoBehaviour {

    MuteScript muteScript;
    CanvasGroup[] canvasGroups;
	// Use this for initialization
	void Start () {
        muteScript = GameObject.Find("MainVision").GetComponent<MuteScript>();
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (muteScript.volumeIsOn)
        {
            canvasGroups[0].alpha = 1;
            canvasGroups[1].alpha = 0;
        }
        else
        {
            canvasGroups[0].alpha = 0;
            canvasGroups[1].alpha = 1;

        }

		
	}
}
