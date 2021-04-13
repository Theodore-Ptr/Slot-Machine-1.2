using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    //reel support
    [SerializeField] private List<ReelTweener> reels;
    [SerializeField] [Range(0.05f, 0.3f)] private float minDelay;
    [SerializeField] [Range(0.3f,0.55f)] private float maxDelay;
    private List<float> delayList;

    //slots info update support
    [SerializeField] private List<SlotData> slotsData;
    public List<SlotData> SlotsData => slotsData;
    [SerializeField] private List<FinalScreen> finalScreens;
    private int finalScreenCounter;
    private int finalElementCounter;

    //buttons support
    [SerializeField] private StopButton stopButton;
    [SerializeField] private StartButton startButton;

    private IEnumerator coroutine;
    //coroutine support
    
    #endregion

    void Awake()
    {
        delayList = new List<float>();
        for (int i = 0; i < reels.Count; i++)
        {
            delayList.Add(Random.Range(minDelay, maxDelay));
        }
    }

    //функция для события кнопки "Start"
    public void OnPlayButtonDown()
    {
        coroutine = StartingCoroutine();
        StartCoroutine(coroutine);
    }

    //функция для события кнопки "Stop"
    public void OnStopButtonDown()
    {
        if (!reels[reels.Count - 1].IsStopping)
        {
            StopCoroutine(coroutine);
            StartCoroutine(StoppingCourutine());
        }
    }

    //функция, позволяющая получить подходяший элемент финального экрана
    public SlotData GetFinalScreenElement()
    {
        if (finalElementCounter > finalScreens[finalScreenCounter].FinalScreens.Count - 1)
        {
            finalScreenCounter++;
            finalElementCounter = 0;
        }
        if(finalScreenCounter > finalScreens.Count-1)
        {
            finalScreenCounter = 0;
        }
        return finalScreens[finalScreenCounter].FinalScreens[finalElementCounter++];
    }

    //корутина отвечает за начало вращения
    private IEnumerator StartingCoroutine()
    {
        startButton.setPassive();
        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].StartReel();
            yield return new WaitForSeconds(delayList[i]);
        }
        yield return new WaitForSeconds(reels[reels.Count-1].BoostingTime);
        stopButton.SetActive();
        yield return new WaitForSeconds(reels[reels.Count - 1].AnimationTime + reels[reels.Count - 1].StoppingTime - 0.001f);
        stopButton.setPassive();
        yield return new WaitForSeconds(0.001f);
        startButton.SetActive();
    }

    //корутина отвечает за остановку
    private IEnumerator StoppingCourutine()
    {
        stopButton.setPassive();
        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].StopReel();
            yield return new WaitForSeconds(delayList[i]);
        }
        startButton.SetActive();
    }
}
