using System.Collections;
using System.Collections.Generic;
using EventObjects;
using UnityEngine;
using UnityEngine.UI;

public class CursorBehaviour : MonoBehaviour
{
    public BoolWithEvent RaycastingObject;
    public BoolWithEvent GrabbingObject;
    public SpriteWithEvent CursorSprite;
    public BoolWithEvent InDialogue;
    
    private Image CursorImage;
    private CanvasGroup _cg;
    private float targetAlpha;


    void Awake()
    {
        CursorImage = GetComponent<Image>();
        _cg = GetComponent<CanvasGroup>();

    }



    void Update()
    {
        if (RaycastingObject.Value && !GrabbingObject.Value && !InDialogue.Value)
        {
            CursorImage.sprite = CursorSprite.Value;
            targetAlpha = 1f;
        }
        else
        {
            targetAlpha = 0;
        }

        _cg.alpha = Mathf.Lerp(_cg.alpha, targetAlpha, 22f*Time.deltaTime);
    }
}
