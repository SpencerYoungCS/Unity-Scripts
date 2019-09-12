using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    int behaviorType;
    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        //set this location to child's 
    }

    public void setModel(int behaviorType)
    {
        int i = 0;
        foreach (Transform models in transform)
        {
            if (i == behaviorType)
            {
                models.gameObject.SetActive(true);
            }
            else
            {
                models.gameObject.SetActive(false);
            }
            i++;
        }

    }

}
