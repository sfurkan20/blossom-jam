using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayAnim(GameObject g)
    {
        g.GetComponent<Animator>().SetBool("Explode", true);
    }

    public void Default(GameObject g)
    {
        g.GetComponent<Animator>().SetBool("Explode", false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
