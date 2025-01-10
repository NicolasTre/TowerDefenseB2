using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TD_EnemyManager : MonoBehaviour
{
    public static TD_EnemyManager Instance;

    [Header("References")]
    [SerializeField] private AudioClip _boomAudio;
    [SerializeField] private Image image;
    [SerializeField] private AudioClip _errorBombAudio;
    [SerializeField] private TextMeshProUGUI cooldownText;

    [Header("Attributes")]
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private float _bombCooldownDuration = 90f; // Cooldown for the bomb PowerUp


    private bool _isBombCooldownActive = false;

    private void Awake() // check if we don't have two instance of this script
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemy(GameObject enemy) // Add enemy to the list 
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemy) // remove enemy of the list
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    public void DestroyAllEnemies()
    {
        if (_isBombCooldownActive)
        {
            Debug.Log("La bombe est en recharge. Patientez avant de l'utiliser à nouveau.");
            TD_AudioManager.instance.PlayClipAt(_errorBombAudio, transform.position);
            return;
        }

        TD_AudioManager.instance.PlayClipAt(_boomAudio, transform.position);

        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null)
            {
                TD_Enemy enemyScript = enemy.GetComponent<TD_Enemy>();
                if (enemyScript != null)
                {
                    TD_LevelManager.main.IncreaseCurrency(enemyScript.CurrencyWorth);
                    gameObject.GetComponent<TD_EnnemySpawner>()._enemiesAlive--;
                    Destroy(enemy); // Détruit l'ennemi
                }
            }
        }

        _enemies.Clear();

        StartCoroutine(StartCooldown()); // start cooldown for re use
    }

    private IEnumerator StartCooldown()
    {
        image.color = Color.red;
        _isBombCooldownActive = true;

        float remainingTime = _bombCooldownDuration;

        while (remainingTime > 0)
        {
            cooldownText.text = $"ready in: {Mathf.CeilToInt(remainingTime)}s";
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        cooldownText.text = "Bomb Ready !";
        image.color = Color.green;
        _isBombCooldownActive = false;
    }
}