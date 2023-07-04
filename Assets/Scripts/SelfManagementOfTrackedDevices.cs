using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelfManagementOfTrackedDevices : MonoBehaviour
{
    public GameObject[] targetObjs;
    public ETrackedDeviceClass targetClass = ETrackedDeviceClass.GenericTracker;
    public KeyCode resetDeviceIds = KeyCode.Tab;

    public int stamp;

    double x0;
    double x1;
    double y0;
    double y1;
    double z0;
    double z1;

    GameObject SaveCsv;//csvに書き込む用
    CsvScript CsvScript;
    GameObject TimeCount;
    TimeCounter TimeCounter;
    //public Trasform posi[];

    CVRSystem _vrSystem;
    List<int> _validDeviceIds = new List<int>();

    void Start()
    {
        var error = EVRInitError.None;
        _vrSystem = OpenVR.Init(ref error, EVRApplicationType.VRApplication_Other);

        if (error != EVRInitError.None) { Debug.LogWarning("Init error: " + error); }

        else
        {
            Debug.Log("init done");
            foreach (var item in targetObjs) { item.SetActive(false); }
            SetDeviceIds();
        }

        //  ファイルに保存する用
        SaveCsv = GameObject.Find("SaveCsv");//SavaCsvを参照する
        CsvScript = SaveCsv.GetComponent<CsvScript>();
        TimeCount = GameObject.Find("TimeCount");
        TimeCounter = TimeCount.GetComponent<TimeCounter>();
        stamp = 1;
    }

    void SetDeviceIds()
    {
        _validDeviceIds.Clear();
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            var deviceClass = _vrSystem.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid && deviceClass == targetClass)
            {
                Debug.Log("OpenVR device at " + i + ": " + deviceClass);
                _validDeviceIds.Add((int)i);
                targetObjs[_validDeviceIds.Count - 1].SetActive(true);
            }
        }
    }

    void UpdateTrackedObj()
    {
        TrackedDevicePose_t[] allPoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];

        _vrSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, allPoses);

        //double x0;
        //double x1;
        //double y0;
        //double y1;
        //double z0;
        //double z1;
        //var[] position;
        //double[] y;
        //double[] z;

        for (int i = 0; i < _validDeviceIds.Count; i++)
        {
            Debug.Log("aaa");
            if (i < targetObjs.Length)
            {
                var pose = allPoses[_validDeviceIds[i]];
                var absTracking = pose.mDeviceToAbsoluteTracking;
                var mat = new SteamVR_Utils.RigidTransform(absTracking);
                targetObjs[i].transform.SetPositionAndRotation(mat.pos, mat.rot);
                if (i == 0)
                {
                    x0 = mat.pos.x;
                    y0 = mat.pos.y;
                    z0 = mat.pos.z;
                    Debug.Log("AATracker" + i + "Position:" + x0);
                }
                else if (i == 1)
                {
                    x1 = mat.pos.x;
                    y1 = mat.pos.y;
                    z1 = mat.pos.z;
                    Debug.Log("AATracker" + i + "Position:" + x1);
                }
            }
        }
        //Debug.Log(posi1.x);
        if (TimeCounter.a == 1)
        {
            CsvScript.SaveData_Zentai(TimeCounter.countTime.ToString("F4"), x0.ToString(), y0.ToString(), z0.ToString(),
                x1.ToString(), y1.ToString(), z1.ToString(), stamp.ToString());
        }
    }

    void Update()
    {
        UpdateTrackedObj();
        Debug.Log("bbb");
        if (Input.GetKeyDown(resetDeviceIds))
        {
            SetDeviceIds();
            Debug.Log("ccc");
        }
    }

    public void OnClick()
    {
        stamp++;
    }
}