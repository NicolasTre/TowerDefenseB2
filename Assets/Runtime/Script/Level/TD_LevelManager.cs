using TMPro;
using UnityEngine;

public class TD_LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _powerUpCanvas;
    [SerializeField] private AudioClip _loseGameAudio;
    public static TD_LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public int life = 3;

    public Animator loseLifeAnim;
    public Animator loseLifeTextAnim;

    public int currency;

    private bool _lvlFinish = false;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
    }

    private void Update()
    {
        _lifeText.text = ("Life : " + life.ToString());

        if (life <= 0 && !_lvlFinish)
        {
            LevelLose();
            _lvlFinish = true;
            return;
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //buy
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("no enought");
            return false;
        }
    }

    private void LevelLose()
    {
        Time.timeScale = 0f;
        _powerUpCanvas.SetActive(false);
        _loseCanvas.SetActive(true);
        TD_AudioManager.instance.PlayClipAt(_loseGameAudio, transform.position);
    }
}
