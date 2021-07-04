using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    public Text buildNum;
    public Text fpsText;
    public Text fpsTotal;
    int total = 0;
    public int FPS = 60;
    public int vSync = 0;
    void Start()
    {
        if(buildNum!=null)
            buildNum.text = "Build " + Application.version;

        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = FPS;
    }

    int countUpdate = 0;
    int TempFps = 0;
    void Update()
    {
        countUpdate++;
        TempFps += (int)(1f / Time.unscaledDeltaTime);
        if (countUpdate >= 10)
        {
            FpsMetr();
        }


    }


    void FpsMetr()
    {
        total += 10;
        fpsText.text = "FPS: " + (TempFps / 10).ToString();
        TempFps = 0;
        countUpdate = 0;
    }

    public void FpsTotal() 
    {
        fpsTotal.text = "Total Fps: " + total + "\n Average Fps: " +  total/Time.time;
    }

}
