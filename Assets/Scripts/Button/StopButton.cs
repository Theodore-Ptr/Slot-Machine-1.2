using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopButton : MonoBehaviour
{ 
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void setPassive()
    {
        gameObject.SetActive(false);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
