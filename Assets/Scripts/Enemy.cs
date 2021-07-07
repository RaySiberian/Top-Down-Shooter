using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private Animator anim;
    [SerializeField] private string deathTriggerName;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bulletPrefabEnemy;
    [SerializeField] private Transform bulletSpawnPosition;
    
    private bool dead;
    private float shotDelay = 0.5f;
    private bool startedShot;
    private void Update()
    {
        Rotate();
        if (!startedShot && Time.time > shotDelay)
        {
            StartCoroutine(nameof(ShootingCoroutine));
            startedShot = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Time.time);
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
        StopAllCoroutines();
        anim.SetTrigger(deathTriggerName);
        GetComponent<CircleCollider2D>().enabled = false;
        dead = true;
    }

    private void Rotate()
    {
        if (!dead)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 directionInvert = -(playerPosition - transform.position);
            transform.up = directionInvert;
        }
    }

    private void Shot()
    {
        Instantiate(bulletPrefabEnemy, bulletSpawnPosition.position, transform.rotation);
    }

    private IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            Shot();
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(1f);
        }
    }
}
