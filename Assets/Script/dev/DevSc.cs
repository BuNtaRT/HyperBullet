using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSc : MonoBehaviour
{
    [SerializeField]
    PlayerPKey.langKey language;
    void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.Language, (int)language);

        PlayerPrefs.Save();
    }

}
