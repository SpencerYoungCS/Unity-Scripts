using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    GameObject gameController;

    public void Start()
    {
        gameController = GameObject.Find("GameController");
        
    }

    public void buy()
    {
        gameController.GetComponent<GameController>().Buy(gameObject.name);

    }

}
