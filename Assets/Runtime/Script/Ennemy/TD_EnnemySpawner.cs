using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TD_EnnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int _baseEnemies = 8;
    [SerializeField] private float _enemiesPerSecond = 0.5f;
    [SerializeField] private float _enemiesPerSecondCap = 15f;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _hitPointIncreasePerWave = 0.5f;
    [SerializeField] private float _difficultyScalingFactor = 0.75f;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private Animator _animCurrentWaves;

    [Header("Events")]
    public static UnityEvent _onEnemyDestroy = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
    private float eps; // enemy per second
    private bool _isSpawning = false;

    private void Awake()
    {
        _onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!_isSpawning)
        {
            return;
        }
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= (1f / eps) && _enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            _enemiesLeftToSpawn--;
            _enemiesAlive++;
            _timeSinceLastSpawn = 0f;
        }

        if (_enemiesAlive == 0 && _enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);

        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
        _currentWaveText.text = ("Wave : " + _currentWave);
        _animCurrentWaves.Play("AnimText");

    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, _enemyPrefabs.Length);
        GameObject prefabToSpawn = _enemyPrefabs[index];

        GameObject enemyInstance = Instantiate(prefabToSpawn, TD_LevelManager.main.startPoint.position, Quaternion.identity);

        TD_Enemy enemy = enemyInstance.GetComponent<TD_Enemy>();
        if (enemy != null)
        {
            float bonusHitPoints = _hitPointIncreasePerWave * (_currentWave - 1); 
            enemy.AddHitPoints(bonusHitPoints);
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_currentWave, _difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(_enemiesPerSecond * Mathf.Pow(_currentWave, _difficultyScalingFactor), 0f, _enemiesPerSecondCap);
    }

    private void EnemyDestroyed()
    {
        _enemiesAlive--;
    }

    private void EndWave()
    {
        _isSpawning = false;
        _timeSinceLastSpawn = 0f;
        _currentWave++;
        StartCoroutine(StartWave());
    }
}