using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private SlotData slotData;
    private void Awake()
    {
        slotData.RectTransform = GetComponent<RectTransform>();
    }
    public SlotData SlotData { get => slotData; set => slotData = value; }

    public void UpdateInfo()
    {
        GetComponent<Image>().sprite = slotData.ArtWork;
    }
}
