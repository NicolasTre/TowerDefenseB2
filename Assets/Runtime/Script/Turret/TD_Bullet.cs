using System.Collections;
using UnityEngine;

public class TD_Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    [Header("Attributes")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _bulletDamage = 2;

    private Transform _target;
    private GameObject _enemyTarget;

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
        if (!_enemyTarget)
        {
            return;
        }

        Vector2 direction = (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _bulletSpeed;
    }

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
        _enemyTarget = target;
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
