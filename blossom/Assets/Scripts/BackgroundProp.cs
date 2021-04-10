using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundProp : MonoBehaviour
{
    float totaltime = 0;
    struct Changer
    {
        public float time;
        public float x1, y1;
        public float x2, y2;
        public int clockwise;
        public Changer(float time, float _x1, float _x2, float _y1, float _y2)
        {
            this.time = time;
            x1 = _x1;
            x2 = _x2;
            y1 = _y1;
            y2 = _y2;
            clockwise = Random.Range(0, 2) == 0 ? -1 : 1;
        }
    }
    Changer changer;
    // 13, -17
    //1.45, 7.5

    //-15, 17
    //8.72, 3.25
    void Start()
    {
        float startx = Random.Range(-16f, 16f);
        float endx = 0;
        if (startx >= 0)
        {
            endx = startx - 16;
        }
        else
        {
            endx = startx + 16;
        }
        changer = new Changer(10, startx, endx, Random.Range(7f,5f), 3f);
        transform.position = new Vector3(changer.x1, changer.y1, transform.position.z);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        totaltime += Time.deltaTime;
        if(pos.x < -16 || pos.x > 16 || pos.y > 11)
        {
            StartCoroutine(Destry());
        }
        float xdiff = (changer.x2 - changer.x1) / changer.time;
        float ydiff = (changer.y2 - changer.y1) / changer.time;
        transform.Translate(xdiff * Time.deltaTime, ydiff * Time.deltaTime, 0, Space.World);
        transform.Rotate(0, 0, changer.clockwise * Time.deltaTime * 10);
        
    }
    
    IEnumerator Destry()
    {
        yield return new WaitForSeconds(1);
        GameObject newgo = new GameObject();
        int no = Random.Range(1, 10);
        newgo.transform.position = new Vector3(1000, 1000, 0.53f);
        newgo.AddComponent<SpriteRenderer>();
        newgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bg/" + no.ToString());
        newgo.AddComponent<BackgroundProp>();
        Destroy(gameObject);
    }
}
