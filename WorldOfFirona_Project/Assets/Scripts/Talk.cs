using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : Interaction
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && enabled)
        {
            onPlayerEnterInteractableArea.Raise(this, 1);

            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && enabled)
        {
            onPlayerExitInteractableArea.Raise(this, 1);

            interactable = false;
            btnpressed = false;
        }
    }

    private void OnDisable()
    {
        onPlayerExitInteractableArea.Raise(this, 2);

        interactable = false;
        btnpressed = false;
    }
}
