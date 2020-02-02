using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public void MainMenuButton()
    {
        FadeOutSceneChange.instance.FadeOut("MainMenuScene", 0.5f);
    }
}
