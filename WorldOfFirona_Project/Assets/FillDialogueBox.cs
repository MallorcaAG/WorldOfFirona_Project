using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillDialogueBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueData diologueData;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI dialogueTxt;

    private void OnEnable()
    {
        nameTxt.text = diologueData.name;
        dialogueTxt.text = diologueData.message;
    }

}
