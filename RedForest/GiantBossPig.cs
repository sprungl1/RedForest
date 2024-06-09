using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GiantBossPig : MonoBehaviour
{
    public GameObject laserPrefab; // The prefab for the laser projectile
    public Transform leftEyeTransform; // The transform of the left eye
    public Transform rightEyeTransform; // The transform of the right eye
    public float laserCooldown = 2f; // Cooldown between each laser shot
    public int maxHealth = 100; // Maximum health of the boss pig
    public GameObject deathEffect; // Particle effect for death
    public Animator animator; // Animator for death animation

    private int currentHealth; // Current health of the boss pig
    private float laserTimer; // Timer for laser cooldown
    private bool isDead; // Flag to check if the boss pig is dead

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (laserTimer <= 0)
            {
                ShootLaser();
                laserTimer = laserCooldown;
            }
            else
            {
                laserTimer -= Time.deltaTime;
            }
        }
    }

    private void ShootLaser()
    {
        Instantiate(laserPrefab, leftEyeTransform.position, leftEyeTransform.rotation);
        Instantiate(laserPrefab, rightEyeTransform.position, rightEyeTransform.rotation);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        // Play death animation
        animator.SetTrigger("Die");
        // Instantiate death effect
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        // Disable collider to prevent further interactions
        GetComponent<Collider>().enabled = false;
        // Destroy the boss pig after a delay to allow for death animation
        Destroy(gameObject, 2f);
        StartCoroutine(MoveToDeathScreen());

    }
    IEnumerator MoveToDeathScreen()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        SceneManager.LoadScene(3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // Destroy the bullet
            TakeDamage(10); // Reduce health by 10 when hit by a bullet
        }
    }
}
