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
    [SerializeField] private GameObject interactionGO;

    [Space]
    [Header("Event")]
    public GameEvent onPlayerAcceptQuest;


    private void OnEnable()
    {
        titleTxt.text = questData.questTitle;
        descriptionTxt.text = questData.questDescription;
        taskTxt.text = questData.questTaskDesc;
        rewardTxt.text = questData.questRewardDesc;
    }

    

    public void AcceptQuest()
    {
        onPlayerAcceptQuest.Raise(this, questData);

        Debug.Log("Quest Accepted");
    }

    public void isMyQuestComplete(Component sender, object data)
    {
        QuestUIData incomingQuest = (QuestUIData)data;
        if(incomingQuest.questID != questData.questID)
        {
            return;
        }
        
        interactionGO.name = "Talk";
        interactionGO.GetComponent<Talk>().enabled = true;
        interactionGO.GetComponent<Quest>().enabled = false;



        gameObject.SetActive(false);
    }
}
