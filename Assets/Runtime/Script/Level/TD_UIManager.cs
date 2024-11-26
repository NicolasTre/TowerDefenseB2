using UnityEngine;

public class TD_UIManager : MonoBehaviour
{
    public static TD_UIManager main;

    private bool _isHoveringUI;

    private void Awake()
    {
        main = this;
    }

    public  void SetHoveringState(bool state)
    {
        _isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return _isHoveringUI;
    }

}
