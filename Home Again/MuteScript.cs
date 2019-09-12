using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteScript : MonoBehaviour
{
    public bool volumeIsOn;

    // Use this for initialization
    void Start()
    {
        volumeIsOn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            volumeIsOn = !volumeIsOn;

            if (volumeIsOn)
                AudioListener.volume = 1;
            else
                AudioListener.volume = 0;
        }
    }
}
