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

    /*List<string> P1attackNames = new List<string>();
    List<string> P2attackNames = new List<string>();
    List<string> P3attackNames = new List<string>();

    List<string> P1magicNames = new List<string>();
    List<string> P2magicNames = new List<string>();
    List<string> P3magicNames = new List<string>();*/

    List<CombatSkillData> P1attack = new List<CombatSkillData>();
    List<CombatSkillData> P2attack = new List<CombatSkillData>();
    List<CombatSkillData> P3attack = new List<CombatSkillData>();

    List<CombatSkillData> P1magic = new List<CombatSkillData>();
    List<CombatSkillData> P2magic = new List<CombatSkillData>();
    List<CombatSkillData> P3magic = new List<CombatSkillData>();

    //God I wanna fukin die
    [SerializeField] private Animator[] myAnimation;
    private static readonly int RaiseUpUI = Animator.StringToHash("RaiseUp");
    private static readonly int LowerDownUI = Animator.StringToHash("LowerDown");

    [SerializeField] private GameObject[] myButtons;

    [SerializeField] CombatManager combatManager;

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
                    P1attack.Add(skillData);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P1magic.Add(skillData);
                }
                    break;
            case 1:
                if (skillData.skillType == SkillType.Attack)
                {
                    P2attack.Add(skillData);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P2magic.Add(skillData);
                }
                break;
            case 2:
                if (skillData.skillType == SkillType.Attack)
                {
                    P3attack.Add(skillData);
                }
                else if (skillData.skillType == SkillType.Magic)
                {
                    P3magic.Add(skillData);
                }
                break;
        }
    }
    void AssignButtonName()
    {   //Party Member 1 Button Name Assignment
        for (int i = 0; i < P1attack.Count; i++)
        {
            P1AttackBtnsTxt[i].text = P1attack.ElementAt(i).skillName;
        }
        for (int i = 0; i < P1magic.Count; i++)
        {
            P1MagicBtnsTxt[i].text = P1magic.ElementAt(i).skillName;
        }
        //Party Member 2 Button Name Assignment
        for (int i = 0; i < P2attack.Count; i++)
        {
            P2AttackBtnsTxt[i].text = P2attack.ElementAt(i).skillName;
        }
        for (int i = 0; i < P2magic.Count; i++)
        {
            P2MagicBtnsTxt[i].text = P2magic.ElementAt(i).skillName;
        }
        //Party Member 3 Button Name Assignment
        for (int i = 0; i < P3attack.Count; i++)
        {
            P3AttackBtnsTxt[i].text = P3attack.ElementAt(i).skillName;
        }
        for (int i = 0; i < P3magic.Count; i++)
        {
            P3MagicBtnsTxt[i].text = P3magic.ElementAt(i).skillName;
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
        if ((int)data == 3) //SECOND PLAYER CHARACTER TURN START
        {
            myAnimation[1].CrossFade("RaiseUp", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(true);
            myButtons[2].SetActive(false);
        }
        if ((int)data == 4) //SECOND PLAYER CHARACTER TURN END
        {
            myAnimation[1].CrossFade("LowerDown", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(false);
        }
        if ((int)data == 5) //THIRD PLAYER CHARACTER TURN START
        {
            myAnimation[2].CrossFade("RaiseUp", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(true);
        }
        if ((int)data == 6) //THIRD PLAYER CHARACTER TURN END
        {
            myAnimation[2].CrossFade("LowerDown", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(false);
        }
        if ((int)data == 99) //PLAYER TURN END
        {
            myAnimation[0].CrossFade("LowerDown", 2f);
            myAnimation[1].CrossFade("LowerDown", 2f);
            myAnimation[2].CrossFade("LowerDown", 2f);
            myButtons[0].SetActive(false);
            myButtons[1].SetActive(false);
            myButtons[2].SetActive(false);
        }
    }

    //Runtime
    public void ButtonPressed(int buttonID)
    {
        //CombatSkillData action = new CombatSkillData();
        //int actor = -1; 

        string btnIDstr = buttonID.ToString();
        //Debug.Log(btnIDstr[2] + " "+ btnIDstr[2].GetType());
        if (btnIDstr[0] == '1')
        {
            //Player Character 1
            combatManager.actor = 0;
            //Debug.Log("CodeRun");
            
            if (btnIDstr[1] == '1')
            {
                //Used an Attack
                combatManager.action = P1attack.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
            else if (btnIDstr[1] == '2')
            {
                //Used Magic
                combatManager.action = P1magic.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
        }
        else if (btnIDstr[0] == '2')
        {
            //Player Character 2
            combatManager.actor = 1;

            if (btnIDstr[1] == '1')
            {
                //Used an Attack
                combatManager.action = P2attack.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
            else if (btnIDstr[1] == '2')
            {
                //Used Magic
                combatManager.action = P2magic.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
        }
        else if (btnIDstr[0] == '3')
        {
            //Player Character 3
            combatManager.actor = 2;

            if (btnIDstr[1] == '1')
            {
                //Used an Attack
                combatManager.action = P3attack.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
            else if (btnIDstr[1] == '2')
            {
                //Used Magic
                combatManager.action = P3magic.ElementAt((int)char.GetNumericValue(btnIDstr[2]) - 1);
            }
        }

    }

    public void SelectTarget(int targetID)
    {
        onTargetSelected.Raise(this, targetID);
    }

}
