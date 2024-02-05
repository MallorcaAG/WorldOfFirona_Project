using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillQuestBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QuestUIData questData;
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI taskTxt;
    [SerializeField] private TextMeshProUGUI rewardTxt;
        [Space]
    [Header("Event")]
    public GameEvent onPlayerAcceptQuest;
    public GameEvent onPlayerDeclineQuest;


    private void OnEnable()
    {
        titleTxt.text = questData.questTitle;
        descriptionTxt.text = questData.questDescription;
        taskTxt.text = questData.questTaskDesc;
        rewardTxt.text = questData.questRewardDesc;
    }

}
