using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsScreenUI : MonoBehaviour
{
    public TextMeshProUGUI resultsText;

    void Start()
    {
        resultsText.text = "The top spun for " + GameManager.instance.topSpinTime.ToString("F1") + " seconds!";
    }

    public void MainMenuButton()
    {
        GameManager.instance.PlaySelectSound();
        GameManager.instance.repairTime = 0;
        GameManager.instance.topSpinTime = 0;
        FadeOutSceneChange.instance.FadeOut("MainMenuScene", 0.5f);
    }
}
