using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CsvScript : MonoBehaviour
{
    private StreamWriter swR;
    private StreamWriter swL;
    private StreamWriter swZentai; //11/22
    public int R1 = 0;
    public int L1 = 0;
    public int Zen1 = 0;//11/22
    
    // Start is called before the first frame update
    void Start()
    {
        swR = new StreamWriter(@"SaveData_R.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = {"時間(秒)","右手角度：x", "右手角度：y", "右手角度：z" , "右手親指(CM関節)：x", "右手親指(CM関節)：y", "右手親指(CM関節)：z", "右手親指(MP関節)：x", "右手親指(MP関節)：y", "右手親指(MP関節)：z",
            "右手親指(IP関節)：x", "右手親指(IP関節)：y", "右手親指(IP関節)：z", "右手親指(先端)：x", "右手親指(先端)：y", "右手親指(先端)：z", "右手人差し指(MP関節)：x", "右手人差し指(MP関節)：y", "右手人差し指(MP関節)：z",
            "右手人差し指(PIP関節)：x", "右手人差し指(PIP関節)：y", "右手人差し指(PIP関節)：z", "右手人差し指(DIP関節)：x", "右手人差し指(DIP関節).y", "右手人差し指(DIP関節)：z", "右手人差し指(先端)：x", "右手人差し指(先端)：y", "右手人差し指(先端)：z",
            "右手中指(MP関節)：x", "右手中指(MP関節)：y", "右手中指(MP関節)：z", "右手中指(PIP関節)：x", "右手中指(PIP関節)：y", "右手中指(PIP関節)：z", "右手中指(DIP関節)：x", "右手中指(DIP関節)：y", "右手中指(DIP関節)：z",
            "右手中指(先端)：x", "右手中指(先端)：y", "右手中指(先端)：z","右手薬指(MP関節)：x", "右手薬指(MP関節)：y", "右手薬指(MP関節)：z", "右手薬指(PIP関節)：x", "右手薬指(PIP関節)：y", "右手薬指(PIP関節)：z" , 
            "右手薬指(DIP関節)：x", "右手薬指(DIP関節)：y", "右手薬指(DIP関節)：z", "右手薬指(先端)：x", "右手薬指(先端)：y", "右手薬指(先端)：z" , "右手小指(MP関節)：x", "右手小指(MP関節)：y", "右手小指(MP関節)：z",
            "右手小指(PIP関節)：x", "右手小指(PIP関節)：y", "右手小指(PIP関節)：z", "右手小指(DIP関節)：x", "右手小指(DIP関節)：y", "右手小指(DIP関節)：z", "右手小指(先端)：x", "右手小指(先端)：y", "右手小指(先端)：z" ,"スタンプ"};
        string s2 = string.Join(",", s1);
        swR.WriteLine(s2);

        swL = new StreamWriter(@"SaveData_L.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s3 = {"時間(秒)","左手角度：x", "左手角度：y", "左手角度：z" , "左手親指(CM関節)：x", "左手親指(CM関節)：y", "左手親指(CM関節)：z", "左手親指(MP関節)：x", "左手親指(MP関節)：y", "左手親指(MP関節)：z",
            "左手親指(IP関節)：x", "左手親指(IP関節)：y", "左手親指(IP関節)：z", "左手親指(先端)：x", "左手親指(先端)：y", "左手親指(先端)：z", "左手人差し指(MP関節)：x", "左手人差し指(MP関節)：y", "左手人差し指(MP関節)：z",
            "左手人差し指(PIP関節)：x", "左手人差し指(PIP関節)：y", "左手人差し指(PIP関節)：z", "左手人差し指(DIP関節)：x", "左手人差し指(DIP関節).y", "左手人差し指(DIP関節)：z", "左手人差し指(先端)：x", "左手人差し指(先端)：y", "左手人差し指(先端)：z",
            "左手中指(MP関節)：x", "左手中指(MP関節)：y", "左手中指(MP関節)：z", "左手中指(PIP関節)：x", "左手中指(PIP関節)：y", "左手中指(PIP関節)：z", "左手中指(DIP関節)：x", "左手中指(DIP関節)：y", "左手中指(DIP関節)：z",
            "左手中指(先端)：x", "左手中指(先端)：y", "左手中指(先端)：z","左手薬指(MP関節)：x", "左手薬指(MP関節)：y", "左手薬指(MP関節)：z", "左手薬指(PIP関節)：x", "左手薬指(PIP関節)：y", "左手薬指(PIP関節)：z" ,
            "左手薬指(DIP関節)：x", "左手薬指(DIP関節)：y", "左手薬指(DIP関節)：z", "左手薬指(先端)：x", "左手薬指(先端)：y", "左手薬指(先端)：z" , "左手小指(MP関節)：x", "左手小指(MP関節)：y", "左手小指(MP関節)：z",
            "左手小指(PIP関節)：x", "左手小指(PIP関節)：y", "左手小指(PIP関節)：z", "左手小指(DIP関節)：x", "左手小指(DIP関節)：y", "左手小指(DIP関節)：z", "左手小指(先端)：x", "左手小指(先端)：y", "左手小指(先端)：z" ,"スタンプ"};
        string s4 = string.Join(",", s3);
        swL.WriteLine(s4);

        swZentai = new StreamWriter(@"SaveData_Zentai.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s5 = {"時間(秒)", "左手絶対座標：x", "左手絶対座標：y", "左手絶対座標：z" , "右手絶対座標：x", "右手絶対座標：y", "右手絶対座標：z", "スタンプ"};
        string s6 = string.Join(",", s5);
        swZentai.WriteLine(s6);//11/22
    }

    public void SaveDataR(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6,string txt7, string txt8, string txt9, 
        string txt10, string txt11, string txt12, string txt13, string txt14, string txt15, string txt16, string txt17, string txt18,
        string txt19, string txt20, string txt21, string txt22, string txt23, string txt24, string txt25, string txt26, string txt27,
        string txt28, string txt29, string txt30, string txt31, string txt32, string txt33, string txt34, string txt35, string txt36,
        string txt37, string txt38, string txt39, string txt40, string txt41, string txt42, string txt43, string txt44, string txt45,
        string txt46, string txt47, string txt48, string txt49, string txt50, string txt51, string txt52, string txt53, string txt54,
        string txt55, string txt56, string txt57, string txt58, string txt59, string txt60, string txt61, string txt62, string txt63, string txt64, string txt65)
    {
        string[] s1 = {txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, txt13, txt14, txt15, txt16, txt17, txt18,
        txt19, txt20, txt21, txt22, txt23, txt24, txt25, txt26, txt27, txt28, txt29, txt30, txt31, txt32, txt33, txt34, txt35, txt36,
        txt37, txt38, txt39, txt40, txt41, txt42, txt43, txt44, txt45, txt46, txt47, txt48, txt49, txt50, txt51, txt52, txt53, txt54,
        txt55, txt56, txt57, txt58, txt59, txt60, txt61, txt62, txt63, txt64, txt65};
        string s2 = string.Join(",", s1);
        swR.WriteLine(s2);
    }

    public void SaveDataL(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6, string txt7, string txt8, string txt9,
        string txt10, string txt11, string txt12, string txt13, string txt14, string txt15, string txt16, string txt17, string txt18,
        string txt19, string txt20, string txt21, string txt22, string txt23, string txt24, string txt25, string txt26, string txt27,
        string txt28, string txt29, string txt30, string txt31, string txt32, string txt33, string txt34, string txt35, string txt36,
        string txt37, string txt38, string txt39, string txt40, string txt41, string txt42, string txt43, string txt44, string txt45,
        string txt46, string txt47, string txt48, string txt49, string txt50, string txt51, string txt52, string txt53, string txt54,
        string txt55, string txt56, string txt57, string txt58, string txt59, string txt60, string txt61, string txt62, string txt63, string txt64, string txt65)
    {
        string[] s3 = { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, txt13, txt14, txt15, txt16, txt17, txt18,
        txt19, txt20, txt21, txt22, txt23, txt24, txt25, txt26, txt27, txt28, txt29, txt30, txt31, txt32, txt33, txt34, txt35, txt36,
        txt37, txt38, txt39, txt40, txt41, txt42, txt43, txt44, txt45, txt46, txt47, txt48, txt49, txt50, txt51, txt52, txt53, txt54,
        txt55, txt56, txt57, txt58, txt59, txt60, txt61, txt62, txt63, txt64, txt65};
        string s4 = string.Join(",", s3);
        swL.WriteLine(s4);
    }

    public void SaveData_Zentai(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6, string txt7, string txt8)
    {
        string[] s5 = { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8};
        string s6 = string.Join(",", s5);
        swZentai.WriteLine(s6);//11/22
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            swR.Close();
            R1 = 1;
            swL.Close();
            L1 = 1;
            swZentai.Close();
            Zen1 = 1;
            Debug.Log("finish");
        }
    }
}
