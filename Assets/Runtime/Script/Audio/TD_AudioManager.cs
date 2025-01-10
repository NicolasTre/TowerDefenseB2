using UnityEngine;
using UnityEngine.Audio;

public class TD_AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip[] _menuPlaylist;
    [SerializeField] private AudioSource _menuAudioSource;
    [SerializeField] private AudioMixerGroup _soundEffectMixer;

    [Header("Attributes")]
    private int _musicIndex = 0;

    public static TD_AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Il y a plus d'une instance de cette audio manager dans la scene");
            return;
        }
        instance = this;
    }
    void Start()
    {
        _menuAudioSource.clip = _menuPlaylist[0];
        _menuAudioSource.Play();
    }

    void Update()
    {
        if (!_menuAudioSource.isPlaying)
        {
            PlayNextSongs();
        }
    }

    private void PlayNextSongs()
    {
        _musicIndex = (_musicIndex + 1) % _menuPlaylist.Length;
        _menuAudioSource.clip = _menuPlaylist[_musicIndex];
        _menuAudioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = _soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO,clip.length);
        return audioSource;
    }
}
