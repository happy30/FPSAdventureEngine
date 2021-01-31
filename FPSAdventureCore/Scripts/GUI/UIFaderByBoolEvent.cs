using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using EventObjects;
using Klak.Math;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFaderByBoolEvent : MonoBehaviour
{
    public BoolWithEvent BoolEvent;

    CanvasGroup _cg;

    private float targetAlpha;

    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }


    void Update()
    {
        targetAlpha = BoolEvent.Value ? 1 : 0;
        _cg.alpha = ETween.Step(_cg.alpha, targetAlpha, 5f);
    }
}
