using UnityEngine;
using TMPro;

public class BulletShooting : MonoBehaviour
{
    public ParticleSystem shotgunmuzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public Transform shootingPoint;
    public int maxAmmunition = 6; // Total ammo
    public int magazineSize = 2; // Ammo in magazine
    public int remAmmunition;
    public int loadedAmmo;
    public TextMeshProUGUI rem_ammo; // Changed from GameObject to TextMeshProUGUI
    public TextMeshProUGUI loaded_ammo; // Changed from GameObject to TextMeshProUGUI
    public AudioClip shootsound;
    public AudioClip reloadSound; // Sound for reloading
    public Animator animator; // Animator for reload animation
  

    private void Start()
    {
        remAmmunition = maxAmmunition; // Initialize remaining ammunition
        loadedAmmo = magazineSize;
        UpdateAmmoText();
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0)) // 0 is for left mouse button
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
        {
            Reload();
        }
    }

    void ShootBullet()
    {
        if (loadedAmmo > 0)
        {
            shotgunmuzzle.Play();
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb)) // Adjusted for 3D
            {
                rb.velocity = transform.forward * bulletSpeed; // Adjusted for 3D, using transform.forward
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource != null && shootsound != null)
                {
                    audioSource.PlayOneShot(shootsound);
                }

            }
            else // Fallback in case Rigidbody is not attached to bullet prefab
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody component.");
            }
            loadedAmmo--;
            UpdateAmmoText();
        }
    }

    void Reload()
    {
        if (loadedAmmo < magazineSize && remAmmunition > 0)
        {
            int bulletsToLoad = Mathf.Min(magazineSize - loadedAmmo, remAmmunition);
            loadedAmmo += bulletsToLoad;
            remAmmunition -= bulletsToLoad;
            UpdateAmmoText();

            // Play reload sound
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }

            // Trigger reload animation
            if (animator != null)
            {
                animator.SetTrigger("Reload");
            }
        }
    }

    void UpdateAmmoText()
    {
        rem_ammo.text = remAmmunition.ToString();
        loaded_ammo.text = "/" + loadedAmmo.ToString();
    }
}
