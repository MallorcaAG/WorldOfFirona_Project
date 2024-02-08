using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestProgressDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;

    private QuestUIData questData;

    public void displayPanel(Component sender, object data)
    {
        questData = (QuestUIData)data;

        title.text = questData.questTitle;
        desc.text = questData.questTaskDesc;
    }
}
