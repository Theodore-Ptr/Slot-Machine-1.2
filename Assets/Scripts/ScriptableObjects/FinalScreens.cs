using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Final Screen", menuName = "Final Screen")]
public class FinalScreen: ScriptableObject
{
    #region Fields
    [SerializeField] private List<SlotData> finalScreens;
    #endregion

    #region Properties
    public List<SlotData> FinalScreens => finalScreens;
    #endregion
}
