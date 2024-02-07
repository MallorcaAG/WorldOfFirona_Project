using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Button Data")]
public class ButtonData : ScriptableObject
{
    public CombatSkillData skillData;
    public Unit actor;
}
