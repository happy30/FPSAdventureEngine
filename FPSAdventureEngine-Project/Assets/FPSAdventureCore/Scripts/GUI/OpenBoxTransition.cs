using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class OpenBoxTransition : MonoBehaviour
{
    private CanvasGroup _cg;
    public SceneEvent MakeDioramaAppear;


    void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void Transition()
    {
        StartCoroutine(FadeTransition());
    }

    IEnumerator FadeTransition()
    {
        while (_cg.alpha < 1)
        {
            _cg.alpha += Time.deltaTime;
            yield return null;
        }
        
        MakeDioramaAppear.Raise();
        yield return new WaitForSeconds(1);
        
        while (_cg.alpha > 0)
        {
            _cg.alpha -= Time.deltaTime;
            yield return null;
        }
        
    }

}
