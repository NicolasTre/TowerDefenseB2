using TMPro;
using UnityEngine;

public class TD_LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeText;
    [SerializeField] private GameObject _LoseCanvas;

    public static TD_LevelManager main;

    public Transform startPoint; 
    public Transform[] path;
    public int life = 3;

    public Animator loseLifeAnim;
    public Animator loseLifeTextAnim;

    public int currency;


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

        if (life <= 0)
        {
            LevelLose();
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
        _LoseCanvas.SetActive(true);
    }
}
