using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CombatHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerHUD;
    [SerializeField] private GameObject[] enemyHUD;
    [SerializeField] private TextMeshProUGUI[] playerHUDNameTxt;
    [SerializeField] private TextMeshProUGUI[] enemyHUDNameTxt;
    [SerializeField] private TextMeshProUGUI[] P1AttackBtnsTxt;
    [SerializeField] private TextMeshProUGUI[] P1MagicBtnsTxt;
    [SerializeField] private TextMeshProUGUI[] P2AttackBtnsTxt;
    [SerializeField] private TextMeshProUGUI[] P2MagicBtnsTxt;
    [SerializeField] private TextMeshProUGUI[] P3AttackBtnsTxt;
    [SerializeField] private TextMeshProUGUI[] P3MagicBtnsTxt;

    List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

    List<string> P1attackNames = new List<string>();
    List<string> P2attackNames = new List<string>();
    List<string> P3attackNames = new List<string>();

    List<string> P1magicNames = new List<string>();
    List<string> P2magicNames = new List<string>();
    List<string> P3magicNames = new List<string>();

    //God I wanna fukin die
    [SerializeField] private Animator[] myAnimation;
    private static readonly int RaiseUpUI = Animator.StringToHash("RaiseUp");
    private static readonly int LowerDownUI = Animator.StringToHash("LowerDown");

    [SerializeField] private GameObject[] myButtons;

    [Header("Events")]
    public GameEvent onButtonPressed;
    public GameEvent onTargetSelected;

    //Initialization
    #region
    public void getPlayerUnitData(Component sender, object data)
    {
        playerUnits.Add((PlayerUnit)data);
    }
    public void getEnemyUnitData(Component sender, object data)
    {
        enemyUnits.Add((EnemyUnit)data);
    }
    void DisplayHUD()
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerHUD[i].gameObject.SetActive(true);
            playerHUDNameTxt[i].text = playerUnits[i].getUnitName();
        //Dont ask me how anything works past this point, I literally forgor
            for (int j = 0; j < playerUnits[i].combatSkills.Length; j++)
            {   
                CombatSkillData skillData = playerUnits[i].combatSkills[j];
                AddSkillNameToList(skillData, i);
            }
        }
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            enemyHUD[i].gameObject.SetActive(true);
            enemyHUDNameTxt[i].text = enemyUnits[i].getUnitName();
        }

        AssignButtonName();
    }
    void AddSkillNameToList(CombatSkillData skillData, int index)
    {
        
        switch (index)
        {
            case 0:
                if (skillData.skillType == SkillType.Attack)
                {
                    P1attackNames.Add(skillData.skillName);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P1magicNames.Add(skillData.skillName);
                }
                    break;
            case 1:
                if (skillData.skillType == SkillType.Attack)
                {
                    P2attackNames.Add(skillData.skillName);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P2magicNames.Add(skillData.skillName);
                }
                break;
            case 2:
                if (skillData.skillType == SkillType.Attack)
                {
                    P3attackNames.Add(skillData.skillName);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P3magicNames.Add(skillData.skillName);
                }
                break;
        }
    }
    void AssignButtonName()
    {   //Party Member 1 Button Name Assignment
        for (int i = 0; i < P1attackNames.Count; i++)
        {
            P1AttackBtnsTxt[i].text = P1attackNames.ElementAt(i);
        }
        for (int i = 0; i < P1magicNames.Count; i++)
        {
            P1MagicBtnsTxt[i].text = P1magicNames.ElementAt(i);
        }
        //Party Member 2 Button Name Assignment
        for (int i = 0; i < P2attackNames.Count; i++)
        {
            P2AttackBtnsTxt[i].text = P2attackNames.ElementAt(i);
        }
        for (int i = 0; i < P2magicNames.Count; i++)
        {
            P2MagicBtnsTxt[i].text = P2magicNames.ElementAt(i);
        }
        //Party Member 3 Button Name Assignment
        for (int i = 0; i < P3attackNames.Count; i++)
        {
            P3AttackBtnsTxt[i].text = P3attackNames.ElementAt(i);
        }
        for (int i = 0; i < P3magicNames.Count; i++)
        {
            P3MagicBtnsTxt[i].text = P3magicNames.ElementAt(i);
        }
    }
    void CleanUpList()
    {
        playerUnits.RemoveAll(item => item == null);
        enemyUnits.RemoveAll(item => item == null);
    }
    #endregion 

    public void updateHUD(Component sender, object data)
    {
        if((int)data == 0)
        {
            CleanUpList();
            DisplayHUD();
        }
        if((int)data == 1)  //FIRST PLAYER CHARACTER TURN START
        {
            myAnimation[0].CrossFade("RaiseUp",2f);
            myButtons[0].SetActive(true);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(false);
        }
        if ((int)data == 2) //FIRST PLAYER CHARACTER TURN END
        {
            myAnimation[0].CrossFade("LowerDown", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(false);
        }
    }

    //Runtime
    public void ButtonPressed(int buttonID)
    {
        onButtonPressed.Raise(this, buttonID);
    }

    public void SelectTarget(int targetID)
    {
        onTargetSelected.Raise(this, targetID);
    }

}
