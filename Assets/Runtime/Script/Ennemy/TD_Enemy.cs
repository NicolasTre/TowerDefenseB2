using UnityEngine;

public class TD_Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _hitPoints;
    [SerializeField] private int _currencyWorth;

    private bool _isDestroyed = false;

    public void TakeDamage(int damage) // function call when a bullet hit a enemy
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0 && !_isDestroyed)
        {
            TD_EnnemySpawner._onEnemyDestroy.Invoke();
            TD_LevelManager.main.IncreaseCurrency(_currencyWorth);
            _isDestroyed = true;
            Destroy(gameObject);
            //StartCoroutine(DestroyParticles());
        }
    }

    public void AddHitPoints(float amount)// function to add Health when a waves start 
    {
        _hitPoints += amount;
    }

    /*
     private IEnumerator DestroyParticles() // function for destroy the clone particles when a enemy die
    {

        gameObject.layer = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        
        //yield return null;
    }
    */
}
