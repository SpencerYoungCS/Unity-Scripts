using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScripts : MonoBehaviour {

    private CanvasGroup canvasGroup;
    private Text text;
	// Use this for initialization
	void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponent<Text>();
        StartCoroutine(DelayedMessage());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DelayedMessage()
    {
        text.text = "You Escaped...";
        yield return new WaitForSeconds(6f);
        for(float i = 0; i < 1; i = i + .02f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3f);
        for(float i = 1; i > 0; i = i - .02f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForEndOfFrame();
        }

        text.text = "For now.";
        for(float i = 0; i < 1; i = i + .02f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForEndOfFrame();
        }
         yield return new WaitForSeconds(3f);
        for(float i = 1; i > 0; i = i - .02f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForEndOfFrame();
        }

         yield return new WaitForSeconds(3f);
        text.text = "The End.";
        for(float i = 0; i < 1; i = i + .02f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForEndOfFrame();
        }

    }
}
