using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slot Data", menuName = "Slot Data")]
public class SlotData : ScriptableObject
{
    [SerializeField]
    string slotName;
    [SerializeField]
    int points;
    [SerializeField]
    Sprite artWork;

    public string SlotName { get { return slotName; } }

    public Sprite ArtWork { get { return artWork; } set { artWork = value; }}

    public int Points { get { return points; } }

}
