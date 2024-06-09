using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        // Add death animation or sound here

        // Disable NavMeshAgent component
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        // Disable Rigidbody component if present
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Remove any velocity
            rb.angularVelocity = Vector3.zero; // Remove any angular velocity
            rb.isKinematic = true; // Make it kinematic so it doesn't move due to physics
        }

        // Disable this script
        enabled = false;
    }


}
