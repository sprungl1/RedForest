using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter(Collider hitInfo) // Note the change here to use OnTriggerEnter for 3D
    {
        WanderAndFlee enemy = hitInfo.GetComponent<WanderAndFlee>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject); // Destroy the bullet after hitting something
    }
}
