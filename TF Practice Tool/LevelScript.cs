using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScript : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Level: " + 1;
    }

    // Update is called once per frame
    public void SetLevel()
    {
        textMesh.text = "Level: " + gameController.level;
    }
}
