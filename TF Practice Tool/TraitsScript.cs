using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraitsScript : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
