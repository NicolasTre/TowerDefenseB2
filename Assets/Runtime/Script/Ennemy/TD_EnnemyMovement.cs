using UnityEngine;

public class TD_EnnemyMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D _rb;


    [Header("Attributes")]
    [SerializeField] private float _moveSpeed = 2f;

    private Transform _target;
    private int _pathIndex = 0;

    private float _baseSpeed;

    private void Start()
    {
        _baseSpeed = _moveSpeed;
        _target = TD_LevelManager.main.path[_pathIndex];
    }

    private void Update()
    {
        FollowPath();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy() // method to move enemy
    {
        Vector2 direction = (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _moveSpeed;
    }

    private void FollowPath() // method for switch the path point and set the path point where enemy need to go
    {
        if (Vector2.Distance(_target.position, transform.position) <= 0.1f)
        {
            _pathIndex++;


            if (_pathIndex >= TD_LevelManager.main.path.Length)
            {
                TD_EnnemySpawner._onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                _target = TD_LevelManager.main.path[_pathIndex];
            }
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    public void ResetSpped()
    {
        _moveSpeed = _baseSpeed;
    }
}
