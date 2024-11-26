using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using TMPro;

public class TD_Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _turretRotationPoint;
    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private GameObject _upgradeUI;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _costUpgrade;


    [Header("Attributes")]
    [SerializeField] private float _targetInRange;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _bulletPerSecond = 1f;
    [SerializeField] private int _baseUpgradeCost = 50;

    private float _bpsBase;
    private float _targetingRangeBase;


    private Transform _targetTransform;
    private float _timeUntilFire;

    private int level = 1;

    private void Start()
    {
        _bpsBase = _bulletPerSecond;
        _targetingRangeBase = _targetInRange;

        _upgradeButton.onClick.AddListener(Upgrade);
    }

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
            _timeUntilFire += Time.deltaTime;

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

    public void OpenUpgradeUI()
    {
        _upgradeUI.SetActive(true);
        _costUpgrade.text = ("Cost : " + _baseUpgradeCost);
    }
    
    public void CloseUpgradeUI()
    {
        _upgradeUI.SetActive(false);
        TD_UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (_baseUpgradeCost > TD_LevelManager.main.currency) return;

        TD_LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        _bulletPerSecond = CalculateBPS();
        _targetInRange = CalculateRange();

        CloseUpgradeUI();

        _baseUpgradeCost = CalculateCost();
        
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(_baseUpgradeCost * Mathf.Pow(level, 0.8f)); 
    }
    
    private float CalculateBPS() // bullet per second
    {
        return _bpsBase * Mathf.Pow(level, 0.5f); 
    }

    private float CalculateRange()
    {
        return _targetingRangeBase * Mathf.Pow(level, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetInRange); 
    }
}
