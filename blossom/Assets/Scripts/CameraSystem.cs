using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform character;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 char_pos = character.position;
        transform.position = new Vector3(char_pos.x,char_pos.y, -10);
        if(transform.position.x < -5.62f)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(-5.62f, pos.y, pos.z);
        }
        else if (transform.position.x > 5.62f)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(5.62f, pos.y, pos.z);
        }
        if (transform.position.y > 4.18f)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, 4.18f, pos.z);
        }
        else if (transform.position.y < -2.13f)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, -2.13f, pos.z);
        }
    }
}
