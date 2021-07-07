using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable otherGO = other.gameObject.GetComponent<IDamageable>();
        otherGO?.GetDamage(damage);
        Destroy(gameObject);
    }
}
