using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamera : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    int width = 1920;
    int height = 1080;
    int fps = 30;
    WebCamTexture webcamTexture;
     void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
    //    //webcamTexture = new WebCamTexture(devices[1].name, this.width, this.height, this.fps);
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        Debug.Log(WebCamTexture.devices.Length);
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
        Debug.Log(webcamTexture.isPlaying);
        Debug.Log(webcamTexture.deviceName);
        //if(webcamTexture.isPlaying == false) webcamTexture.Play();
        //DontDestroyOnLoad(GameObject);
    }
    // public void OnClickCamera()
    // {
    //     WebCamDevice[] devices = WebCamTexture.devices;
    //     //webcamTexture = new WebCamTexture(devices[1].name, this.width, this.height, this.fps);
    //     webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
    //     GetComponent<Renderer>().material.mainTexture = webcamTexture;
    //     webcamTexture.Play();
    // }
    void Update(){
        if(webcamTexture.isPlaying == false) webcamTexture.Play();
    }
}
