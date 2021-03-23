using System;
using UnityEngine;
using UnityEngine.Events;

public class SlotMachineButton : MonoBehaviour, IButton
{
    private ReelStartEvent reelStart;

    void Start()
    {
        reelStart = new ReelStartEvent();
        EventManager.AddReelStartInvoker(this);
    }

    #region PublicMethods
    public void OnButtonClicked()
    {
        reelStart.Invoke();
    }

    public void AddReelStartListener(UnityAction listener)
    {
        reelStart.AddListener(listener);
    }
    #endregion
}
