using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_LoseMenu : MonoBehaviour
{
    public void ReplayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
