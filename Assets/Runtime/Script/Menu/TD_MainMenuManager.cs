using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_MainMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animTransiMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _leaveConfirmMenu;
    [SerializeField] private AudioClip _buttonAudio;


    public void OnPLayButton(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene);
    }

    public void AnimTransiCreditsOn()
    {
        StartCoroutine(AnimTransi(_mainMenu, _creditsMenu));
    }
    public void AnimTransiCreditsOff()
    {
        StartCoroutine(AnimTransi(_creditsMenu, _mainMenu));
    }
    
    public void AnimTransiLeaveOn()
    {
        StartCoroutine(AnimTransi(_mainMenu, _leaveConfirmMenu));
    }
    
    public void AnimTransiLeaveOff()
    {
        StartCoroutine(AnimTransi(_leaveConfirmMenu, _mainMenu));
    }

    public void YesLeaveButton()
    {
        Application.Quit();
    }

    private IEnumerator AnimTransi(GameObject _currentScene, GameObject _nextScene)
    {
        AnimatorStateInfo stateInfo = _animTransiMenu.GetCurrentAnimatorStateInfo(0);
        TD_AudioManager.instance.PlayClipAt(_buttonAudio, transform.position);
        float animationDuration = stateInfo.length;
        _animTransiMenu.Play("PanelAnimTransiOff");
        yield return new WaitForSeconds(2f);
        _currentScene.SetActive(false);
        _nextScene.SetActive(true);
    }

}
