using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //скрипт весящий на Виртуальной камере - отвечает за тряску 

    public static CameraShake Instance { get; private set; }

    private float                    _intensive = 1.2f;
    private CinemachineVirtualCamera _virtualCam;

    private void Start()
    {
        Instance = this;
        _virtualCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float time) 
    {
        StartCoroutine(shakeIenum(time));
    }

    private IEnumerator shakeIenum(float time) 
    {
        CinemachineBasicMultiChannelPerlin CMCP = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / time;
            float progress = Mathf.Lerp(_intensive,0,timeStep);
            CMCP.m_AmplitudeGain = progress;
            yield return null;
        }
    }
}
