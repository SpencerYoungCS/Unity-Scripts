using UnityEngine;
using System.Collections;
//this makes enemy's melee attacks hurt me
public class MeleeHitBox : MonoBehaviour
{
    NPC enemy;
    public int damage = 20;
    private bool invincibilityWindow;

    private void Start()
    {
        invincibilityWindow = false;
        enemy = GetComponentInParent<NPC>();
    }
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (enemy.isAlive)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (!invincibilityWindow)
                {
                    StartCoroutine(HurtPlayer(player));
                }

            }
        }
    }
    IEnumerator HurtPlayer(PlayerCharacter player)
    {
        invincibilityWindow = true;
        if (player != null)
        {
            if (player.getHealth() > 0)
                player.Hurt(damage);
        }
        yield return new WaitForSeconds(0.7f);
        invincibilityWindow = false;
    }
}