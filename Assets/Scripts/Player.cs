using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable 
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Animator animator;
    [SerializeField] private string shotTriggerName;
    [SerializeField] private float health; 
    
    private float lastShotTime;
    private float fireRate = 0.3f;
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && (lastShotTime + fireRate) < Time.time)
        {
            lastShotTime = Time.time;
            PlayShotAnimation();
            Instantiate(bulletPrefab, spawnPosition.position, transform.rotation);
        }
    }

    private void PlayShotAnimation()
    {
        animator.SetTrigger(shotTriggerName);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Med"))
        {
            // Тут должна хилить аптечка, а не персонаж
            health += 2;
            Destroy(other.gameObject);
        }
    }

    public void GetDamage(float incomeDamage)
    {
        health -= incomeDamage;
        if (health <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        Debug.Log("ВМЭР НАХОЙ");
    }
}
