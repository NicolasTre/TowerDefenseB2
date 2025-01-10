using UnityEngine;
using UnityEngine.EventSystems;

public class TD_UpgradeHanglerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool _mouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseOver = true;
        TD_UIManager.main.SetHoveringState(true);
    }  
        
    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseOver = false;
        TD_UIManager.main.SetHoveringState(false);
        gameObject.SetActive(false);
    }
}
