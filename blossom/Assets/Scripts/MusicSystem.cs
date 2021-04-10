using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public enum Stage { MAIN_MENU = 0, FIRST_DIALOGS = 1, LEVEL1 = 2,LEVEL2 = 3,LEVEL3 = 4, VICTORY= 5, DEFEAT = 6};
    static AudioSource source;
    public AudioClip[] clips;
    public static MusicSystem singleton;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        singleton = this;
        source = GetComponent<AudioSource>();
        setStage(Stage.MAIN_MENU);
    }

    void Update()
    {
        
    }

    public static void setStage(Stage s)
    {
        source.clip = singleton.clips[(int)s];
        source.Play();
    }

    public void SetScene(int id)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(id);
    }
}
