using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TD_EnnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int _baseEnemies = 8;
    [SerializeField] private float _enemiesPerSecond = 0.5f;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent _onEnemyDestroy = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
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

        if (_timeSinceLastSpawn >= ( 1f /_enemiesPerSecond) && _enemiesLeftToSpawn > 0)
        {
            Debug.Log("ff");
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
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = _enemyPrefabs[0];
        Instantiate(prefabToSpawn, TD_LevelManager.main._startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_currentWave, _difficultyScalingFactor));   
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
