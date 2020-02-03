using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        GameManager.instance.PlaySelectSound();
        FadeOutSceneChange.instance.FadeOut("RepairScene", 0.5f);
    }

    public void CreditsButton()
    {
        GameManager.instance.PlaySelectSound();
        FadeOutSceneChange.instance.FadeOut("CreditsScene", 0.5f);
    }

    public void QuitButton()
    {
        GameManager.instance.PlaySelectSound();
        Application.Quit();
    }
}
