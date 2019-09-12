using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this rotates the whatever this is attached to (in this case the gun's barrel)

public class RotateBarrel : MonoBehaviour {
   public void Rotate(int degrees)
    {
        StartCoroutine(RotateMe(degrees));
    }

    IEnumerator RotateMe(int degrees)
    {
        for(int i = 0; i < (degrees/3); i++)
        {
            //rotate 1 degree every x seconds.
            transform.Rotate(new Vector3(0, 0, 3));
            yield return new WaitForSeconds(0.01f);
        }
    }
        

}
