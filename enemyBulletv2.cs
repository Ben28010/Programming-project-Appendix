using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletv2 : MonoBehaviour
{
    public GameObject explosion;
    public float range = 3f;
    public int damageAmount = 8;
    PlayerStats stats;
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(explosion, transform.position, Quaternion.identity); //instantiates a new game object
        Destroy(impact, 2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range); //creates an array for overlapping colliders
        foreach (Collider nearbyObject in colliders)
        {
            if(nearbyObject.tag == "User")
            {
                stats.TakeDamage(damageAmount); //calls take damage function
            }
        }
        Destroy(gameObject); //destroys the projectile
    }
}
