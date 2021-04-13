using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slot Data", menuName = "Slot Data")]
public class SlotData : ScriptableObject
{
    #region Fields 
    [SerializeField]
    private string slotName;
    [SerializeField]
    private int points;
    [SerializeField]
    private Sprite artWork;
    [SerializeField]
    #endregion

    #region Properties

    private RectTransform rectTransform = null;
    public string SlotName { get { return slotName; } }

    public Sprite ArtWork { get { return artWork; } set { artWork = value; }}

    public int Points { get { return points; } }

    public RectTransform RectTransform { get => rectTransform; set => rectTransform = value; }
    #endregion
}
