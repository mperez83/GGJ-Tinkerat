﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPointSpawner : MonoBehaviour
{
    public GameObject cursorPoint;
    [HideInInspector]
    public RepairPhaseHandler repairPhaseHandler;

    int currentSpawnPoint;
    public Transform spawnPointHolder;

    void Start()
    {
        CursorPoint newCursorPoint = Instantiate(cursorPoint, spawnPointHolder.GetChild(currentSpawnPoint).position, Quaternion.identity).GetComponent<CursorPoint>();
        newCursorPoint.cursorPointSpawner = this;
    }

    public void CursorPointTriggered()
    {
        repairPhaseHandler.IncreaseTaskProgress();
        if (repairPhaseHandler.currentTaskProgress >= repairPhaseHandler.currentTaskGoal)
        {
            repairPhaseHandler.tasksCompleted++;
            repairPhaseHandler.ChooseRandomTask();
            Destroy(gameObject);
            return;
        }

        currentSpawnPoint++;
        if (currentSpawnPoint >= spawnPointHolder.childCount) currentSpawnPoint = 0;

        CursorPoint newCursorPoint = Instantiate(cursorPoint, spawnPointHolder.GetChild(currentSpawnPoint).position, Quaternion.identity).GetComponent<CursorPoint>();
        newCursorPoint.cursorPointSpawner = this;
    }
}
