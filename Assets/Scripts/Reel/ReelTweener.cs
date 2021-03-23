using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This class is completely responsible for tweening
/// </summary>
public class ReelTweener : MonoBehaviour
{
    #region Fields
    //Parent Rotation support
    RectTransform parentTransform;
    //Child iteration support
    List<RectTransform> slotsTransform;

    //Looping support
    [SerializeField] int offsetAmmount;
    [SerializeField] float oneLoopTime;
    private bool isStarted = false;
    private int loopsEllapsed = 0;
    private int initialPosition = 35;
    private int destinationPoint = -35;

    //temporary decision for Slot randomizing
    Sprite upperColor;
    Sprite tempColor;

    //Slot randomizing support
    int rand;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        parentTransform = GetComponent<RectTransform>();
        slotsTransform = new List<RectTransform>();
        foreach (RectTransform slot in parentTransform)
        {
            slotsTransform.Add(slot);
        }
    }
    #endregion

    #region Public Methods
    public bool IsStarted
    {
        get { return isStarted; }
    }

    public void TweenSlots(List<SlotData> slotData)
    {
        isStarted = true;
        var tween = parentTransform.DOAnchorPosY(destinationPoint, oneLoopTime).SetLoops(offsetAmmount).SetEase(Ease.Linear);
        tween.OnStepComplete(() => PerformChanges(slotData, tween));
        tween.OnComplete(() => PrapareForNextIteration(tween));
    }
    #endregion

    #region Private Methods

    // This method is called on DOTween loop Iteration finished
    private void PerformChanges(List<SlotData> slotData, Tweener tweener)
    {   
        loopsEllapsed++;
        DragSlots(slotData);

        ChangeScale(tweener);
    }

    //this methods updates reel's slots
    private void DragSlots(List<SlotData> slotData)
    {
         upperColor = slotsTransform[0].gameObject.GetComponent<Image>().sprite;
         rand = Random.Range(0, 8);
         slotsTransform[0].gameObject.GetComponent<Image>().sprite = slotData[rand].ArtWork;

         for (int i = 1; i < 4; i++)
         {
             tempColor = slotsTransform[i].gameObject.GetComponent<Image>().sprite;
             slotsTransform[i].gameObject.GetComponent<Image>().sprite = upperColor;
             upperColor = tempColor;
         }
    }

    //this methods slows don and speeds up the reel
    private void ChangeScale(Tweener tweener)
    {
        if (loopsEllapsed < offsetAmmount / 2)
            tweener.timeScale += 0.5f;
        else if (loopsEllapsed > offsetAmmount / 2)
        {
            tweener.timeScale -= 0.5f;
        }
    }

    //this method is called when DOTween is finished
    private void PrapareForNextIteration(Tweener tweener)
    {
        parentTransform.localPosition = new Vector3(parentTransform.localPosition.x, initialPosition, parentTransform.localPosition.z);
        loopsEllapsed = 0;
        isStarted = false;
        tweener.Kill();
    }
#endregion
}