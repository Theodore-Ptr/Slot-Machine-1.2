using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DerivativeCounter
{
    public static float CountDerivative(this Ease ease, float time)
    {
        switch (ease)
        {
            case Ease.InQuad:
                return 2 * time;
            case Ease.InCubic:
                return 3 * time * time;
            case Ease.InExpo:
                return Mathf.Exp(time);
        }

        return 0;
    }

}
