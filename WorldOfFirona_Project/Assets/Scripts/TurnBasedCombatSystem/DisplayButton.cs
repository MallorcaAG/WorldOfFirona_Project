using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        if(myText.text == "")
        {
            gameObject.SetActive(false);
        }
    }
}
