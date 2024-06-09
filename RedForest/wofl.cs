using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public bool robbed;
    public float wanderSpeed = 2f;
    public float sprintSpeed = 10f;
    public float wanderRadius = 10f;
    public float stunDuration = 2f;
    public GameObject player;
    public PickupItem pickupItem; // Reference to the player's holding status
    public bool isStunned = false;
    public bool isSprinting;
    private Vector3 wanderTarget;
   

    void Start()
    {
        robbed = false;
        PickNewWanderTarget();
    }

    void Update()
    {
       
        if (pickupItem.HasItem)
        {
            SprintToPlayer();
        }
        else
        {
            Wander();
        }
    }

    void PickNewWanderTarget()
    {
        Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
        randomPoint += transform.position;
        randomPoint.y = transform.position.y; // Keep the same height

        wanderTarget = randomPoint;
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, wanderTarget) < 1f)
        {
            PickNewWanderTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);
    }

    void SprintToPlayer()
    {
        isSprinting = true;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, sprintSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            StartCoroutine(StunPlayer());
        }
    }

    IEnumerator StunPlayer()
    {
        robbed = true;
        isStunned = true;
        player.GetComponent<FPSController>().Stun(stunDuration);
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        pickupItem.HasItem = false;
        pickupItem.pigPickUp.SetActive(false);
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
