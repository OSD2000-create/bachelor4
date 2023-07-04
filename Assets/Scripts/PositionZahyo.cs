using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR;
using Valve.VR;
using System.IO;
using System.Text;

public class PositionZahyo : MonoBehaviour
{
    //private StreamWriter swL;

    GameObject SaveCsv;
    CsvScript CsvScript;

    GameObject TimeCount;
    TimeCounter TimeCounter;

    public int stamp;
    public UnityEngine.UI.Text FaseCount;


    // Start is called before the first frame update
    void Start()
    {
        SaveCsv = GameObject.Find("SaveCsv"); //SavaCsvを参照する
        CsvScript = SaveCsv.GetComponent<CsvScript>();
        TimeCount = GameObject.Find("TimeCount");
        TimeCounter = TimeCount.GetComponent<TimeCounter>();
        stamp = 1;
    }

    //右手
    public Transform HandR;    //手の角度
    public Transform ThumbR1;   //親指
    public Transform ThumbR2;
    public Transform ThumbR3;
    public Transform ThumbR4;
    public Transform IndexR1;   //人差し指
    public Transform IndexR2;
    public Transform IndexR3;
    public Transform IndexR4;
    public Transform MiddleR1;  //中指
    public Transform MiddleR2;
    public Transform MiddleR3;
    public Transform MiddleR4;
    public Transform RingR1;    //薬指
    public Transform RingR2;
    public Transform RingR3;
    public Transform RingR4;
    public Transform PinkyR1;   //小指
    public Transform PinkyR2;
    public Transform PinkyR3;
    public Transform PinkyR4;

