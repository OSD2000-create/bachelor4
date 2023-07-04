using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public UnityEngine.UI.Text TimeText;

    public static float countTime;

    public int a;

    // Start is called before the first frame update
    void Start()
    {
        countTime = 0;
        a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (a == 1)
        {
            countTime += Time.deltaTime;
        }
        if (a == 2)
        {
            countTime = countTime;
        }
        if (a == 0)
        {
            countTime = 0;
        }

        TimeText.text = "Time:" + countTime.ToString("F4");
    }

    public void OnClick1()
    {
        a = 1;
    }
    public void OnClick2()
    {
        a = 2;
    }
    public void OnClick0()
    {
        a = 0;
    }
}
