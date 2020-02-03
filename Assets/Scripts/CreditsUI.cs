using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public AudioSource taraAudio;
    public AudioSource michaelAudio;

    public void MainMenuButton()
    {
        GameManager.instance.PlaySelectSound();
        FadeOutSceneChange.instance.FadeOut("MainMenuScene", 0.5f);
    }

    public void TaraButton()
    {
        taraAudio.Play();
    }

    public void MichaelButton()
    {
        michaelAudio.Play();
    }
}
