using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 20;
    private GameObject fireball;

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.tag != "Projectile")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();

            if (player != null)
            {
                player.Hurt(damage);
            }

            Destroy(this.gameObject);
        }
    }
}
