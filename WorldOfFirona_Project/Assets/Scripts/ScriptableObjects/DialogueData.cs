using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Dialogue")]
public class DialogueData : ScriptableObject
{
    public string NPC_name;
    [TextArea] public string message;
}
