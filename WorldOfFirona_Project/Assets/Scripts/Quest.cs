using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : Interaction
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onPlayerEnterInteractableArea.Raise(this, 2);

            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onPlayerExitInteractableArea.Raise(this, 2);

            interactable = false;
            btnpressed = false;
        }
    }
}
