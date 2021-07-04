using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //скрипт весящий на Виртуальной камере - отвечает за тряску 

    public static CameraShake singl { get; private set; }
    float intensive = 1.2f;
    CinemachineVirtualCamera virtualCam;

    private void Start()
    {
        singl = this;
        virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float time) 
    {
        StartCoroutine(shakeIenum(time));
    }

    IEnumerator shakeIenum(float time) 
    {
        CinemachineBasicMultiChannelPerlin CMCP = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time;
            float progress = Mathf.Lerp(intensive,0,timeStep);
            CMCP.m_AmplitudeGain = progress;
            yield return null;
        }
    }
}
