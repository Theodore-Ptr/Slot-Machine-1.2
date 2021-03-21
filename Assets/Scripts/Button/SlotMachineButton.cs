using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineButton : IButton
{
    public static event Action ButtonPressed = delegate { };
    public void OnButtonClicked()
    {
            ButtonPressed();
    }
}
