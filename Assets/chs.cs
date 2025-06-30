using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chs : MonoBehaviour
{
    float a, b = 1.0f,c = 0.40f;
    float speed = 0.05f;
    bool t;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (a >= b) {
            t = true;
        }
        if (a <= c) {
            t = false;
        }

        if (t)
        {
            a -= speed;
        }
        else 
        {
            a += speed;
        }

        GetComponent<Image>().color = new Color(0.2653524f, 0.5810797f, 0.8396226f, a);
    }
}
