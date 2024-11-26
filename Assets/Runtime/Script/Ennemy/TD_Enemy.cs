using UnityEngine;

public class TD_Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _hitPoints;
    [SerializeField] private int _currencyWorth;

    private bool _isDestroyed = false;

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0 && !_isDestroyed)
        {
            TD_EnnemySpawner._onEnemyDestroy.Invoke();
            TD_LevelManager.main.IncreaseCurrency(_currencyWorth);
            _isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void AddHitPoints(float amount)
    {
        _hitPoints += amount;
    }
}