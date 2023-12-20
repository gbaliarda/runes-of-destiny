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
        _turnInIcon.gameObject.SetActive(false);


        gameObject.GetComponent<Button>().onClick.AddListener(() => QuestManager.instance.OpenDetailedQuest(_questId));
    }

    public void SetQuest(bool isAcceptable, string title, int questId)
    {
        _acceptIcon.gameObject.SetActive(isAcceptable);
        _turnInIcon.gameObject.SetActive(!isAcceptable);
        _questTitle.text = title;
        _questId = questId;
    }
}
