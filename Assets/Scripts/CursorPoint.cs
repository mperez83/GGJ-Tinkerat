using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    bool triggered;
    [HideInInspector]
    public CursorPointSpawner cursorPointSpawner;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (!triggered)
        {
            triggered = true;
            cursorPointSpawner.CursorPointTriggered();
            LeanTween.scale(gameObject, transform.localScale * 1.5f, 0.25f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.alpha(gameObject, 0, 0.25f).setDestroyOnComplete(true);
        }
    }
}
