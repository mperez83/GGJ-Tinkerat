using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RepairPhaseHandler : MonoBehaviour
{
    public enum Task { Saw, Hammer, Nibble }
    Task task;
    Task prevTask;
    public Task[] listOfTasks;

    [HideInInspector]
    public int currentTaskProgress;
    [HideInInspector]
    public int currentTaskGoal;

    public int tasksCompleted;     // Set this to -1 just so Start() doesn't automatically grant 1 completed task
    int numberOfTasksToComplete = 8;
    public TextMeshProUGUI tasksCompletedText;

    public TextMeshProUGUI taskInstructionText;
    public Image taskProgressBar;

    public GameObject nibbleButton;

    public CursorPointSpawner sawCPSPrefab;
    public CursorPointSpawner hammerCPSPrefab;

    public SpriteRenderer mouseSpriteRenderer;
    int currentSpriteIndex;
    public Sprite[] nibbleSprites;
    public Sprite[] hammerSprites;
    public Sprite[] sawSprites;

    public AudioClip hammerSound;
    public AudioClip sawSound;
    public AudioClip nibbleSound;
    public AudioSource audioSource;



    void Start()
    {
        ChooseRandomTask();
    }

    void Update()
    {
        GameManager.instance.repairTime += Time.deltaTime;
    }

    public void ChooseRandomTask()
    {
        // If the player accomplished all of their tasks, go to the play scene
        tasksCompleted++;
        currentTaskProgress = 0;
        UpdateTasksCompletedText();

        if (tasksCompleted >= numberOfTasksToComplete)
        {
            nibbleButton.SetActive(false);
            GetComponent<RepairPhaseEnd>().enabled = true;
            Destroy(this);
            return;
        }

        // Otherwise, set up for the next task
        taskProgressBar.fillAmount = currentTaskProgress;
        currentSpriteIndex = 0;

        if (task == Task.Nibble) nibbleButton.SetActive(false);

        // Select new task
        while (task == prevTask)
            task = (Task)Random.Range(0, 3);
        prevTask = task;

        // Set up specific task
        switch (task)
        {
            case Task.Nibble:
                currentTaskGoal = 10;
                taskInstructionText.text = "Task: NIBBLE";
                mouseSpriteRenderer.sprite = nibbleSprites[currentSpriteIndex];
                nibbleButton.SetActive(true);
                break;

            case Task.Hammer:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS2 = Instantiate(hammerCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS2.repairPhaseHandler = this;
                taskInstructionText.text = "Task: HAMMER";
                mouseSpriteRenderer.sprite = hammerSprites[currentSpriteIndex];
                break;

            case Task.Saw:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS = Instantiate(sawCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS.repairPhaseHandler = this;
                taskInstructionText.text = "Task: SAW";
                mouseSpriteRenderer.sprite = sawSprites[currentSpriteIndex];
                break;
        }
    }

    public void GoToNextTask()
    {
        // If the player accomplished all of their tasks, go to the play scene
        tasksCompleted++;
        currentTaskProgress = 0;
        UpdateTasksCompletedText();

        if (tasksCompleted >= listOfTasks.Length)
        {
            nibbleButton.SetActive(false);
            GetComponent<RepairPhaseEnd>().enabled = true;
            Destroy(this);
            return;
        }

        // Otherwise, set up for the next task
        taskProgressBar.fillAmount = currentTaskProgress;
        currentSpriteIndex = 0;

        if (task == Task.Nibble) nibbleButton.SetActive(false);

        task = listOfTasks[tasksCompleted];

        // Set up specific task
        switch (task)
        {
            case Task.Nibble:
                currentTaskGoal = 10;
                taskInstructionText.text = "Task: NIBBLE";
                mouseSpriteRenderer.sprite = nibbleSprites[currentSpriteIndex];
                nibbleButton.SetActive(true);
                break;

            case Task.Hammer:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS2 = Instantiate(hammerCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS2.repairPhaseHandler = this;
                taskInstructionText.text = "Task: HAMMER";
                mouseSpriteRenderer.sprite = hammerSprites[currentSpriteIndex];
                break;

            case Task.Saw:
                currentTaskGoal = 10;
                CursorPointSpawner newCPS = Instantiate(sawCPSPrefab).GetComponent<CursorPointSpawner>();
                newCPS.repairPhaseHandler = this;
                taskInstructionText.text = "Task: SAW";
                mouseSpriteRenderer.sprite = sawSprites[currentSpriteIndex];
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
        if (currentTaskProgress >= currentTaskGoal) ChooseRandomTask();
        UpdateMouseSprite();

        // Squash and stretch
        LeanTween.cancel(mouseSpriteRenderer.gameObject);
        mouseSpriteRenderer.transform.localScale = Vector3.one;
        LeanTween.scaleY(mouseSpriteRenderer.gameObject, 0.95f, 0.05f);
        LeanTween.scaleX(mouseSpriteRenderer.gameObject, 1.05f, 0.05f).setOnComplete(() =>
        {
            LeanTween.scaleY(mouseSpriteRenderer.gameObject, 1.1f, 0.15f).setEase(LeanTweenType.easeOutQuad);
            LeanTween.scaleX(mouseSpriteRenderer.gameObject, 0.9f, 0.15f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
            {
                LeanTween.scale(mouseSpriteRenderer.gameObject, Vector3.one, 0.1f).setEase(LeanTweenType.easeInQuad);
            });
        });
    }

    public void UpdateMouseSprite()
    {
        currentSpriteIndex++;

        switch (task)
        {
            case Task.Nibble:
                if (currentSpriteIndex >= nibbleSprites.Length) currentSpriteIndex = 0;
                mouseSpriteRenderer.sprite = nibbleSprites[currentSpriteIndex];
                audioSource.PlayOneShot(nibbleSound);
                break;

            case Task.Hammer:
                if (currentSpriteIndex >= hammerSprites.Length) currentSpriteIndex = 0;
                mouseSpriteRenderer.sprite = hammerSprites[currentSpriteIndex];
                if (currentSpriteIndex == 1) audioSource.PlayOneShot(hammerSound);
                break;

            case Task.Saw:
                if (currentSpriteIndex >= sawSprites.Length) currentSpriteIndex = 0;
                mouseSpriteRenderer.sprite = sawSprites[currentSpriteIndex];
                audioSource.PlayOneShot(sawSound);
                break;
        }
    }
}
