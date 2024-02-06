using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCompletionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject questCompletePanel;
    [SerializeField] private TextMeshProUGUI rewardsTxt;

    private QuestUIData questData;

    public void displayPanel(Component sender, object data)
    {
        questData = (QuestUIData)data;

        rewardsTxt.text = questData.questRewardDesc;

        questCompletePanel.SetActive(true);
    }
}
