using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player animator script

public class PlayerAnimScript : MonoBehaviour
{
    PlayerCharacter player;
    Animator _animator;
    FPSInput playerFPSInput;
    RayShooter playerRayShooter;
    //RayShooter playerRayshooter;

    // Use this for initialization
    void Start()
    {
        playerRayShooter = GetComponentInChildren<RayShooter>();
        player = GetComponent<PlayerCharacter>();
        _animator = GetComponent<Animator>();
        playerFPSInput = GetComponent<FPSInput>();
     //   playerRayshooter = GetComponentInChildren<RayShooter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.isAlive)
        {

            _animator.SetBool("isLookingAtObject", playerRayShooter.isLookingAtObject);
            _animator.SetBool("isAlive", true);

            if (playerFPSInput.movement.z != 0)
                _animator.SetBool("isWalking", true);
            else
                _animator.SetBool("isWalking", false);

                _animator.SetBool("isWalkingBackwards", playerFPSInput.isWalkingBackwards);


            if (playerFPSInput.speed > 3)
                _animator.SetBool("isRunning", true);
            else
               _animator.SetBool("isRunning", false);

            _animator.SetBool("isCrouching", playerFPSInput.isCrouching);

        }
        else
        {
            _animator.SetBool("isAlive", false);
        }
        //if (playerRayshooter.bulletFire[2].isPlaying)
          //  _animator.SetBool("fire", true);
        //else
         //   _animator.SetBool("fire", false);


    }
}
