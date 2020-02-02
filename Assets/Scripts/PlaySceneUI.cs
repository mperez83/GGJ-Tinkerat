using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneUI : MonoBehaviour
{
    public RectTransform instructions;

    void Start()
    {
        LeanTween.moveLocalX(instructions.gameObject, 0, 1f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            LeanTween.delayedCall(instructions.gameObject, 2f, () =>
            {
                LeanTween.moveLocalX(instructions.gameObject, 550, 1f).setEase(LeanTweenType.easeInCubic).setDestroyOnComplete(true);
            });
        });
    }
}
