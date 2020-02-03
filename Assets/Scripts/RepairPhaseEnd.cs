using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairPhaseEnd : MonoBehaviour
{
    public Canvas endCanvas;
    public Image fadeOverlay;
    public TextMeshProUGUI resultsText;
    public Button continueButton;

    void Start()
    {
        endCanvas.gameObject.SetActive(true);
        string resultsString = "Repair time: " + GameManager.instance.repairTime.ToString("F1") + "\nRank: ";

        if (GameManager.instance.repairTime <= 10)
        {
            resultsString += "Perfect! 2000";
            resultsText.color = Color.magenta;
        }
        else if (GameManager.instance.repairTime <= 12)
        {
            resultsString += "Extreme!";
            resultsText.color = Color.magenta;
        }
        else if (GameManager.instance.repairTime <= 14)
        {
            resultsString += "Awesome!";
            resultsText.color = Color.red;
        }
        else if (GameManager.instance.repairTime <= 16)
        {
            resultsString += "Tight!";
            resultsText.color = Color.red;
        }
        else if (GameManager.instance.repairTime <= 18)
        {
            resultsString += "Radical!";
            resultsText.color = Color.yellow;
        }
        else if (GameManager.instance.repairTime <= 20)
        {
            resultsString += "Cool!";
            resultsText.color = Color.yellow;
        }
        else if (GameManager.instance.repairTime <= 22)
        {
            resultsString += "Jammin'!";
            resultsText.color = Color.yellow;
        }
        else if (GameManager.instance.repairTime <= 24)
        {
            resultsString += "Nice!";
            resultsText.color = Color.cyan;
        }
        else if (GameManager.instance.repairTime <= 26)
        {
            resultsString += "Great!";
            resultsText.color = Color.cyan;
        }
        else
        {
            resultsString += "Good!";
            resultsText.color = Color.cyan;
        }

        if (GameManager.instance.repairTime <= 16)
            resultsString += "\nDouble-jump enabled!";

        resultsText.text = resultsString;
        resultsText.color = new Color(resultsText.color.r, resultsText.color.g, resultsText.color.b, 0);

        LeanTween.value(gameObject, fadeOverlay.color.a, 0.85f, 1).setEase(LeanTweenType.easeInOutCubic).setOnUpdate((float value) =>
        {
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, value);
        }).setOnComplete(() =>
        {
            continueButton.gameObject.SetActive(true);
            LeanTween.scale(resultsText.gameObject, Vector3.one * 1.5f, 3f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.value(0, 1, 3f).setOnUpdate((float value) =>
            {
                resultsText.color = new Color(resultsText.color.r, resultsText.color.g, resultsText.color.b, value);
            });
        });
    }

    public void ContinueButton()
    {
        GameManager.instance.PlaySelectSound();
        FadeOutSceneChange.instance.FadeOut("PlayScene", 1f);
    }
}
