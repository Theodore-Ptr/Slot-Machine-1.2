using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReelTweener : MonoBehaviour
{

    #region Fields

    //dependency injection attempt
    [SerializeField] private GameManager gameManager;
    private RectTransform rectTransform;
    private List<RectTransform> slots;

    //reel update support
    private float realOffset;
    private float startingPosition;
    private int iteratonsCount;
    private float currentIterOffset;
    private Tweener mainTweener;
    private float iterationTime;

    //boosting support
    [SerializeField] private Ease boostingEase;
    [SerializeField] private float boostingTime;
    [SerializeField] private int boostingOffsets;

    //cruising support
    [SerializeField] private float animationTime;
    [SerializeField] private int offsetAmount;

    //stopping support
    [SerializeField] private Ease stoppingEase;
    [SerializeField] private float stoppingTime;
    [SerializeField] private int stoppingOffsets;

    [SerializeField] private float slotHeight;

    public bool IsStarted { get; private set; } = false;
    public bool IsStopping { get; private set; } = false;

    #endregion

    #region Properties
    public float BoostingTime => boostingTime;
    public float AnimationTime => animationTime;
    public float StoppingTime => stoppingTime;

    #endregion

    #region Unity Methods
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.localPosition.y;
        slots = new List<RectTransform>();
        foreach (RectTransform slot in rectTransform)
        {
            slots.Add(slot);
        }
    }

    void Update()
    {
        if (IsStarted)
            iterationTime += Time.deltaTime;
        CalculateOffset();
        if (currentIterOffset >= slotHeight)
        {
            DragSlot();
        }
    }
    #endregion

    #region Public Methods

    public void StopReel()
    {
        if (!IsStopping)
        {
            mainTweener.Kill();
            float deltaDist = (slotHeight + realOffset % slotHeight);
            float currentSpeed = boostingEase.CountDerivative(boostingTime) * slotHeight * boostingOffsets;
            float deltaTime = deltaDist / currentSpeed;
            //доводим до конца кадра(слота) с линейной скоростью
            mainTweener = rectTransform.DOLocalMoveY(startingPosition + (realOffset - deltaDist), deltaTime)
                .OnComplete(OnCruiseComplete);
        }
    }

    public void StartReel()
    {
        if (!IsStarted)
        {
            IsStarted = true;
            mainTweener = rectTransform.DOLocalMoveY(startingPosition - boostingOffsets * slotHeight, boostingTime)
                .SetEase(boostingEase).OnComplete(OnBoostComplete);
        }
    }

    #endregion

    #region Private Methods

    private void OnBoostComplete()
    {
        Debug.Log("Running");
        //stopButton.SetActive();
        PrepareForNextTween();
        mainTweener = rectTransform.DOLocalMoveY(startingPosition - offsetAmount * slotHeight, animationTime)
            .SetEase(Ease.Linear).OnComplete(OnCruiseComplete);
    }

    private void OnCruiseComplete()
    {
        Debug.Log("Stopping");
        IsStopping = true;
        PrepareForNextTween();
        mainTweener = rectTransform.DOLocalMoveY(startingPosition - stoppingOffsets * slotHeight, stoppingTime)
            .SetEase(stoppingEase).OnComplete(OnStopComplete);
        
    }

    private void OnStopComplete()
    {
        PrepareForNextTween();
        iterationTime = 0;
        IsStarted = false;
        IsStopping = false;
    }

    private void DragSlot()
    {
        iteratonsCount++;
        var bottomSymbol = slots.OrderBy(x => x.localPosition.y).First();
        if (IsStopping && stoppingOffsets - iteratonsCount <= 3)
            bottomSymbol.GetComponent<Image>().sprite = gameManager.GetFinalScreenElement().ArtWork;
        else
            bottomSymbol.GetComponent<Image>().sprite = gameManager.SlotsData[Random.Range(0, 8)].ArtWork;
        bottomSymbol.localPosition += new Vector3(0, slots.Count * slotHeight, 0);
    }

    private void CalculateOffset()
    {
        realOffset = rectTransform.localPosition.y - startingPosition;
        currentIterOffset = -realOffset - iteratonsCount * slotHeight;
    }

    private void PrepareForNextTween()
    {
        var bottomSymbol = slots.OrderBy(x => x.localPosition.y).First();
        bottomSymbol.localPosition += new Vector3(0, slots.Count * slotHeight, 0);
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, startingPosition);
        foreach (var slot in slots)
        {
            slot.localPosition -= new Vector3(0, slotHeight * (iteratonsCount + 1));
        }
        realOffset = 0;
        iteratonsCount = 0;
        currentIterOffset = 0;
    }

    #endregion
}
