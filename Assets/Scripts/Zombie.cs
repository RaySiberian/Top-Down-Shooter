using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveRadius = 10;
    [SerializeField] private float attackRadius = 3;
    
    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string attackTriggerName;
    
    private Player player;
    private Transform playerTransform;
    private Transform cacheTransform;
    private ZombieMovement zombieMovement;
    private float lastAttackTime;
    
    private void Awake()
    {
        cacheTransform = transform;
        zombieMovement = GetComponent<ZombieMovement>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Update()
    {
        var playerPos = playerTransform.transform.position;
        var distance = Vector3.Distance(playerPos, cacheTransform.position);

        if (distance > moveRadius)
        {
            //Idle
            zombieMovement.enabled = false;
        }   
        else if (distance < attackRadius)
        {
            //Attack
            Attack();
            zombieMovement.enabled = false;
        }
        else
        {
            //Move
            Move();
            Debug.Log($"Move");
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime > attackRate) 
        {
            animator.SetTrigger(attackTriggerName);
            player.GetDamage(damage);
            lastAttackTime = Time.time;
        }
        
    }

    private void Move()
    {
        zombieMovement.enabled = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,0.13f,0),moveRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,0.13f,0),attackRadius);
    }
    
}
