using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RepairPhaseHandler : MonoBehaviour
{
    enum Task { MouseLeftRight, MouseUpDown, MashLeftClick }
    Task task;
    Task prevTask;
    [HideInInspector]
    public int currentTaskProgress;
    [HideInInspector]
    public int currentTaskGoal;

    [HideInInspector]
    public int tasksCompleted;
    int numberOfTasksToComplete = 5;
    public TextMeshProUGUI tasksCompletedText;

    public TextMeshProUGUI taskInstructionText;
    public Image taskProgressBar;

    public CursorPointSpawner leftRightCPSPrefab;
    public CursorPointSpawner upDownCPSPrefab;

    public SpriteRenderer mouseSpriteRenderer;
    int currentSpriteIndex;
    public Sprite[] mouseLeftRightSprites;
    public Sprite[] mouseUpDownSprites;
    public Sprite[] mashLeftClickSprites;



    void Start()
    {
        ChooseRandomTask();
    }

    void Update()
    {
        switch (task)
        {
            case Task.MouseLeftRight:
                break;

            case Task.MouseUpDown:
                break;

            case Task.MashLeftClick:
                if (Input.GetMouseButtonDown(0))
                {
                    // Increase task progress
                    IncreaseTaskProgress();

                    // Update mouse sprite
                    currentSpriteIndex++;
                    if (currentSpriteIndex >= mashLeftClickSprites.Length) currentSpriteIndex = 0;
                    mouseSpriteRenderer.sprite = mashLeftClickSprites[currentSpriteIndex];

                    // Increase total tasks completed progress
                    if (currentTaskProgress >= currentTaskGoal)
                    {
                        tasksCompleted++;
                        ChooseRandomTask();
                    }
                }
                break;
        }
    }

    public void ChooseRandomTask()
    {
        // If the player accomplished all of their tasks, go to the play scene
        if (tasksCompleted >= numberOfTasksToComplete)
        {
            SceneManager.LoadScene("PlayScene");
            return;
        }

        // Otherwise, set up for the next task
        UpdateTasksCompletedText();
        currentTaskProgress = 0;
        taskProgressBar.fillAmount = currentTaskProgress;
        currentSpriteIndex = 0;

        // Select new task
        while (task == prevTask)
            task = (Task)Random.Range(0, 3);
        prevTask = task;

        // Set up specific task
        switch (task)
        {
            case Task.MouseLeftRight:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS = Instantiate(leftRightCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS.repairPhaseHandler = this;
                taskInstructionText.text = "HAMMER";
                mouseSpriteRenderer.sprite = mouseLeftRightSprites[currentSpriteIndex];
                break;

            case Task.MouseUpDown:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS2 = Instantiate(upDownCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS2.repairPhaseHandler = this;
                taskInstructionText.text = "SAW";
                mouseSpriteRenderer.sprite = mouseUpDownSprites[currentSpriteIndex];
                break;

            case Task.MashLeftClick:
                currentTaskGoal = 10;
                taskInstructionText.text = "NIBBLE";
                mouseSpriteRenderer.sprite = mashLeftClickSprites[currentSpriteIndex];
                break;
        }
    }

    public void UpdateTasksCompletedText()
    {
        tasksCompletedText.text = "Tasks Completed: " + tasksCompleted + "/" + numberOfTasksToComplete;
    }

    public void IncreaseTaskProgress()
    {
        currentTaskProgress++;
        taskProgressBar.fillAmount = currentTaskProgress / (float)currentTaskGoal;

        // Squash and stretch
        LeanTween.cancel(mouseSpriteRenderer.gameObject);
        mouseSpriteRenderer.transform.localScale = Vector3.one * 2;
        LeanTween.scaleY(mouseSpriteRenderer.gameObject, 1.7f, 0.05f);
        LeanTween.scaleX(mouseSpriteRenderer.gameObject, 2.3f, 0.05f).setOnComplete(() =>
        {
            LeanTween.scaleY(mouseSpriteRenderer.gameObject, 2.25f, 0.15f).setEase(LeanTweenType.easeOutQuad);
            LeanTween.scaleX(mouseSpriteRenderer.gameObject, 1.75f, 0.15f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
            {
                LeanTween.scale(mouseSpriteRenderer.gameObject, Vector3.one * 2, 0.1f).setEase(LeanTweenType.easeInQuad);
            });
        });

    }
}
