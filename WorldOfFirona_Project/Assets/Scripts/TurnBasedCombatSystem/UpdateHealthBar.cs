using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UpdateHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthSlider;
    [SerializeField] private TextMeshProUGUI myName;


    public void updateSliderDisplay(Component sender, object data)
    {
        Unit myUnit = (Unit)data;
        if(myName.text == myUnit.getUnitName())
        {
            int healthPercentage = calculateHealthDisplay(myUnit);
            healthPercentage = healthPercentage > 100 ? 100 : healthPercentage;

            /*Debug.Log(healthPercentage + " " + healthSlider.sizeDelta.x + " " + healthSlider.sizeDelta.y);*/
            healthSlider.sizeDelta = new Vector2(healthPercentage, healthSlider.sizeDelta.y);
        }

    }

    int calculateHealthDisplay(Unit hp)
    {
        /*Debug.Log(hp.getMaxHP());
        Debug.Log(hp.getCurrentHP());*/


        int max = hp.getMaxHP();
        int cur = hp.getCurrentHP();

        return (int)(((float)cur / (float)max) * 100);
    }
}
