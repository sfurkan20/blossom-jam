using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static int stage = 0;
    public static Main singleton;
    public Image hp1img, hp2img, ragebar;
    public GameObject char1, char2;
    public GameObject ending, fightstart;
    float _hp1;
    public float hp1
    {
        get
        {
            return _hp1;
        }
        set
        {
            hp1img.transform.localScale = new Vector3(value / 100f, 1, 1);
            if(value <= 0)
            {
                ending.SetActive(true);
                Destroy(char1);
                Destroy(char2);
            }
            _hp1 = value;
        }
    }
    float _hp2;
    public float hp2
    {
        get
        {
            return _hp2;
        }
        set
        {
            if(value <= 0)
            {
                NextStage(0);
                Destroy(char1);
                Destroy(char2);
            }
            hp2img.transform.localScale = new Vector3(value / 100f, 1, 1);
            _hp2 = value;
        }
    }
    float _rage;
    public float rage
    {
        get
        {
            return _rage;
        }
        set
        {
            if(value > 100)
            {
                value = 100;
            }
            ragebar.transform.localScale = new Vector3(value / 100f, 1, 1);
            _rage = value;
        }
    }
    void Start()
    {
        singleton = this;
        hp1 = 100;
        hp2 = 100;
        rage = 0;
    }

    void Update()
    {
        DialogSystem.currentDialog = new DialogSystem.Dialog(6, DialogSystem.Dialog.post_state.NEW_DIALOG);
    }

    public void setHP(string name, float decrease)
    {
        switch (name)
        {
            case "char2":
                hp1 += decrease;
                break;
            case "char1":
                hp2 += decrease;
                break;
        }
    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void NextStage(int i)
    {
        if(stage == 3)
        {
            GameOver();
        }
        else
        {
            stage++;
            DialogSystem.onDialog = false;
            DialogSystem.singleton.dialog_related.SetActive(false);
            fightstart.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/pp/" + stage.ToString());
            fightstart.SetActive(true);
        }
    }
}
