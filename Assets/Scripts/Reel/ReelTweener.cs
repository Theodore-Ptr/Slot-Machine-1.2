using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelTweener : MonoBehaviour
{
    private List<RectTransform> slots;

    //Updating Reel support
    [SerializeField] int loopsAmount = 10;
    private int offsetElapsed = 0; //кол-во смещений
    private int actualIndex = 0; //инлдекс просматриваемого в списке элемента

    //
    private float toMovePosition = 70f; //куда изначально собираемся двигать
    private Color32 upperColor; //цвет вышестоящего элемента

    void Start()
    {
        //формируем первоначальный список
        slots = new List<RectTransform>();
        foreach (RectTransform slot in GetComponent<RectTransform>())
        {
            slots.Add(slot);
        }

        //водораздел пройдёт здесь
        foreach (RectTransform slot in GetComponent<RectTransform>())
        {
            Tweener tween = slot.DOAnchorPosY(toMovePosition, 0.4f).SetLoops(loopsAmount).SetEase(Ease.Linear);
            tween.OnStepComplete(() => MyCallback(tween));
            toMovePosition -= 70;
        }
    }
    private void MyCallback(Tween tween)
    {
        if (offsetElapsed < loopsAmount * 4 - 4)
        {
            ++offsetElapsed;
            //если это слот, находящийся в самом верху(вне колеса)
            if (actualIndex == 0)
                InstantiateNewSlot();
            //если этот слот находится в колесе
            else 
                UnifySlots();
        }
        if (offsetElapsed > loopsAmount * 4 - 4)
        {
            tween.Kill();
        }
    }

    //устанавливает слоту новый рандомный цвет
    private void InstantiateNewSlot()
    {
        upperColor = slots[0].gameObject.GetComponent<Image>().color;
        slots[0].gameObject.GetComponent<Image>().color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        actualIndex++;
    }


    //подтягиваем цвет элемента снизу
    private void UnifySlots()
    {
        Color tempColor = slots[actualIndex].gameObject.GetComponent<Image>().color;
        slots[actualIndex].gameObject.GetComponent<Image>().color = upperColor;
        upperColor = tempColor;
        actualIndex++;
        actualIndex %= 4;
    }
}
