using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mediator pattern Implemintation
/// </summary>
public class EventManager : MonoBehaviour
{
    #region Fields
    //Storage support
    static List<SlotMachineButton> reelStartInvokers = new List<SlotMachineButton>();
    static List<UnityAction> reelStartListeners = new List<UnityAction>();
    #endregion

    #region Public Methods
    // Adds the given script as a  reel start invoker
    public static void AddReelStartInvoker(SlotMachineButton invoker)
    {
        // add invoker to list and add all listeners to invoker
        reelStartInvokers.Add(invoker);
        foreach (UnityAction listener in reelStartListeners)
        {
            invoker.AddReelStartListener(listener);
        }
    }

    // Adds the given method as a reel start listener
    public static void AddReelStartListener(UnityAction listener)
    {
        // add listener to list and to all invokers
        reelStartListeners.Add(listener);
        foreach (SlotMachineButton invoker in reelStartInvokers)
        {
            invoker.AddReelStartListener(listener);
        }
    }
    #endregion
}
