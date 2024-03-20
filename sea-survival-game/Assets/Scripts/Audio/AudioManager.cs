using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    public AudioClip mainMenu;
    public AudioClip background1;
    public AudioClip background2;
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Audio Manager in the scene.Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            musicSource.clip = mainMenu;
            musicSource.Play();
        }
        if(currentScene.name == "B-test")
        {
            int r = Random.Range(0,2);
            if(r==1)
            {
                musicSource.clip = background1;
                musicSource.Play();
            }
            if (r == 2)
            {
                musicSource.clip = background2;
                musicSource.Play();
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
