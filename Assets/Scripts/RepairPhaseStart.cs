using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairPhaseStart : MonoBehaviour
{
    public Image fadeOverlay;
    public Button startButton;
    public RectTransform leftSide;
    public RectTransform rightSide;
    public AudioSource audioSource;

    void Start()
    {
        LeanTween.delayedCall(gameObject, 0.75f, () =>
        {
            LeanTween.moveLocalX(leftSide.gameObject, -250, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                LeanTween.delayedCall(gameObject, 0.5f, () =>
                {
                    LeanTween.moveLocalX(rightSide.gameObject, 225, 1).setEase(LeanTweenType.easeOutCubic);
                });
            });
        });
    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        GameManager.instance.PlaySelectSound();
        startButton.gameObject.SetActive(false);

        LeanTween.cancel(gameObject);
        Destroy(leftSide.gameObject);
        Destroy(rightSide.gameObject);

        LeanTween.value(gameObject, fadeOverlay.color.a, 0, 1).setEase(LeanTweenType.easeInQuad).setOnUpdate((float value) =>
        {
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, value);
        }).setOnComplete(() =>
        {
            Destroy(fadeOverlay.transform.parent.gameObject);
            GetComponent<RepairPhaseHandler>().enabled = true;
            Destroy(this);
        });
    }

    public void Boink()
    {
        audioSource.PlayOneShot(GetComponent<RepairPhaseHandler>().nibbleSound);
    }
}
