using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public Image Background;
    public TextMeshProUGUI ChoiceText;

    private float _targetAmount;
    private float _initialAmount = 0.013f;

    public Vector3 HoverScale;
    Vector3 _targetScale;

    public SceneEvent SelectChoice;
    
    
    public float FillSpeed;

    void Awake()
    {
        _targetAmount = _initialAmount;
        _targetScale = Vector3.one;
    }


    public void MouseOver()
    {
        _targetAmount = 1;
        ChoiceText.color = Color.black;
        _targetScale = HoverScale;
    }

    public void MouseExit()
    {
        _targetAmount = _initialAmount;
        Background.fillAmount = _initialAmount;
        ChoiceText.color = Color.white;
        _targetScale = Vector3.one;
    }

    public void MouseClick()
    {
        MouseExit();
        SelectChoice.Raise();
    }
    
    void Update()
    {
        Background.fillAmount = Mathf.Lerp(Background.fillAmount, _targetAmount, FillSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, FillSpeed * Time.deltaTime);
    }
}
