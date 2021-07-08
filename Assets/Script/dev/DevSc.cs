using UnityEngine;

public class DevSc : MonoBehaviour
{
    [SerializeField]
    PlayerPKey.langKey language;
    void Awake()
    {
        PlayerPrefs.SetInt(PlayerPKey.LANGUAGE, (int)language);

        PlayerPrefs.Save();
    }

}