    //左手
    public Transform HandL;    //手の甲
    public Transform ThumbL1;   //親指
    public Transform ThumbL2;
    public Transform ThumbL3;
    public Transform ThumbL4;
    public Transform IndexL1;   //人差し指
    public Transform IndexL2;
    public Transform IndexL3;
    public Transform IndexL4;
    public Transform MiddleL1;  //中指
    public Transform MiddleL2;
    public Transform MiddleL3;
    public Transform MiddleL4;
    public Transform RingL1;    //薬指
    public Transform RingL2;
    public Transform RingL3;
    public Transform RingL4;
    public Transform PinkyL1;   //小指
    public Transform PinkyL2;
    public Transform PinkyL3;
    public Transform PinkyL4;
    //public Transform RoleL; //手の角度


   
    // Update is called once per frame
    void Update()
    {
        //右手
        Vector3 HandPosR = HandR.eulerAngles;
        Vector3 ThumbPosR1 = ThumbR1.position;
        Vector3 ThumbPosR2 = ThumbR2.position;
        Vector3 ThumbPosR3 = ThumbR3.position;
        Vector3 ThumbPosR4 = ThumbR4.position;
        Vector3 IndexPosR1 = IndexR1.position;
        Vector3 IndexPosR2 = IndexR2.position;
        Vector3 IndexPosR3 = IndexR3.position;
        Vector3 IndexPosR4 = IndexR4.position;
        Vector3 MiddlePosR1 = MiddleR1.position;
        Vector3 MiddlePosR2 = MiddleR2.position;
        Vector3 MiddlePosR3 = MiddleR3.position;
        Vector3 MiddlePosR4 = MiddleR4.position;
        Vector3 RingPosR1 = RingR1.position;
        Vector3 RingPosR2 = RingR2.position;
        Vector3 RingPosR3 = RingR3.position;
        Vector3 RingPosR4 = RingR4.position;
        Vector3 PinkyPosR1 = PinkyR1.position;
        Vector3 PinkyPosR2 = PinkyR2.position;
        Vector3 PinkyPosR3 = PinkyR3.position;
        Vector3 PinkyPosR4 = PinkyR4.position;

        //左手
        Vector3 HandPosL = HandL.eulerAngles;
        Vector3 ThumbPosL1 = ThumbL1.position;
        Vector3 ThumbPosL2 = ThumbL2.position;
        Vector3 ThumbPosL3 = ThumbL3.position;
        Vector3 ThumbPosL4 = ThumbL4.position;
        Vector3 IndexPosL1 = IndexL1.position;
        Vector3 IndexPosL2 = IndexL2.position;
        Vector3 IndexPosL3 = IndexL3.position;
        Vector3 IndexPosL4 = IndexL4.position;
        Vector3 MiddlePosL1 = MiddleL1.position;
        Vector3 MiddlePosL2 = MiddleL2.position;
        Vector3 MiddlePosL3 = MiddleL3.position;
        Vector3 MiddlePosL4 = MiddleL4.position;
        Vector3 RingPosL1 = RingL1.position;
        Vector3 RingPosL2 = RingL2.position;
        Vector3 RingPosL3 = RingL3.position;
        Vector3 RingPosL4 = RingL4.position;
        Vector3 PinkyPosL1 = PinkyL1.position;
        Vector3 PinkyPosL2 = PinkyL2.position;
        Vector3 PinkyPosL3 = PinkyL3.position;
        Vector3 PinkyPosL4 = PinkyL4.position;

        ThumbPosR1.x = ThumbPosR1.x + 10;
        ThumbPosR2.x = ThumbPosR2.x + 10;
        ThumbPosR3.x = ThumbPosR3.x + 10;
        ThumbPosR4.x = ThumbPosR4.x + 10;
        IndexPosR1.x = IndexPosR1.x + 10;
        IndexPosR2.x = IndexPosR2.x + 10;
        IndexPosR3.x = IndexPosR3.x + 10;
        IndexPosR4.x = IndexPosR4.x + 10;
        MiddlePosR1.x = MiddlePosR1.x + 10;
        MiddlePosR2.x = MiddlePosR2.x + 10;
        MiddlePosR3.x = MiddlePosR3.x + 10;
        MiddlePosR4.x = MiddlePosR4.x + 10;
        RingPosR1.x = RingPosR1.x + 10;
        RingPosR2.x = RingPosR2.x + 10;
        RingPosR3.x = RingPosR3.x + 10;
        RingPosR4.x = RingPosR4.x + 10;
        PinkyPosR1.x = PinkyPosR1.x + 10;
        PinkyPosR2.x = PinkyPosR2.x + 10;
        PinkyPosR3.x = PinkyPosR3.x + 10;
        PinkyPosR4.x = PinkyPosR4.x + 10;

        ThumbPosL1.x = ThumbPosL1.x + 15;
        ThumbPosL2.x = ThumbPosL2.x + 15;
        ThumbPosL3.x = ThumbPosL3.x + 15;
        ThumbPosL4.x = ThumbPosL4.x + 15;
        IndexPosL1.x = IndexPosL1.x + 15;
        IndexPosL2.x = IndexPosL2.x + 15;
        IndexPosL3.x = IndexPosL3.x + 15;
        IndexPosL4.x = IndexPosL4.x + 15;
        MiddlePosL1.x = MiddlePosL1.x + 15;
        MiddlePosL2.x = MiddlePosL2.x + 15;
        MiddlePosL3.x = MiddlePosL3.x + 15;
        MiddlePosL4.x = MiddlePosL4.x + 15;
        RingPosL1.x = RingPosL1.x + 15;
        RingPosL2.x = RingPosL2.x + 15;
        RingPosL3.x = RingPosL3.x + 15;
        RingPosL4.x = RingPosL4.x + 15;
        PinkyPosL1.x = PinkyPosL1.x + 15;
        PinkyPosL2.x = PinkyPosL2.x + 15;
        PinkyPosL3.x = PinkyPosL3.x + 15;
        PinkyPosL4.x = PinkyPosL4.x + 15;

        //TextPosition.text = string.Format("HansPosition:x:" + HandPos.x.ToString() + "y:" + HandPos.y.ToString() + "z:" + HandPos.z.ToString());
        //Debug.Log("HandPosR:" + HandPosR.x + ", " + HandPosR.y + ", " + HandPosR.z +
        //    "\nThumbPosR:" + ThumbPosR4.x + ", " + ThumbPosR4.y + ", " + ThumbPosR4.z +
        //    "\nIndexPosR:" + IndexPosR4.x + ", " + IndexPosR4.y + ", " + IndexPosR4.z +
        //    "\nMiddlePosR:" + MiddlePosR4.x + ", " + MiddlePosR4.y + ", " + MiddlePosR4.z +
        //    "\nRingPosR:" + RingPosR4.x + ", " + RingPosR4.y + ", " + RingPosR4.z +
        //    "\nPinkyPosR:" + PinkyPosR4.x + ", " + PinkyPosR4.y + ", " + PinkyPosR4.z +
        //    "\nHandPosL:" + HandPosL.x + ", " + HandPosL.y + ", " + HandPosL.z +
        //    "\nThumbPosL:" + ThumbPosL4.x + ", " + ThumbPosL4.y + ", " + ThumbPosL4.z +
        //    "\nIndexPosL:" + IndexPosL4.x + ", " + IndexPosL4.y + ", " + IndexPosL4.z +
        //    "\nMiddlePosL:" + MiddlePosL4.x + ", " + MiddlePosL4.y + ", " + MiddlePosL4.z +
        //    "\nRingPosL:" + RingPosL4.x + ", " + RingPosL4.y + ", " + RingPosL4.z +
        //    "\nPinkyPosL:" + PinkyPosL4.x + ", " + PinkyPosL4.y + ", " + PinkyPosL4.z);

        if (TimeCounter.a == 1)
        {
            CsvScript.SaveDataR(TimeCounter.countTime.ToString("F4"), HandPosR.x.ToString(), HandPosR.y.ToString(), HandPosR.z.ToString(),
                ThumbPosR1.x.ToString(), ThumbPosR1.y.ToString(), ThumbPosR1.z.ToString(),
                ThumbPosR2.x.ToString(), ThumbPosR2.y.ToString(), ThumbPosR2.z.ToString(),
                ThumbPosR3.x.ToString(), ThumbPosR3.y.ToString(), ThumbPosR3.z.ToString(),
                ThumbPosR4.x.ToString(), ThumbPosR4.y.ToString(), ThumbPosR4.z.ToString(),
                IndexPosR1.x.ToString(), IndexPosR1.y.ToString(), IndexPosR1.z.ToString(),
                IndexPosR2.x.ToString(), IndexPosR2.y.ToString(), IndexPosR2.z.ToString(),
                IndexPosR3.x.ToString(), IndexPosR3.y.ToString(), IndexPosR3.z.ToString(),
                IndexPosR4.x.ToString(), IndexPosR4.y.ToString(), IndexPosR4.z.ToString(),
                MiddlePosR1.x.ToString(), MiddlePosR1.y.ToString(), MiddlePosR1.z.ToString(),
                MiddlePosR2.x.ToString(), MiddlePosR2.y.ToString(), MiddlePosR2.z.ToString(),
                MiddlePosR3.x.ToString(), MiddlePosR3.y.ToString(), MiddlePosR3.z.ToString(),
                MiddlePosR4.x.ToString(), MiddlePosR4.y.ToString(), MiddlePosR4.z.ToString(),
                RingPosR1.x.ToString(), RingPosR1.y.ToString(), RingPosR1.z.ToString(),
                RingPosR2.x.ToString(), RingPosR2.y.ToString(), RingPosR2.z.ToString(),
                RingPosR3.x.ToString(), RingPosR3.y.ToString(), RingPosR3.z.ToString(),
                RingPosR4.x.ToString(), RingPosR4.y.ToString(), RingPosR4.z.ToString(),
                PinkyPosR1.x.ToString(), PinkyPosR1.y.ToString(), PinkyPosR1.z.ToString(),
                PinkyPosR2.x.ToString(), PinkyPosR2.y.ToString(), PinkyPosR2.z.ToString(),
                PinkyPosR3.x.ToString(), PinkyPosR3.y.ToString(), PinkyPosR3.z.ToString(),
                PinkyPosR4.x.ToString(), PinkyPosR4.y.ToString(), PinkyPosR4.z.ToString(), stamp.ToString());

            CsvScript.SaveDataL(TimeCounter.countTime.ToString("F4"), HandPosL.x.ToString(), HandPosL.y.ToString(), HandPosL.z.ToString(),
                ThumbPosL1.x.ToString(), ThumbPosL1.y.ToString(), ThumbPosL1.z.ToString(),
                ThumbPosL2.x.ToString(), ThumbPosL2.y.ToString(), ThumbPosL2.z.ToString(),
                ThumbPosL3.x.ToString(), ThumbPosL3.y.ToString(), ThumbPosL3.z.ToString(),
                ThumbPosL4.x.ToString(), ThumbPosL4.y.ToString(), ThumbPosL4.z.ToString(),
                IndexPosL1.x.ToString(), IndexPosL1.y.ToString(), IndexPosL1.z.ToString(),
                IndexPosL2.x.ToString(), IndexPosL2.y.ToString(), IndexPosL2.z.ToString(),
                IndexPosL3.x.ToString(), IndexPosL3.y.ToString(), IndexPosL3.z.ToString(),
                IndexPosL4.x.ToString(), IndexPosL4.y.ToString(), IndexPosL4.z.ToString(),
                MiddlePosL1.x.ToString(), MiddlePosL1.y.ToString(), MiddlePosL1.z.ToString(),
                MiddlePosL2.x.ToString(), MiddlePosL2.y.ToString(), MiddlePosL2.z.ToString(),
                MiddlePosL3.x.ToString(), MiddlePosL3.y.ToString(), MiddlePosL3.z.ToString(),
                MiddlePosL4.x.ToString(), MiddlePosL4.y.ToString(), MiddlePosL4.z.ToString(),
                RingPosL1.x.ToString(), RingPosL1.y.ToString(), RingPosL1.z.ToString(),
                RingPosL2.x.ToString(), RingPosL2.y.ToString(), RingPosL2.z.ToString(),
                RingPosL3.x.ToString(), RingPosL3.y.ToString(), RingPosL3.z.ToString(),
                RingPosL4.x.ToString(), RingPosL4.y.ToString(), RingPosL4.z.ToString(),
                PinkyPosL1.x.ToString(), PinkyPosL1.y.ToString(), PinkyPosL1.z.ToString(),
                PinkyPosL2.x.ToString(), PinkyPosL2.y.ToString(), PinkyPosL2.z.ToString(),
                PinkyPosL3.x.ToString(), PinkyPosL3.y.ToString(), PinkyPosL3.z.ToString(),
                PinkyPosL4.x.ToString(), PinkyPosL4.y.ToString(), PinkyPosL4.z.ToString(), stamp.ToString());
        }
        
        FaseCount.text = "fase:" + stamp.ToString();
    }

    public void OnClick()
    {
        stamp++;
    }
}
