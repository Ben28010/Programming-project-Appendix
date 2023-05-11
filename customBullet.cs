using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customBullet : MonoBehaviour
{
    //references
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //damage
    public int explosionDamage;
    public float explosionRange;

    //stats
    public float bounciness;
    public bool useGravity;

    //lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_material;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions)
        {
            Explode(); //calls explode function if the collisions have passed the allowed max
        }

        maxLifetime = Time.deltaTime; //counts down the maxLifetime
        if (maxLifetime <= 0)
        {
            Explode(); //calls explode function when the lifetime reaches 0
        }
    }

    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity); //checks if the explosion doesnt exist, if so creates a new one

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies); //defines new array for the enemies with the layer whatIsEnemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().TakeDamage(explosionDamage); //calls the takedamage function in the enemy that the players projectile has hit
        }

        Invoke("Delay", 0.05f); //starts delay function with a specific delay
    }

    private void Delay()
    {
        Destroy(gameObject); //starts delay function with a specific delay
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++; //increases collisions by 1 every time this function is called

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode(); //if the collider tag is enemy the projectile explodes
    }

    private void Setup()
    {
        physics_material = new PhysicMaterial(); //creates new physics material
        physics_material.bounciness = bounciness; //sets the bounciness of the physics material
        physics_material.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_material.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_material; //gets the component material from the sphere colider and sets it to the new physics material
        rb.useGravity = useGravity; //ensures the rigidbody is using gravity
    }
}
