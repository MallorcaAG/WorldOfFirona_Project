using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class CombatManager : MonoBehaviour
{
    [SerializeField] private BattleState state;
    [Space]
    [SerializeField] private GameObject[] playerParty;
    [SerializeField] private GameObject[] enemyParty;

    [SerializeField] private Transform[] playerBattlePosition;
    [SerializeField] private Transform[] enemyBattlePosition;

    PlayerUnit[] playerUnit = new PlayerUnit[3];
    EnemyUnit[] enemyUnit = new EnemyUnit[3];

    string buttonID, targetID;

    [Header("Events")]
    public GameEvent sendPlayerDataToHUD;
    public GameEvent sendEnemyDataToHUD;
    public GameEvent updateHUD;
    public GameEvent characterMoveForward;




    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(initializeBattle());
    }

    IEnumerator initializeBattle()
    {
        for(int i = 0; i < playerParty.Length; i++)
        {
            GameObject playerGO = Instantiate(playerParty[i], playerBattlePosition[i]);
            playerUnit[i] = playerGO.GetComponent<PlayerUnit>();
        }

        for (int i = 0; i < enemyParty.Length; i++)
        {
            GameObject enemyGO = Instantiate(enemyParty[i], enemyBattlePosition[i]);
            enemyUnit[i] = enemyGO.GetComponent<EnemyUnit>();
        }

        initializeHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn(1);
    }

    void initializeHUD()
    {
        for(int i = 0; i < playerUnit.Length; i++)
        {
            sendPlayerDataToHUD.Raise(this, playerUnit[i]);
        }

        for (int j = 0; j < enemyUnit.Length; j++)
        {
            sendEnemyDataToHUD.Raise(this, enemyUnit[j]);
        }

        updateHUD.Raise(this, 0);
    }

    void PlayerTurn(int characterTurn)
    {
        if(characterTurn == 1)
        {
            characterMoveForward.Raise(this, 1);
            updateHUD.Raise(this, 1);
        }
    }

    public void getSelectedAction(Component sender, object btnID)
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        buttonID = ((int)btnID).ToString();
        Debug.Log(buttonID[0] + " " + buttonID[1] + " " + buttonID[2]);
    }

    public void getSelectedTarget(Component sender, object trgtID)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //updateHUD.Raise(this, 2);
        targetID = ((int)trgtID).ToString();
        Debug.Log(targetID[0] + " " + targetID[1] + " " + targetID[2]);
        PlayerAction(buttonID, targetID);
    }

    void PlayerAction(string action, string target)
    {
        //Who is doing the action
        if (action[0].Equals("1"))
        {

        }
        else if (action[0].Equals("2"))
        {

        }
        else if (action[0].Equals("3"))
        {

        }
        //Who is the action being done upon
        if (target[1].Equals("1"))
        {
            //Action done to Team
            if (target[2].Equals("0"))
            {

            }
            else if (target[2].Equals("1"))
            {

            }
            else if (target[2].Equals("2"))
            {

            }
        }
        else if (target[1].Equals("2"))
        {
            //Action done to Opponent
            if (target[2].Equals("0"))
            {

            }
            else if (target[2].Equals("1"))
            {

            }
            else if (target[2].Equals("2"))
            {

            }
        }
    }

}
