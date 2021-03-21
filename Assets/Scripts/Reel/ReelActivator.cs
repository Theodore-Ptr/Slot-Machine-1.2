using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelActivator : MonoBehaviour
{
    private bool isActive;

    public bool IsActive 
    {
        get { return isActive; }
        set { isActive = value; }
    }


    void Start()
    {
        isActive = false;
    }
}
