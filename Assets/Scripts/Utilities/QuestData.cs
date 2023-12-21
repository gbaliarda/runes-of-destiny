using System;
using UnityEngine;

[System.Serializable]
public class QuestData : IEquatable<QuestData>
{
    private int _questId;
    private string _title;
    private string _description;
    private string _objective;
    private int[] _itemRewards;
    private bool _isFinished;
    private bool _isDelivered;
    private int _killCount;
    private int _progress;

    public int QuestId => _questId;
    public string Title => _title;
    public string Description => _description;
    public string Objective => _objective;
    public int[] ItemRewards => _itemRewards;
    public bool IsFinished => _isFinished;
    public bool IsDelivered => _isDelivered;
    public int KillCount => _killCount;
    public int Progress => _progress;
    public QuestData(int questId, string title, string description, string objective, int[] itemRewards)
    {
        _questId = questId;
        _title = title;
        _description = description;
        _objective = objective;
        _itemRewards = itemRewards;
        _isFinished = false;
        _isDelivered = false;
        _killCount = 0;
        _progress = 0;
    }

    public QuestData(int questId, string title, string description, string objective, int[] itemRewards, int killCount)
    {
        _questId = questId;
        _title = title;
        _description = description;
        _objective = objective;
        _itemRewards = itemRewards;
        _isFinished = false;
        _isDelivered = false;
        _killCount = killCount;
        _progress = 0;
    }

    public void SetIsFinished(bool isFinished)
    {
        if (_isFinished == isFinished) return;
        _isFinished = isFinished;
        if (isFinished) EventsManager.instance.EventFinishQuest(_questId);
    }

    public void SetIsDelivered(bool isDelivered)
    {
        if (_isDelivered == isDelivered) return;
        _isDelivered = isDelivered;
        if (isDelivered) EventsManager.instance.EventDeliverQuest(_questId);
    }

    public void SetProgress(int progress)
    {
        _progress = progress;
    }

    public override int GetHashCode()
    {
        return QuestId.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        QuestData otherQuestData = (QuestData)obj;
        return QuestId.Equals(otherQuestData.QuestId);
    }

    public bool Equals(QuestData other)
    {
        if (other == null)
        {
            return false;
        }

        return QuestId.Equals(other.QuestId);
    }
}
