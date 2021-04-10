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
        if (!DialogSystem.onDialog)
        {
            Vector3 char_pos = character.position;
            transform.position = new Vector3(char_pos.x, char_pos.y, -10);
            if (transform.position.x < -4.48f)
            {
                Vector3 pos = transform.position;
                transform.position = new Vector3(-4.48f, pos.y, pos.z);
            }
            else if (transform.position.x > 4.48f)
            {
                Vector3 pos = transform.position;
                transform.position = new Vector3(4.48f, pos.y, pos.z);
            }
            if (transform.position.y > 3.46f)
            {
                Vector3 pos = transform.position;
                transform.position = new Vector3(pos.x, 3.46f, pos.z);
            }
            else if (transform.position.y < -1.5f)
            {
                Vector3 pos = transform.position;
                transform.position = new Vector3(pos.x, -1.5f, pos.z);
            }
        }
    }
}
