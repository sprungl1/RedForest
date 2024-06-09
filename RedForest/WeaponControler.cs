using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponControler : MonoBehaviour
{
    public GameObject Sword;

    public bool CanAttack = true;
    public float AttackCooldown = 0.75f;
    public AudioClip SwordAttackSound;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
           
            if (CanAttack)
            {
                SwordAttack();
                
            }
        }
    }

    public void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwordAttackSound);
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

}
