using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPromptDisplay : MonoBehaviour
{
    [Tooltip("0 = General Interaction" + "\n" +
             "1 = Talk Interaction" + "\n" +
             "2 = Quest Interaction")]
    [SerializeField] private GameObject[] interactionUI;

    public void DisplayInteraction(Component sender, object typeOfInteraction)
    {
        /*  NOTE:
         *      0 = General Interaction
         *      1 = Talk Interaction
         *      2 = Quest Interaction
         *      
         *      If there are any more interactions to add in the future, 
         *      just extend these codes
         */
        if((int)typeOfInteraction == 0)
        {
            interactionUI[0].SetActive(true);
        }
        else if ((int)typeOfInteraction == 1)
        {
            interactionUI[1].SetActive(true);
        }
        else if ((int)typeOfInteraction == 2)
        {
            interactionUI[2].SetActive(true);
        }
    }

    public void HideInteraction(Component sender, object typeOfInteraction)
    {
        /*  NOTE:
         *      0 = General Interaction
         *      1 = Talk Interaction
         *      2 = Quest Interaction
         *      
         *      If there are any more interactions to add in the future, 
         *      just extend these codes
         */
        if ((int)typeOfInteraction == 0)
        {
            interactionUI[0].SetActive(false);
        }
        else if ((int)typeOfInteraction == 1)
        {
            interactionUI[1].SetActive(false);
        }
        else if ((int)typeOfInteraction == 2)
        {
            interactionUI[2].SetActive(false);
        }
    }
}
