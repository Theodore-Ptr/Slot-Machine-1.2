using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class contains all the components of reel (SR principle attempt)
/// </summary>
public class Reel : MonoBehaviour
{
    #region Fields
    //Slots params
    [SerializeField] List<SlotData> slotsData;

    //tweener support
    private ReelTweener tweener;
    #endregion

    #region MonoBehaviour Methods
    void Start()
    {
        tweener = GetComponent<ReelTweener>();
    }
    #endregion

    #region public Methods
    // Runs reelTweener
    public void StartSpeenining()
    {
        if (!tweener.IsStarted)
            tweener.TweenSlots(slotsData);
    }
    #endregion
}
