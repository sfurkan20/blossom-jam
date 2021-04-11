using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSystem : MonoBehaviour
{
    public static bool canWalk = true;
    public static bool canWalkW = true;
    public static bool walking = false;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canWalk)
        {
            walking = false;
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.x > 15.4f)
                {
                    walking = false;
                    transform.position = new Vector3(15.4f, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.Translate(Time.deltaTime * 3.5f, 0, 0);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    walking = true;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (transform.position.x < -14.92f)
                {
                    walking = false;
                    transform.position = new Vector3(-14.92f, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.Translate(Time.deltaTime * 3.5f, 0, 0);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    walking = true;
                }
            }
            if (Input.GetKey(KeyCode.W) && canWalkW)
            {
                if (transform.position.y > 8.55f)
                {
                    walking = false;
                    transform.position = new Vector3(transform.position.x, 8.55f, transform.position.z);
                }
                else
                {
                    transform.Translate(0, Time.deltaTime * 3.5f, 0);
                    walking = true;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.y < -6.15f)
                {
                    walking = false;
                    transform.position = new Vector3(transform.position.x, -6.15f, transform.position.z);
                }
                else
                {
                    transform.Translate(0, -Time.deltaTime * 3.5f, 0);
                    walking = true;
                }
            }
            animator.SetBool("Walk", walking);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            canWalkW = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            canWalkW = true;
        }
    }
}
