using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveNotificator : MonoBehaviour
{
    public CanvasGroup _cg;
    public Text Text;


    public void SetObjective(string txt)
    {
        Text.text = txt;
        _cg.alpha = 0;

        StartCoroutine(Notification());

    }

    IEnumerator Notification()
    {
        while (_cg.alpha < 1)
        {
            _cg.alpha += Time.deltaTime * 2f;
            yield return null;
        }
        
        yield return new WaitForSeconds(5f);
        
        while (_cg.alpha > 0)
        {
            _cg.alpha -= Time.deltaTime * 0.5f;
            yield return null;
        }
        
    }
    
    
}
