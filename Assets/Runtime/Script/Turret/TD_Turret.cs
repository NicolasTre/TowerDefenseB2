using UnityEngine;
using UnityEditor;
using System;

public class TD_Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _turretRotationPoint;
    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPoint;


    [Header("Attributes")]
    [SerializeField] private float _targetInRange;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _bulletPerSecond = 1f;


    private Transform _targetTransform;
    private float _timeUntilFire;


    private void Update()
    {
        if (_targetTransform == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            _targetTransform = null;
        }
        else
        {
            _timeUntilFire = Time.deltaTime;

            if(_timeUntilFire >= 1f / _bulletPerSecond)
            {
                Shoot();
                _timeUntilFire = 0f;
            }
        }

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
        TD_Bullet bulletScript = bullet.GetComponent<TD_Bullet>();
        bulletScript.SetTarget(_targetTransform);   
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(_targetTransform.position.y - transform.position.y, _targetTransform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f; 

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        _turretRotationPoint.rotation = Quaternion.RotateTowards(_turretRotationPoint.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetInRange, (Vector2)transform.position, 0f, _enemiesMask);

        if (hits.Length > 0)
        {
            _targetTransform = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(_targetTransform.position, transform.position) <= _targetInRange;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetInRange); 
    }
}
