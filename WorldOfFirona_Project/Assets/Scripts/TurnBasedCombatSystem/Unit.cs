using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected string unitName;

    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    [SerializeField] public CombatSkillData[] combatSkills;
    

    public string getUnitName()
    {
        return unitName;
    }

}
