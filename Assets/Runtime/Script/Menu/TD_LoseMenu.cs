using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_LoseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip _buttonClip;

    public void ReplayButton()
    {
        TD_AudioManager.instance.PlayClipAt(_buttonClip, transform.position);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        TD_AudioManager.instance.PlayClipAt(_buttonClip, transform.position);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
