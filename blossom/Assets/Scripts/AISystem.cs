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
        StartCoroutine(CheckRivalPos());
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack1", true);
        }
    }

    IEnumerator CheckRivalPos()
    {
        target = rival.position;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CheckRivalPos());
    }
}
