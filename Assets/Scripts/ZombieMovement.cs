using System;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator zombieAnimator;
    
    private Transform playerTransform;
    private Transform cacheTransform;
    private Rigidbody2D rb;
    private Vector3 direction;

    private void Awake()
    {
        cacheTransform = transform;
    }

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        Vector3 playerPosition = playerTransform.transform.position;
        direction = playerPosition - transform.position;
        cacheTransform.up = direction;
    }

    private void Move()
    {
        rb.velocity = direction * 0.5f;
        SetMoveAnimator(direction.magnitude);
    }

    private void SetMoveAnimator(float magnitude)
    {
        zombieAnimator.SetFloat("Speed",magnitude);
    }
    
    private void OnDisable()
    {
        rb.Sleep();
        SetMoveAnimator(0);
    }

   
}