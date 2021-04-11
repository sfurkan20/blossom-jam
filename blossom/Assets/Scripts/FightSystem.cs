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
    public SpriteRenderer moonlander;
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
        attacking = 0;
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
            last_press_delta = 0;
            last_press_threshold = 1.2f;
            attacking = 3;
        }
    }

    public void startFght(GameObject g)
    {
        attacking = 0;
        // ADD IDLE ANIMATION
        //DialogSystem.singleton.char1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>
        DialogSystem.singleton.char1.transform.GetChild(0).GetComponent<AttackSystem>().currentAttack = AttackSystem.AttackType.NONE;
        Animator a = DialogSystem.singleton.char1.GetComponent<Animator>();
        Animator a2 = DialogSystem.singleton.char2.GetComponent<Animator>();
        a2.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Controllers/" + Main.stage.ToString());
        a.SetBool("Attack1", false);
        a.SetBool("Attack2", false);
        a.SetBool("Attack3", false);
        a.SetBool("Stun", false);
        a.SetBool("Walk", false);
        Main.singleton.hp1 = 100;
        Main.singleton.hp2 = 100;
        Main.singleton.rage = 0;
        startFight(Main.stage);
        g.SetActive(false);
    }
    public static void startFight(int id)
    {
        MusicSystem.setStage((MusicSystem.Stage)(id + 1));
        singleton.moonlander.sprite = Resources.Load<Sprite>("Sprites/vehicles/" + id.ToString());
        singleton.c1pp.sprite = Resources.Load<Sprite>("Sprites/pp/0");
        singleton.c2pp.sprite = Resources.Load<Sprite>("Sprites/pp/" + id.ToString());
        DialogSystem.singleton.char1.SetActive(true);
        DialogSystem.singleton.char2.SetActive(true);
        DialogSystem.singleton.char1.transform.position = new Vector3(-6f, -2.58f, 1);
        DialogSystem.singleton.char2.transform.position = new Vector3(6f, -2.58f, 1.1f);
        /*GameObject c1 = Instantiate(DialogSystem.singleton.char1, null, true);
        GameObject c2 = Instantiate(DialogSystem.singleton.char2, null, true);
        c1.name = "char1";
        c2.name = "char2";
        c1.SetActive(false);
        c2.SetActive(false);*/
        DialogSystem.onDialog = false;
        DialogSystem.singleton.fight_related.SetActive(true);
        DialogSystem.singleton.dialog_related.SetActive(false);
        WalkSystem.canWalk = true;
    }
}
