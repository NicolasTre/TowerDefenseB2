using UnityEngine;

public class TD_BuildManager : MonoBehaviour
{
    public static TD_BuildManager main;

    [Header("References")]
    [SerializeField] private TD_Tower[] _towers;
    [SerializeField] private GameObject[] _towerCanvasSelected;
    [SerializeField] private Animator _animCanvas;
    private GameObject _towerSelected;

    private int selectedTower = 0;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        _towerSelected = _towerCanvasSelected[0];
    }

    private void Update()
    {
        ActiveCanvasAnimShop();
    }

    public TD_Tower GetSelectedTower()
    {
        return _towers[selectedTower];
    }

    public void SelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    private void ActiveCanvasAnimShop()
    {
        switch (selectedTower)
        {
            case 0:
                ActiveCanvas(0, "Canvas0");
                break;
            case 1:
                ActiveCanvas(1, "Canvas1");
                break;
            case 2:
                ActiveCanvas(2, "Canvas 2");
                break;
            case 3:
                ActiveCanvas(3, "Canvas4");
                break;
        }
    }

    private void ActiveCanvas(int towerID, string nameAnim)
    {
        _towerSelected.SetActive(false);
        _towerCanvasSelected[towerID].SetActive(true);
        _animCanvas.Play(nameAnim);
        _towerSelected = _towerCanvasSelected[towerID];
    }

}
