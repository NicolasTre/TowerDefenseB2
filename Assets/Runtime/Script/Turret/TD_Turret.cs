using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _upgradeSound;


    [Header("Attributes")]
    [SerializeField] private float _targetInRange;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _bulletPerSecond = 1f;
    [SerializeField] private int _baseUpgradeCost = 50;

    private float _bpsBase;
    private float _targetingRangeBase;
    private Transform _currentTargetForRotation;

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

        if (_targetTransform == null || !CheckTargetIsInRange())
        {
            FindTarget();
            return;
        }
        else
        {
            if (_targetTransform != null && IsRotationAligned())
            {
                _timeUntilFire += Time.deltaTime;

                if (_timeUntilFire >= 1f / _bulletPerSecond)
                {
                    Shoot();
                    _timeUntilFire = 0f;
                }
            }
        }
    }

    private void Shoot()
    {
        TD_AudioManager.instance.PlayClipAt(_shootSound, transform.position);
        FindTarget();
        GameObject bullet = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
        TD_Bullet bulletScript = bullet.GetComponent<TD_Bullet>();
        bulletScript.SetTarget(_targetTransform.gameObject);
    }

    private void RotateTowardsTarget()
    {
        if (_currentTargetForRotation == null || _currentTargetForRotation != _targetTransform)
        {
            _currentTargetForRotation = _targetTransform;
        }

        Vector3 direction = _currentTargetForRotation.position - _turretRotationPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        _turretRotationPoint.rotation = Quaternion.RotateTowards(
            _turretRotationPoint.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }

    private bool IsRotationAligned()
    {
        if (_targetTransform == null) return false;

        Vector3 direction = _targetTransform.position - _turretRotationPoint.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(_turretRotationPoint.eulerAngles.z, targetAngle));

        return angleDifference < 5f;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetInRange, Vector2.zero, 0f, _enemiesMask);

        if (hits.Length > 0)
        {
            // Trier les cibles par distance
            _targetTransform = hits
                .OrderBy(hit => Vector2.Distance(hit.transform.position, transform.position))
                .First().transform;
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

        TD_AudioManager.instance.PlayClipAt(_upgradeSound, transform.position);
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
