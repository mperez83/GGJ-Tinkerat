using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutSceneChange : MonoBehaviour
{
    public static FadeOutSceneChange instance;
    public Image fadeOverlay;
    bool fadingOut;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void FadeOut(string sceneToChangeTo, float fadeTime)
    {
        if (!fadingOut)
        {
            fadingOut = true;

            LeanTween.value(gameObject, 0, 1, fadeTime).setOnUpdate((float value) =>
            {
                fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, value);
            }).setOnComplete(() =>
            {
                SceneManager.LoadScene(sceneToChangeTo);
                FadeIn(fadeTime);
            });
        }
    }

    void FadeIn(float fadeTime)
    {
        LeanTween.value(gameObject, 1, 0, fadeTime).setOnUpdate((float value) =>
        {
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, value);
        }).setOnComplete(() =>
        {
            fadingOut = false;
        });
    }
}
