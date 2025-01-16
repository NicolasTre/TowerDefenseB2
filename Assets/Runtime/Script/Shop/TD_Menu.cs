using TMPro;
using UnityEngine;
public class TD_Menu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool _isMenuOpen = true;

    public void ToggleMenu()
    {
        _isMenuOpen = !_isMenuOpen;
        anim.SetBool("MenuOpen", _isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = ("Money : " + TD_LevelManager.main.currency.ToString());
    }
}
