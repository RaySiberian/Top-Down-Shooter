using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private Animator anim;
    [SerializeField] private string deathTriggerName;
    [SerializeField] private Transform player;

    private void Update()
    {
        Rotate();
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
        anim.SetTrigger(deathTriggerName);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Rotate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 directionInvert = -( playerPosition - transform.position);
        transform.up = directionInvert;
    }
}
