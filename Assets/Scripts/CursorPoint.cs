using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    bool triggered;
    [HideInInspector]
    public CursorPointSpawner cursorPointSpawner;
    public SpriteRenderer sr;
    Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
        transform.localScale = transform.localScale * 0.5f;
        LeanTween.scale(gameObject, baseScale, 0.15f).setEase(LeanTweenType.easeOutCubic);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        LeanTween.alpha(gameObject, 1, 0.15f).setEase(LeanTweenType.easeOutCubic);
    }

    void OnMouseOver()
    {
        if (!triggered)
        {
            triggered = true;
            LeanTween.cancel(gameObject);
            transform.localScale = baseScale;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

            cursorPointSpawner.CursorPointTriggered();
            LeanTween.scale(gameObject, baseScale * 1.5f, 0.15f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.alpha(gameObject, 0, 0.25f).setEase(LeanTweenType.easeOutCubic).setDestroyOnComplete(true);
        }
    }
}
