using UnityEngine;

public class TD_Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip _audioDeath;

    [Header("Attributes")]
    [SerializeField] private float _hitPoints;
    public int _currencyWorth;
    public int CurrencyWorth => _currencyWorth;
    private bool _isDestroyed = false;

    public void TakeDamage(float damage) // function call when a bullet hit a enemy
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0 && !_isDestroyed)
        {
            TD_EnnemySpawner._onEnemyDestroy.Invoke();
            TD_LevelManager.main.IncreaseCurrency(_currencyWorth);
            TD_EnemyManager.Instance.RemoveEnemy(gameObject);
            _isDestroyed = true;
            TD_AudioManager.instance.PlayClipAt(_audioDeath, transform.position);
            Destroy(gameObject);
            //StartCoroutine(DestroyParticles());
        }
    }

    public void AddHitPoints(float amount)// function to add Health when a waves start 
    {
        _hitPoints += amount;
    }
}
