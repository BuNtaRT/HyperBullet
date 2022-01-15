using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionBullet : MonoBehaviour
{
    //Bullet manager on runtime lvl
    static public SessionBullet Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("SessionBullet have copy!!");
    }

    [SerializeField]
    int  _bullet = 0;
    [SerializeField]
    Text _bulletText;


    public void InitBullet(float bullet) 
    {
        _bullet          = (int)(bullet * 2);
        _bulletText.text = _bullet.ToString();
    }

    public void Add(float addBullet) 
    {
        _bullet += (int)(addBullet * 2);
    }

    public bool WriteOff(float minusB) 
    {
        Debug.Log("Ballance write-off minus="+ (int)(minusB * 2) + " ballance="+_bullet);

        int tempBullet = _bullet - (int)(minusB * 2);

        if (tempBullet >= 0)
        {
            _bullet          = tempBullet;
            _bulletText.text = _bullet.ToString();
            return true;
        }
        else 
        {
            return false;
        }
    }
}
