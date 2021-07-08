using UnityEngine;

public class BlackScreenUI : MonoBehaviour
{
    public static BlackScreenUI Instance { private set; get; }
    Animation BlackScreen;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");
    }
    private void Start() { BlackScreen = gameObject.GetComponent<Animation>(); }
    public void Enable() { BlackScreen.Play("BlackScreen"); }
    public void Disable() { BlackScreen.Play("BlackScreenOff"); }
}
