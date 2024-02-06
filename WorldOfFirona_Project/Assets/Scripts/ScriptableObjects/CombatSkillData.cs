using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Magic
}

public enum SkillFunction
{
    Damage,
    Heal,
    RechargeMana,
    Buff,
    Debuff
}

[CreateAssetMenu(menuName = "Combat/Combat Skill")]
public class CombatSkillData : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public SkillFunction skillFunction;
    public int skillValue;
    [Tooltip("This is the skill's Mana requirements\nInput 0 if the skill is not a Magic Skill")]
    public int skillRequirement = 0;

}
