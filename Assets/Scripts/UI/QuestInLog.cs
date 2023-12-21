using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class QuestInLog : MonoBehaviour
{
    private int _questId;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _titleButton;
    [SerializeField] private TextMeshProUGUI _objectives;
    [SerializeField] private TextMeshProUGUI _progress;
    void Start()
    {

    }

    public void SetQuestInLog(int questId, bool inProgress, string title, string objectives)
    {
        _title.text = title + (inProgress ? " (In Progress): " : " (Finished): ");
        _questId = questId;
        _titleButton.onClick.RemoveAllListeners();
        _titleButton.onClick.AddListener(() => QuestManager.instance.OpenDetailedQuest(_questId));
        if (inProgress)
        {
            _objectives.text = objectives;
            _objectives.color = HexToColor("#ffffff");
        } else
        {
            _objectives.text = "<s>" + objectives + "</s>";
            _objectives.color = HexToColor("#cccccc");
        }
        _progress.gameObject.SetActive(false);
    }

    public void SetQuestInLog(int questId, bool inProgress, string title, string objectives, string progress)
    {
        _title.text = title + (inProgress ? " (In Progress): " : " (Finished): ");
        _questId = questId;
        _titleButton.onClick.RemoveAllListeners();
        _titleButton.onClick.AddListener(() => QuestManager.instance.OpenDetailedQuest(_questId));
        if (inProgress)
        {
            _objectives.text = objectives;
            _progress.text = progress;
            _objectives.color = HexToColor("#ffffff");
            _progress.color = HexToColor("#ffffff");
        }
        else
        {
            _objectives.text = "<s>" + objectives + "</s>";
            _objectives.color = HexToColor("#cccccc");
        }
    }

    public void SetProgress(string progress)
    {
        _progress.text = progress;
    }
    private Color HexToColor(string hex)
    {
        Color color = Color.black;

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }

        return color;
    }
}
