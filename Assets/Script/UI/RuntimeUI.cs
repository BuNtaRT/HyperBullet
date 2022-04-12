using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RuntimeUI : MonoBehaviour
{
    [SerializeField]
    Text      _messageText;
    [SerializeField]
    StartLogo _startLogo;

    Queue<string> _messageQueue  = new Queue<string>();
    bool          _isShowMessNow = false              ;
    Color         _mesC = new Color()                 ;
    Color         _mesCA = new Color()                ;

    public static RuntimeUI Instance;

    private void Awake()
    {
        if (Instance != null)
            throw new System.Exception("RuntimeUI instance over 1");
        else
            Instance = this;
        _mesC  = _messageText.color;
        _mesCA = new Color(_mesC.r, _mesC.g, _mesC.b, 0);
        _messageText.color = _mesCA;


    }

    #region Message
    public void ShowMessage(string mes) 
    {

        _messageQueue.Enqueue(mes);
        if (!_isShowMessNow)
            StartCoroutine(MessageShower());
    }

    private IEnumerator MessageShower()
    {
        _messageText.gameObject.SetActive(true);
        _isShowMessNow = true;

        do
        {
            _messageText.DOColor(_mesC, 0.5f).SetUpdate(true);
            _messageText.text = _messageQueue.Dequeue();

            yield return new WaitForSecondsRealtime(3.5f);
            _messageText.DOColor(_mesCA, 0.25f).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.25f);

        } while (_messageQueue.Count != 0);

        _isShowMessNow = false;
        _messageText.gameObject.SetActive(false);
    }
    #endregion

    public void ShowStartLogo() 
    {
        _startLogo.Show();
    }
}
