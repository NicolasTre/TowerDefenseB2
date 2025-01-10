using UnityEngine;

public class TD_ButtonAnim : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Animator _animButton;
    [SerializeField] private string _nameAnim;


    public void OnMouseEnter()
    {
       _animButton.Play(_nameAnim);
    }

    public void OnMouseExit()
    {
       // _animButton.Play(_nameAnim);
    }
}
