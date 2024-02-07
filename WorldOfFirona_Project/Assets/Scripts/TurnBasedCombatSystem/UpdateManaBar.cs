using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class UpdateManaBar : MonoBehaviour
{
    [SerializeField] private RectTransform manaSlider;
    [SerializeField] private TextMeshProUGUI myName;


    public void updateSliderDisplay(Component sender, object data)
    {
        PlayerUnit myUnit = (PlayerUnit)data;
        if (myName.text == myUnit.getUnitName())
        {
            int healthPercentage = calculateHealthDisplay(myUnit);
            healthPercentage = healthPercentage > 100 ? 100 : healthPercentage;

            //Debug.Log(healthPercentage + " " + healthSlider.sizeDelta.x + " " + healthSlider.sizeDelta.y);
            manaSlider.sizeDelta = new Vector2(healthPercentage, manaSlider.sizeDelta.y);
        }

    }

    int calculateHealthDisplay(PlayerUnit hp)
    {
        /*Debug.Log(hp.getMaxMP());
        Debug.Log(hp.getCurrentMP());*/


        int max = hp.getMaxMP();
        int cur = hp.getCurrentMP();

        return (int)(((float)cur / (float)max) * 100);
    }
}
