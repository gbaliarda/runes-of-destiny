using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Image _acceptIcon;
    [SerializeField] private Image _turnInIcon;
    [SerializeField] private TextMeshProUGUI _questTitle;
    private int _questId;

    void Start()
    {


        gameObject.GetComponent<Button>().onClick.AddListener(() => QuestManager.instance.OpenDetailedQuest(_questId));
    }

    public void SetQuest(bool isFinished, string title, int questId)
    {
        _acceptIcon.gameObject.SetActive(!isFinished);
        _turnInIcon.gameObject.SetActive(isFinished);
        _questTitle.text = title;
        _questId = questId;
    }
}
