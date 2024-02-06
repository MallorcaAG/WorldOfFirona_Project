using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest",menuName = "Quest")]
public class QuestUIData : ScriptableObject
{
    public string questID;
    [Space]
    public string questTitle;
    [TextArea] public string questDescription;

    public QuestType questType;

    public string questTaskDesc;    
        private int questTaskReq;

    
    public string questRewardDesc;  
        private QuestRewardType questRewardType; 
        private int questRewardQuantity;











    public enum QuestType
    {
        defeat,
        fetch
    }

    public enum QuestRewardType
    {
        gold,
        item,
        miscellaneous
    }

}
