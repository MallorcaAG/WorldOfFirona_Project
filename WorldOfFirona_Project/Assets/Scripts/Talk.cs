using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : Interaction
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onPlayerEnterInteractableArea.Raise(this, 1);

            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onPlayerExitInteractableArea.Raise(this, 1);

            interactable = false;
            btnpressed = false;
        }
    }
}
