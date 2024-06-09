using UnityEngine;

public class WanderAndFlee : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float wanderRadius = 10f; // The radius within which the NPC will wander
    public float fleeDistance = 5f; // The distance at which the NPC will start fleeing
    public float fleeSpeed = 5f; // The speed at which the NPC will flee
    public float wanderSpeed = 2f; // The speed at which the NPC will wander
    public float wanderWaitTime = 5f; // The time the NPC will wait before changing direction
    public float fleeWaitTime = 5f; // The time the NPC will wait before resuming wandering
    public int maxHealth = 100; // The maximum health of the NPC
    public bool dead;

    private Vector3 wanderTarget;
    private bool isFleeing = false;
    private float waitTimer;
    private int currentHealth; // Current health of the NPC

    void Start()
    {
        dead = false;
        // Initialize the wander target to a random position within the wander radius
        wanderTarget = Random.insideUnitSphere * wanderRadius;
        wanderTarget += transform.position;

        // Initialize the NPC's health
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            // If the NPC's health is 0 or less, stop moving
            return;
        }

        if (isFleeing)
        {
            // Flee from the player
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            transform.position += fleeDirection * fleeSpeed * Time.deltaTime;

            // Check if the NPC is safe to resume wandering
            if (Vector3.Distance(transform.position, player.position) > fleeDistance)
            {
                isFleeing = false;
                waitTimer = Time.time + fleeWaitTime;
            }
        }
        else
        {
            // Wander around
            if (Time.time > waitTimer)
            {
                // Change wander target
                wanderTarget = Random.insideUnitSphere * wanderRadius;
                wanderTarget += transform.position;
                waitTimer = Time.time + wanderWaitTime;
            }

            // Move towards the wander target
            transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

            // Check if the player is too close
            if (Vector3.Distance(transform.position, player.position) < fleeDistance)
            {
                isFleeing = true;
            }
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dead = true;
            // NPC is dead, stop moving
            currentHealth = 0;
            // Optionally, you can also disable the NPC's collider or renderer here
        }
    }
}
