
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public int damage = 8;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform; //sets the player transform
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling(); //starts patrolling when player is not in any range
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); //starts chasing when player is in sight range
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); //starts attacking when player is in both sight and attack range
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint(); //calls function if there is no exisiting walkpoint

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint); //makes the enemy move to the walkpoint
        }
            

        Vector3 distanceToWalkPoint = transform.position - walkPoint; //finds the distance to the walk point

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; //resets walk point
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
            
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); //makes the nemy move to where the player is
    }

    private void AttackPlayer()
    {
        

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position); //makes the enemy stop where it currently is

        transform.LookAt(player); //makes the enemy lock onto the player

        if (!alreadyAttacked)
        {
            /// Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false; 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); //draws the attack range in red
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); //draws the sight range in yellow
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
          
}
