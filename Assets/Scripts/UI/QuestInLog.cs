using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestInLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _objectives;
    void Start()
    {
        
    }

    public void SetQuestInLog(string title, string objectives)
    {
        _title.text = title + " (In Progress): ";
        _objectives.text = objectives;
    }
}
