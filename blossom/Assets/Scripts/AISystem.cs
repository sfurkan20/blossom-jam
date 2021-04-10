using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : MonoBehaviour
{
    Vector2 target;
    Animator animator;
    public Transform rival;
    void Start()
    {
        //StartCoroutine(CheckRivalPos());
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        target = rival.position;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 diff = target - pos;
        transform.Translate(diff * Time.deltaTime, Space.World);
        if(diff.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(diff.magnitude > 2.2f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            if (!animator.GetBool("Attack1") && !animator.GetBool("Attack2"))
            {
                int fist_prob = Random.Range(0, 100);
                int id = fist_prob <= 30 ? 2 : 1;
                animator.SetBool("Attack" + id.ToString(), true);
            }
        }
    }

    IEnumerator CheckRivalPos()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CheckRivalPos());
    }
}
