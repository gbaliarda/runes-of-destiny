using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    #region SINGLETON
    static public QuestManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    private string _npcName;
    private Database _database;
    [SerializeField] private GameObject _generalView;
    [SerializeField] private GameObject _questList;
    [SerializeField] private GameObject _questUIPrefab;

    [SerializeField] private GameObject _detailedQuestView;
    [SerializeField] private TextMeshProUGUI _detailedQuestTitle;
    [SerializeField] private TextMeshProUGUI _detailedQuestDescription;
    [SerializeField] private TextMeshProUGUI _detailedQuestObjectives;
    [SerializeField] private GameObject _detailedQuestRewardPanel;
    [SerializeField] private RewardItem _detailedQuestItemReward;
    [SerializeField] private Button _detailedQuestAccept;
    [SerializeField] private Button _detailedQuestDeliver;

    private List<GameObject> _quests;

    public string NpcName => _npcName;

    void Start()
    {
        _database = new Database();

        _quests = new List<GameObject>();


        gameObject.SetActive(false);

        EventsManager.instance.OnTalkWithNpc += OnTalkWithNpc;
    }

    void Update()
    {

    }

    void OnTalkWithNpc(string name, int npcId)
    {
        _npcName = name;
        QuestData[] quests = _database.GetQuests(npcId);

        foreach (QuestData quest in quests)
        {
            QuestData playerQuest = Player.instance.GetQuestInLog(quest.QuestId);
            if (playerQuest != null)
            {
                Debug.Log($"player has quest and {playerQuest.IsDelivered}");
                if (!playerQuest.IsFinished || playerQuest.IsDelivered) continue;
                GameObject questUI = Instantiate(_questUIPrefab, _questList.transform);
                _quests.Add(questUI);
                questUI.GetComponent<QuestUI>()?.SetQuest(true, quest.Title, quest.QuestId);
            } else
            {
                GameObject questUI = Instantiate(_questUIPrefab, _questList.transform);
                _quests.Add(questUI);
                questUI.GetComponent<QuestUI>()?.SetQuest(false, quest.Title, quest.QuestId);
            }
        }

        gameObject.SetActive(true);
        _detailedQuestView.SetActive(false);
        _generalView.SetActive(true);
    }

    public void OpenDetailedQuest(int questId)
    {
        Debug.Log(questId);
        QuestData questData = _database.GetQuest(questId);
        Debug.Log("QuestData");
        if (questData == null) return;
        QuestData questInPlayerLog = Player.instance.GetQuestInLog(questId);
        if (questInPlayerLog != null) questData = questInPlayerLog;
        _generalView.SetActive(false);
        _detailedQuestView.SetActive(true);

        _detailedQuestTitle.text = questData.Title;
        _detailedQuestDescription.text = questData.Description;
        _detailedQuestObjectives.text = questData.Objective;
        if (!questData.IsFinished)
        {
            _detailedQuestDeliver.gameObject.SetActive(false);
            _detailedQuestAccept.gameObject.SetActive(true);
            _detailedQuestAccept.onClick.RemoveAllListeners();
            _detailedQuestAccept.onClick.AddListener(() => AcceptQuest(questId));
        } else
        {
            _detailedQuestDeliver.gameObject.SetActive(true);
            _detailedQuestAccept.gameObject.SetActive(false);
            _detailedQuestDeliver.onClick.RemoveAllListeners();
            _detailedQuestDeliver.onClick.AddListener(() => DeliverQuest(questId));
        }
        _detailedQuestRewardPanel.SetActive(false);
        if (questData.ItemRewards != null && questData.ItemRewards.Length > 0)
        {
            _detailedQuestRewardPanel.SetActive(true);
            _detailedQuestItemReward.UpdateItem(questData.ItemRewards[0]);
        }
    }

    public void CloseQuestPanel()
    {
        foreach (GameObject quest in _quests)
        {
            Destroy(quest);
        }
        _quests.Clear();
        gameObject.SetActive(false);
        _detailedQuestView.SetActive(false);
        _generalView.SetActive(true);
    }

    public void AcceptQuest(int questId)
    {
        Player.instance.AcceptQuest(questId);
        CloseQuestPanel();
    }

    public void DeliverQuest(int questId)
    {
        Player.instance.DeliverQuest(questId);
        CloseQuestPanel();
    }
}
