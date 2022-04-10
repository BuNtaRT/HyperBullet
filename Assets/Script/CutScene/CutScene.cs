using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    PlayableDirector _director    ;
    [SerializeField]
    GameObject       _mapContainer;
    Transform        _road        ;
    Transform        _light       ;
    [SerializeField]
    Transform        _builds      ;


    [SerializeField]
    GameObject _camera;
    [SerializeField]
    GameObject _player;

    public static CutScene Instance;

    void Awake()
    {
        if (Instance != null)
            throw new Exception("CutScene install over 1");
        else
            Instance = this;
    }

    void Start()
    {
        Transform loadetMap = _mapContainer.transform.GetChild(0);

        foreach (Transform temp in loadetMap.transform)
        {
            if (temp.name.ToLower().Contains("map"))
                _builds = temp;
            if (temp.name.ToLower().Contains("light"))
                _light = temp;
            if (temp.name.ToLower().Contains("road"))
                _road = temp;
        }
    }

    public Transform GetMapContainer() => _mapContainer.transform;

    public void Show(PlayableAsset entryPlay, bool showMap) 
    {
        Show(entryPlay, null, showMap);
    }

    public void Show(PlayableAsset entryPlay,Action callback = null, bool showMap = true)
    {
        _director.playableAsset = entryPlay;
        StartCoroutine(EndScene(entryPlay.duration,callback, showMap));
    }


    IEnumerator EndScene(double time, Action callback, bool showMap) 
    {
        ChangeActiveObject(false);

        Debug.Log(_builds.gameObject.name);
        _builds.gameObject.SetActive(showMap);
        _director.gameObject.SetActive(true);
        //_director.Play();
        GlobalEventsManager.InvokPause(PauseStatus.cutScene,true);
        yield return new WaitForSecondsRealtime((float)time);
        GlobalEventsManager.InvokPause(PauseStatus.cutScene, false);

        _director.gameObject.SetActive(false);
        if (callback != null) 
        {
            callback.Invoke();
        }
        _builds.gameObject.SetActive(false);

        ChangeActiveObject(true);
    }

    void ChangeActiveObject(bool enable) 
    {
        _camera.GetComponent<CinemachineBrain>().enabled = enable;
        _player.SetActive(enable);
    }
}
