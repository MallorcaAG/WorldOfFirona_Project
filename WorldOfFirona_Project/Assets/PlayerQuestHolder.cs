using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerQuestHolder : MonoBehaviour
{
    [SerializeField] private QuestUIData quest;
    [SerializeField][Range(0,100)] private float progress;
    [Space]
    [Header("Events")]
    public GameEvent onQuestCompletion;
    

    private void Update()
    {
        CheckQuestProgress();
    }

    public void PlayerAcceptQuest(Component sender, object data)
    {
        try
        {
            quest = (QuestUIData)data;

            progress = 0;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
    }

    public void QuestProgress(float progressMade)
    {
        progress += progressMade;
    }

    public void CheckQuestProgress()
    {
        if(quest ==  null)
        {
            return;
        }
        if(!(progress >= 100))
        {
            return;
        }

        QuestComplete();
    }

    private void QuestComplete()
    {
        onQuestCompletion.Raise(this, quest);

        quest = null;
        progress = 0;
    }
}
