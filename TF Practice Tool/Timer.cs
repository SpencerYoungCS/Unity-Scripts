using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time.ToString().IndexOf('.') + 2 < 2)
            textMesh.text = Time.time.ToString();
        else
            textMesh.text = Time.time.ToString().Remove(Time.time.ToString().IndexOf('.') + 2);
    }
}
