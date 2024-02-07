using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected string unitName;

    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] public CombatSkillData[] combatSkills;

    private bool isDead;

    [Header("Events")]
    public GameEvent onHealthChanged;
    public GameEvent onDefeat;

    private void Start()
    {
        isDead = false;

        currentHP = maxHP;
    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }


        if(currentHP <= 0)
        {
            Debug.Log(unitName+" HAS DIED");
            isDead = true;

            //

            gameObject.SetActive(false);
        }
    }

    public string getUnitName()
    {
        return unitName;
    }

    public int getMaxHP()
    {
        return maxHP;
    }

    public int getCurrentHP()
    {
        return currentHP;
    }

    public bool unitIsDead()
    {
        return isDead;
    }

    public void dealDamage(int damage, Unit target)
    {
        currentHP -= damage;

        onHealthChanged.Raise(this, target);
    }

    public void regenHP(int health, Unit target)
    {
        currentHP += health;

        onHealthChanged.Raise(this, target);
    }

}
