using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Globals;
using Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public GameObject taskEntryPrefab;
    public float verticalSpacing;
    public float scrollDuration;

    private RectTransform _rectTransform;

    private float taskEntryHeight;

    private int _totalTaskNum;
    private int _currentTask = 0;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        GetComponent<VerticalLayoutGroup>().spacing = verticalSpacing;
        taskEntryHeight = taskEntryPrefab.GetComponent<RectTransform>().sizeDelta.y;
        GenerateTaskList();
    }

    private void Update()
    {
        // TODO: DELETE THIS WHEN BUILD!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpdateTask();
        }
    }

    private void GenerateTaskList()
    {
        List<LevelObjectivesSO.LevelObjective> levelObjectives =
            GameManager.Instance.levelManager.levelObjectivesSO.levelObjectives;
        _totalTaskNum = levelObjectives.Count;
        foreach (var task in levelObjectives)
        {
            GameObject taskObject = Instantiate(taskEntryPrefab, transform);
            TextMeshProUGUI taskName = taskObject.transform.Find("Task Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI taskDescription = taskObject.transform.Find("Task Description").GetComponent<TextMeshProUGUI>();

            taskName.text = task.levelName;
            taskDescription.text = task.description;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }

    public void UpdateTask()
    {
        if (_currentTask < _totalTaskNum - 1)
        {
            transform.GetChild(_currentTask).GetChild(0).GetChild(0).GetComponent<Image>().DOFade(1, 1f)
                .OnComplete(() =>
                {
                    _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + taskEntryHeight + verticalSpacing,
                        scrollDuration).SetEase(Ease.OutBounce);
                    _currentTask++;
                });
        }
    }
}
