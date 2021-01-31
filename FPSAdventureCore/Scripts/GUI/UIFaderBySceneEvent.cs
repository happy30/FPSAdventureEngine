using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaderBySceneEvent : MonoBehaviour
{
    public bool FadeIn;
    public float Speed;
    private CanvasGroup _cg;
    public GameObject InactiveAtZero;

    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void FadeUI()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        if (!FadeIn)
        {
            _cg.alpha = 1;
            while (_cg.alpha > 0)
            {
                _cg.alpha -= Speed * Time.deltaTime;
                yield return null;
            }

            if (InactiveAtZero != null) InactiveAtZero.SetActive(false);
        }
        else
        {
            while (_cg.alpha < 1)
            {
                _cg.alpha += Speed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
