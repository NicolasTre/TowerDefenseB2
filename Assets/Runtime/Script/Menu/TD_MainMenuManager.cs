using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_MainMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string _nameScene;


    private void OnPLayButton()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
