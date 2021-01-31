using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using EventObjects;
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
        _cg.alpha = Mathf.Lerp(_cg.alpha, targetAlpha, 5f * Time.deltaTime);
    }
}
