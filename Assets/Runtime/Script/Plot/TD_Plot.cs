using UnityEngine;

public class TD_Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] SpriteRenderer _sr;
    [SerializeField] Color _hoverColor;

    private GameObject _towerObj;
    private Color _startColor;

    public TD_Turret turret; 

    private void Start()
    {
        _startColor = _sr.color;
    }

    private void OnMouseEnter()
    {
        _sr.color = _hoverColor;
    }

    private void OnMouseExit()
    {
       _sr.color = _startColor;
    }

    private void OnMouseDown()
    {
        if (TD_UIManager.main.IsHoveringUI())
        {
            return;
        }

        if (_towerObj != null) 
        {
            turret.OpenUpgradeUI();
            return;
        }

        TD_Tower towerToBuild = TD_BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > TD_LevelManager.main.currency)
        {

            return; 
        }

        TD_LevelManager.main.SpendCurrency(towerToBuild.cost);  
        _towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = _towerObj.GetComponent<TD_Turret>();
    }
}