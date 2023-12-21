using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogManager : MonoBehaviour
{
    private Database _database;
    private Dictionary<QuestData, GameObject> _quests;
    [SerializeField] private GameObject _questInLogPrefab;
    void Start()
    {
        _quests = new Dictionary<QuestData, GameObject>();
        _database = new Database();

        EventsManager.instance.OnAcceptQuest += OnAcceptQuest;
        EventsManager.instance.OnUpdateQuest += OnUpdateQuest;
        EventsManager.instance.OnFinishQuest += OnFinishQuest;
        EventsManager.instance.OnDeliverQuest += OnDeliverQuest;

        gameObject.SetActive(false);
    }

    private void OnAcceptQuest(int questId)
    {
        QuestData questData = _database.GetQuest(questId);
        if (questData == null) return;

        GameObject questInLog = Instantiate(_questInLogPrefab, transform);
        _quests.Add(questData, questInLog);
        if (questData.KillCount > 0)
        {
            questInLog.GetComponent<QuestInLog>()?.SetQuestInLog(true, questData.Title, questData.Objective, questData.Progress+"/"+questData.KillCount);
        } else
        {
            questInLog.GetComponent<QuestInLog>()?.SetQuestInLog(true, questData.Title, questData.Objective);
        }
        gameObject.SetActive(_quests.Count != 0);
    }

    private void OnUpdateQuest(int questId)
    {
        QuestData questData = Player.instance.GetQuestInLog(questId);
        if (questData == null) return;

        GameObject questInLog = _quests.GetValueOrDefault(questData);
        questInLog.GetComponent<QuestInLog>()?.SetProgress(questData.Progress + "/" + questData.KillCount);
    }

    private void OnFinishQuest(int questId)
    {
        QuestData questData = Player.instance.GetQuestInLog(questId);
        if (questData == null) return;

        GameObject questInLog = _quests.GetValueOrDefault(questData);
        questInLog.GetComponent<QuestInLog>()?.SetQuestInLog(false, questData.Title, questData.Objective);
    }

    private void OnDeliverQuest(int questId)
    {
        QuestData questData = Player.instance.GetQuestInLog(questId);
        if (questData == null) return;

        GameObject questInLog = _quests.GetValueOrDefault(questData);
        _quests.Remove(questData);
        Destroy(questInLog);
        gameObject.SetActive(_quests.Count != 0);
    }
}
