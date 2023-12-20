using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogManager : MonoBehaviour
{
    private Database _database;
    private List<QuestData> _quests;
    [SerializeField] private GameObject _questInLogPrefab;
    void Start()
    {
        _quests = new List<QuestData>();
        _database = new Database();

        EventsManager.instance.OnAcceptQuest += OnAcceptQuest;

        gameObject.SetActive(false);
    }

    private void OnAcceptQuest(int questId)
    {
        QuestData questData = _database.GetQuest(questId);
        if (questData == null) return;

        _quests.Add(questData);
        GameObject questInLog = Instantiate(_questInLogPrefab, transform);
        questInLog.GetComponent<QuestInLog>()?.SetQuestInLog(questData.Title, questData.Objective);
        gameObject.SetActive(_quests.Count != 0);
    }
}
