using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    //Single responsibility attempt
    private ReelTweener reelTweener;
    //private ReelActivator reelActivator;
    bool isStarted = false;
    
    //Scriptable objects that contain slot information
    [SerializeField] List<SlotData> data;

    private void Awake()
    {
        reelTweener = GetComponent<ReelTweener>();//собственно получение скрипта- вращателя (и запуск( пока так))
    }
    void Start()
    {
        isStarted = true;
    }

   
    void Update()
    {
    }
}
