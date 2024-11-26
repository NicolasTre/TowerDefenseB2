using UnityEngine;

public class TD_BuildManager : MonoBehaviour
{
    public static TD_BuildManager main;

    [Header("References")]
    [SerializeField] private TD_Tower[] _towers; 


    [Header("Attributes")]

    private int selectedTower = 0;
    private void Awake()
    {
        main = this;
    }

    public TD_Tower GetSelectedTower()
    {
        return _towers[selectedTower];
    }

    public void SelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
