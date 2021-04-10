using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main singleton;
    public Image hp1img, hp2img, ragebar;
    float _hp1;
    public float hp1
    {
        get
        {
            return _hp1;
        }
        set
        {
            hp1img.transform.localScale = new Vector3(value / 100f, 1, 1);
            _hp1 = value;
        }
    }
    float _hp2;
    public float hp2
    {
        get
        {
            return _hp2;
        }
        set
        {
            hp2img.transform.localScale = new Vector3(value / 100f, 1, 1);
            _hp2 = value;
        }
    }
    float _rage;
    public float rage
    {
        get
        {
            return _rage;
        }
        set
        {
            _rage = value;
        }
    }
    void Start()
    {
        singleton = this;
        hp1 = 100;
        hp2 = 100;
    }

    void Update()
    {
        
    }

    public void setHP(string name, float decrease)
    {
        switch (name)
        {
            case "char2":
                hp1 += decrease;
                break;
            case "char1":
                hp2 += decrease;
                break;
        }
    }
}
