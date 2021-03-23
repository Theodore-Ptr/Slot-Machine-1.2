using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// perhaps, a terrible solution, however
/// I can not make games without it yet
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Fields
    //Reel support
    [SerializeField] Reel[] reels;
    [SerializeField] float minReelStartDelayTime = 0.2f;
    [SerializeField] float maxReelStartDelayTime = 0.5f;
    #endregion

    #region MonoBehaviour Methods
    //Proceeds Game Initialization
    private void Start()
    {
        EventManager.AddReelStartListener(StartSlotsRotation);
    }
    #endregion

    #region Private Methods
    //Starts The Coroutine
    private void StartSlotsRotation()
    {
        StartCoroutine(StartReels());
    }

    //Responsible For tweening all the slots
    private IEnumerator StartReels()
    {
        foreach(Reel reel in reels) {
            //Allow The Reels To Spin For A Random Amout Of Time Then Stop Them
            reel.StartSpeenining();
            yield return new WaitForSeconds(Random.Range(minReelStartDelayTime, maxReelStartDelayTime));
        }
    }
    #endregion
}
