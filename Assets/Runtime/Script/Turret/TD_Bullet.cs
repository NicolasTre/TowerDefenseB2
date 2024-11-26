using System.Collections;
using UnityEngine;

public class TD_Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    [Header("Attributes")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private int _bulletDamage = 2;

    private Transform _target;

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void Update()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }

    private void MoveBullet()
    {
        if (!_target)
        {
            return;
        }

        Vector2 direction = (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _bulletSpeed;
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<TD_Enemy>().TakeDamage(_bulletDamage);
        Destroy(gameObject);
    }

    private IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
