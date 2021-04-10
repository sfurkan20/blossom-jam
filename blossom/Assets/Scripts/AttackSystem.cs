using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public bool didAttack = false;
    public GameObject otherChar;
    public enum AttackType { ATTACK_SLAP, ATTACK_FIST, ATTACK_RAGE, NONE };
    public AttackType currentAttack = AttackType.ATTACK_SLAP;
    public Animator animator;
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();   
    }

    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            bool _continue = false;
            if (info.IsTag("0"))
            {
                _continue = false;
                currentAttack = AttackType.NONE;
            }
            else if (info.IsTag("1"))
            {
                _continue = true;
                currentAttack = AttackType.ATTACK_SLAP;
            }
            else if (info.IsTag("2"))
            {
                _continue = true;
                currentAttack = AttackType.ATTACK_FIST;
            }
            else if (info.IsTag("3"))
            {
                _continue = true;
                currentAttack = AttackType.ATTACK_RAGE;
            }
            if (_continue)
            {
                if (transform.parent.name == "char1")
                {
                    print(info.normalizedTime % info.length < 0.02f);
                }
                if (info.normalizedTime % info.length < 0.02f)
                {
                    TakeDamage(currentAttack);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            didAttack = false;
        }
    }

    public void TakeDamage(AttackType type)
    {
        float deltahp = 0;
        float deltarage = 0;
        int stun_prob = Random.Range(0, 100);
        switch (type)
        {
            case AttackType.ATTACK_SLAP:
                deltahp = Random.Range(-1f, -0.5f);
                deltarage = Random.Range(3f, 6f);
                Main.singleton.setHP(transform.parent.name, deltahp);
                if(stun_prob <= 5)
                {
                    Stun();
                }
                break;
            case AttackType.ATTACK_FIST:
                deltahp = Random.Range(-7f, -4f);
                deltarage = Random.Range(10f, 20f);
                Main.singleton.setHP(transform.parent.name, deltahp);
                if(stun_prob <= 30)
                {
                    Stun();
                }
                break;
            case AttackType.ATTACK_RAGE:
                deltahp = Random.Range(-15f, -9f);
                Main.singleton.setHP(transform.parent.name, deltahp);
                Stun();
                break;
        }
        if(transform.parent.name == "char1")
        {
            Main.singleton.rage += deltarage;
        }
    }

    public void Stun()
    {
        if(transform.parent.name == "char1")
        {
            GameObject.Find("char2").GetComponent<Animator>().SetBool("Stun", true);
        }
        if (transform.parent.name == "char2")
        {
            GameObject.Find("char1").GetComponent<Animator>().SetBool("Stun", true);
        }
    }
}
