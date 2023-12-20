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

    public int QuestId => _questId;
    public string Title => _title;
    public string Description => _description;
    public string Objective => _objective;
    public int[] ItemRewards => _itemRewards;
    public bool IsFinished => _isFinished;
    public QuestData(int questId, string title, string description, string objective, int[] itemRewards)
    {
        _questId = questId;
        _title = title;
        _description = description;
        _objective = objective;
        _itemRewards = itemRewards;
        _isFinished = false;
    }

    public void SetIsFinished(bool isFinished)
    {
        _isFinished = isFinished;
    }

    public bool Equals(QuestData other)
    {
        if (other is null)
            return false;

        return QuestId == other.QuestId;
    }
}
