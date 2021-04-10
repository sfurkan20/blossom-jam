using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSystem : MonoBehaviour
{
    public static FightSystem singleton;
    public Animator animator;
    static int _attacking = 0;
    public Image c1pp, c2pp;
    public static int attacking
    {
        get
        {
            return _attacking;
        }
        set
        {
            _attacking = value;
            if (value == 0)
            {
                singleton.animator.SetBool("Attack1", false);
                singleton.animator.SetBool("Attack2", false);
                singleton.animator.SetBool("Attack3", false);
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    singleton.animator.SetBool("Attack" + i.ToString(), i == value);
                }
            }
        }
    }
    float last_press_delta = 0;
    float last_press_threshold = 0.5f;
    void Start()
    {
        singleton = this;
    }

    void Update()
    {
        last_press_delta += Time.deltaTime;
        if(last_press_delta > last_press_threshold)
        {
            attacking = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            last_press_delta = 0;
            last_press_threshold = 0.5f;
            attacking = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            last_press_delta = 0;
            last_press_threshold = 0.8f;
            attacking = 2;
        }
        else if(Input.GetKeyDown(KeyCode.R) && Main.singleton.rage >= 100)
        {
            Main.singleton.rage = 0;
            last_press_delta = 0;
            last_press_threshold = 1.2f;
            attacking = 3;
        }
    }

    public void startFght(GameObject g)
    {
        startFight(Main.stage);
        g.SetActive(false);
    }
    public static void startFight(int id)
    {
        singleton.c1pp.sprite = Resources.Load<Sprite>("Sprites/pp/0");
        singleton.c2pp.sprite = Resources.Load<Sprite>("Sprites/pp/" + id.ToString());
        DialogSystem.singleton.char1.SetActive(true);
        DialogSystem.singleton.char2.SetActive(true);
        GameObject c1 = Instantiate(DialogSystem.singleton.char1, null, true);
        GameObject c2 = Instantiate(DialogSystem.singleton.char2, null, true);
        c1.name = "char1";
        c2.name = "char2";
        c1.SetActive(false);
        c2.SetActive(false);
        Main.singleton.hp2 = 100;
        Main.singleton.rage = 0;
        DialogSystem.singleton.char1 = c1;
        DialogSystem.singleton.char2 = c2;
        DialogSystem.onDialog = false;
        DialogSystem.singleton.fight_related.SetActive(true);
        DialogSystem.singleton.dialog_related.SetActive(false);
        WalkSystem.canWalk = true;
    }
}
