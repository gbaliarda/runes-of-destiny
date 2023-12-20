using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [SerializeField] private int _npcId;
    private Database _database;
    private QuestData[] _questData;

    public int NpcId => _npcId;

    new void Start()
    {
        base.Start();
        _database = new Database();
        Debug.Log("Getting quests");
        _questData = _database.GetQuests(_npcId);

        Debug.Log($"quests amount: {_questData.Length}");
    }

    new void Update()
    {
        base.Update();
    }
}
