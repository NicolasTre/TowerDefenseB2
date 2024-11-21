using UnityEngine;

public class TD_LevelManager : MonoBehaviour
{
    public static TD_LevelManager main;

    public Transform _startPoint; 
    public Transform[] path; 


    private void Awake()
    {
        main = this; 
    }

}
