using System;
using UnityEngine;
using UnityEngine.Events;

public class StartButton : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
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
