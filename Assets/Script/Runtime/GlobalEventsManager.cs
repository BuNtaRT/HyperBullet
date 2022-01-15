using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventsManager : MonoBehaviour
{
    public static UnityEvent<SpellSO> OnGetSpell = new UnityEvent<SpellSO>();
    public static void SendSpell(SpellSO spell) => OnGetSpell.Invoke(spell);
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent OnGetPerk = new UnityEvent();
    public static void CallPerk() => OnGetPerk.Invoke();
    //---------------------------------------------------------------------------------------------------
    public static UnityEvent<Transform> OnEnemyKill = new UnityEvent<Transform>();
    public static void SendEnemyKill(Transform pos) => OnEnemyKill.Invoke(pos);
    //---------------------------------------------------------------------------------------------------
    static bool _pauseStatus = false;
    static float _lastTimeScale = 1f;
    public static UnityEvent<PauseStatus,bool> OnPause = new UnityEvent<PauseStatus,bool>();
    public static void InvokPause(PauseStatus status, bool enable)
    {
        if (enable != _pauseStatus)
        {
            _pauseStatus = enable;
            if (enable)
                _lastTimeScale = Time.timeScale;

            Time.timeScale = enable ? 0f : _lastTimeScale;
            OnPause.Invoke(status, enable);
        }
    }
    //---------------------------------------------------------------------------------------------------

}

