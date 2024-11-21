using UnityEngine;

public class TD_Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    [Header("Attributes")]
    [SerializeField] private float _bulletSpeed = 5f;

    private Transform _target;

    private void Start()
    {
        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.position - transform.position).normalized;  

        _rb.linearVelocity = direction * _bulletSpeed;
    }


    public void SetTarget(Transform target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
