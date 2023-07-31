using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private int damage;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, damageRadius, damageableLayer);

        if (col == null)
            return;

        col.GetComponent<Damageable>().Damage(damage);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}