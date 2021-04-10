using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    public static FightSystem singleton;
    public static int level = 1;
    public Animator animator;
    static int _attacking = 0;
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
                for (int i = 0; i < 3; i++)
                {
                    singleton.animator.SetBool("Attack" + i.ToString(), i == value);
                }
            }
        }
    }
    float last_press_delta = 0;
    void Start()
    {
        singleton = this;
    }

    void Update()
    {
        last_press_delta += Time.deltaTime;
        if(last_press_delta > 0.5)
        {
            attacking = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            last_press_delta = 0;
            attacking = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            last_press_delta = 0;
            attacking = 2;
        }
    }

    public static void startFight(int id)
    {
        WalkSystem.canWalk = true;
    }
}
