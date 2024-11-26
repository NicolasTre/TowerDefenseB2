using UnityEngine;

public class TD_LevelManager : MonoBehaviour
{
    public static TD_LevelManager main;

    public Transform startPoint; 
    public Transform[] path;

    public int currency;


    private void Awake()
    {
        main = this; 
    }

    private void Start()
    {
        currency = 50;
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


}
