using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReelTest : MonoBehaviour
{
    //Data containment support
    private List<RectTransform> slots;

    //Updating Reel support
    [SerializeField] int loopsAmount = 10;
    private int offsetElapsed = 0;
    private int actualCalls = 0;
    //
    float toMovePosition = 70f;
    Color32 upperColor;

    void Start()
    {
        
        slots = new List<RectTransform>();
        foreach (RectTransform slot in GetComponent<RectTransform>())
        {
            slots.Add(slot);
        }

        //водораздел пройдёт здесь
        foreach(RectTransform slot in GetComponent<RectTransform>())
        {
            Tweener tween = slot.DOAnchorPosY(toMovePosition, 0.4f).SetLoops(loopsAmount).SetEase(Ease.Linear);
            tween.OnStepComplete(()=>MyCallback(tween));
            toMovePosition -= 70;
        }
    }
    private void MyCallback(Tween tween)
    {
        if (offsetElapsed < loopsAmount * 4 - 4)
        {
            ++offsetElapsed;
            if (actualCalls == 0)
                InstantiateNewSlot();
            else
                UnifySlots();
        }
        if(offsetElapsed > loopsAmount * 4 - 4)
        {
            tween.Kill();
        }
    }

    private void InstantiateNewSlot()
    {
        upperColor = slots[0].gameObject.GetComponent<Image>().color;
        slots[0].gameObject.GetComponent<Image>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        actualCalls++;
    }

    private void UnifySlots()
    {
        Color tempColor = slots[actualCalls].gameObject.GetComponent<Image>().color;
        slots[actualCalls].gameObject.GetComponent<Image>().color = upperColor;
        upperColor = tempColor;
        actualCalls++;
        actualCalls %= 4;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
