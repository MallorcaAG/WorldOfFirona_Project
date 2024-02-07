using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    [SerializeField] CombatHUDManager ui;
    [Space]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [Space]


    List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

    string targetID;
    public CombatSkillData action;
    public int actor;
    public Unit target;
    int playerTurnCount = 0;

    [Header("Events")]
    public GameEvent sendPlayerDataToHUD;
    public GameEvent sendEnemyDataToHUD;
    public GameEvent updateHUD;
    public GameEvent characterMoveForward;
    public GameEvent characterMoveBackward;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(initializeBattle());
    }

    private void FixedUpdate()
    {
        //CHECK FOR WIN CONDITION
        checkPlayerWinCondition();
        checkPlayerLoseCondition();
    }

    IEnumerator initializeBattle()
    {
        for(int i = 0; i < playerParty.Length; i++)
        {
            GameObject playerGO = Instantiate(playerParty[i], playerBattlePosition[i]);
            //playerUnit[i] = playerGO.GetComponent<PlayerUnit>();

            playerUnits.Add(playerGO.GetComponent<PlayerUnit>());
        }

        for (int i = 0; i < enemyParty.Length; i++)
        {
            GameObject enemyGO = Instantiate(enemyParty[i], enemyBattlePosition[i]);
            //enemyUnit[i] = enemyGO.GetComponent<EnemyUnit>();

            enemyUnits.Add(enemyGO.GetComponent<EnemyUnit>());
        }

        initializeHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        playerTurnCount = 1;

        PlayerTurn(playerTurnCount);
    }

    void initializeHUD()
    {
        for(int i = 0; i < playerUnits.Count; i++)
        {
            sendPlayerDataToHUD.Raise(this, playerUnits.ElementAt(i));
        }

        for (int j = 0; j < enemyUnits.Count; j++)
        {
            sendEnemyDataToHUD.Raise(this, enemyUnits.ElementAt(j));
        }

        updateHUD.Raise(this, 0);
    }

    void PlayerTurn(int characterTurn)
    {
        if(characterTurn == 99)
        {
            updateHUD.Raise(this, 99);
        }
        if (characterTurn == 1)
        {
            characterMoveForward.Raise(this, 1);
            updateHUD.Raise(this, 1);
        }
        else if(characterTurn == 2)
        {
            characterMoveForward.Raise(this, 2);
            updateHUD.Raise(this, 2);
            updateHUD.Raise(this, 3);

        }
        else if(characterTurn == 3)
        {
            characterMoveForward.Raise(this, 3);
            updateHUD.Raise(this, 4);
            updateHUD.Raise(this, 5);
        }
    }

    public void getSelectedAction(Component sender, object data)
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        //actor = (PlayerUnit)userID;


    }

    public void getSelectedTarget(Component sender, object trgtID)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //updateHUD.Raise(this, 2);
        targetID = ((int)trgtID).ToString();
        //Debug.Log(targetID[0] + " " + targetID[1] + " " + targetID[2]);
        PlayerAction(action, targetID, actor);

        //CHECK FOR WIN CONDITION
        checkPlayerWinCondition();
        checkPlayerLoseCondition();


        //END TURN
        StartCoroutine(EndPlayerCharacterTurn());
    }

    void PlayerAction(CombatSkillData actionID, string targetID, int actor)
    {
        //Who is the action being done upon
        if (targetID[1] == '1')
        {
            //Action done to Team
            if (targetID[2] == '0')
            {
                target = playerUnits.ElementAt(0);
            }
            else if (targetID[2]== '1')
            {
                target = playerUnits.ElementAt(1);
            }
            else if (targetID[2] == '2')
            {
                target = playerUnits.ElementAt(2);
            }
        }
        else if (targetID[1] == '2')
        {
            //Action done to Opponent
            if (targetID[2] == '0')
            {
                target = enemyUnits.ElementAt(0);
            }
            else if (targetID[2] == '1')
            {
                target = enemyUnits.ElementAt(1);
            }
            else if (targetID[2] == '2')
            {
                target = enemyUnits.ElementAt(2);
            }
        }


        //What is the Action
        if (actionID.skillType == SkillType.Magic)
        {
            if (actionID.skillFunction == SkillFunction.Heal)
            {
                playerUnits.ElementAt(actor).subtractMP(actionID.skillRequirement, playerUnits.ElementAt(actor));

                if (target != null)
                {
                    target.regenHP(actionID.skillValue, target);
                }
            }
            if (actionID.skillFunction == SkillFunction.Damage)
            {
                playerUnits.ElementAt(actor).subtractMP(actionID.skillRequirement, playerUnits.ElementAt(actor));
                
                if(target!=null)
                {
                    target.dealDamage(actionID.skillValue, target);
                }
            }
            if (actionID.skillFunction == SkillFunction.RechargeMana)
            {
                playerUnits.ElementAt(actor).subtractMP(actionID.skillRequirement, playerUnits.ElementAt(actor));

                if (target != null)
                {
                    try
                    {
                        PlayerUnit t = (PlayerUnit)target;

                        t.addMP(actionID.skillValue, t);
                    }
                    catch(InvalidCastException e)
                    {
                        //Do nothing
                    }
                    //target.dealDamage(actionID.skillValue, target);
                }
            }

        }
        else if (actionID.skillType == SkillType.Attack)
        {
            target.dealDamage(actionID.skillValue, target);
        }

        //CHECK FOR WIN CONDITION
        checkPlayerWinCondition();
        checkPlayerLoseCondition();
    }

    IEnumerator EndPlayerCharacterTurn()
    {
        characterMoveBackward.Raise(this, playerTurnCount);

        playerTurnCount++;

        if(playerTurnCount > playerUnits.Count)
        {
            state = BattleState.ENEMYTURN;

            playerTurnCount = 99;
            PlayerTurn(playerTurnCount);
            playerTurnCount = 0;

            yield return new WaitForSeconds(0.5f);

            //RUN GAME EVENT THAT SAYS ITS THE OPPONENT'S TURN NOW
            Debug.Log("PLAYER TURN HAS ENDED");
            StartCoroutine(startEnemyTurn());

            yield break;
        }

        //CHECK FOR WIN CONDITION
        checkPlayerWinCondition();
        checkPlayerLoseCondition();

        Debug.Log("Next Player Character Turn ");
        PlayerTurn(playerTurnCount);



    }

    public IEnumerator startEnemyTurn()
    {
        if(state  != BattleState.ENEMYTURN)
        {
            yield break;
        }
        for(int i = 0; i < enemyUnits.Count; i++)
        {
            //IF UNIT IS NOT DEAD
            if (!(enemyUnits.ElementAt(i).unitIsDead())) 
            {
                Debug.Log("ALIVE:"+enemyUnits.ElementAt(i).unitIsDead());

                yield return new WaitForSeconds(1f);

                Debug.Log("===It is "+ enemyUnits.ElementAt(i).getUnitName()+"'s turn===");

                int actionChoice = UnityEngine.Random.Range(0, enemyUnits.ElementAt(i).combatSkills.Length);
                int target = UnityEngine.Random.Range(0, playerUnits.Count);

                

                Debug.Log("actionChoice index "+actionChoice);
                Debug.Log("targetting player number " + target);

                int damage = enemyUnits.ElementAt(i).combatSkills[actionChoice].skillValue;
                playerUnits.ElementAt(target).dealDamage(damage, playerUnits.ElementAt(target));


            }

            //CHECK FOR WIN CONDITION
            checkPlayerWinCondition();
            checkPlayerLoseCondition();


        }

        Debug.Log("ENEMY TURN HAS ENDED");

        state = BattleState.PLAYERTURN;

        

        playerTurnCount = 1;
        PlayerTurn(playerTurnCount);
    }

    
    void checkPlayerWinCondition()
    {
        int deadEnemies = 0;
        for(int i = 0; i < enemyUnits.Count; i++)
        {
            //CHECK IF ANY ENEMIES ARE STILL ALIVE
            if(!(enemyUnits.ElementAt(i).unitIsDead()))
            {
                return;
            }

            deadEnemies++;
        }

        if(deadEnemies == enemyUnits.Count)
        {
            state = BattleState.WON;
        }

        if (state  == BattleState.WON)
        {
            Debug.Log("THE PLAYER HAS WON");
            Instantiate(winScreen);

            Time.timeScale = 0f;
        }
    }
    
    void checkPlayerLoseCondition()
    {
        int deadPlayers = 0;
        for (int i = 0; i < playerUnits.Count; i++)
        {
            //CHECK IF PARTY CHARACTERS ARE STILL ALIVE
            if (!(playerUnits.ElementAt(i).unitIsDead()))
            {
                return;
            }

            deadPlayers++;
            
        }

        if (deadPlayers == playerUnits.Count)
        {
            state = BattleState.LOST;
        }

        if (state == BattleState.LOST)
        {
            Debug.Log("THE PLAYER HAS LOST");
            Instantiate(loseScreen);

            Time.timeScale = 0f;
        }
    }




}
