using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private GameObject myInteraction;

    [Header("Events")]
    public GameEvent onPlayerEnterInteractableArea;
    public GameEvent onPlayerExitInteractableArea;
    

    protected string playerTag = "Player";
    protected bool interactable;
    protected bool btnpressed;

    private void Start()
    {
        interactable = false;
        btnpressed = false;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {

        if(!interactable)
        {
            myInteraction.SetActive(false);
            return;
        }

        if (Input.GetKey(interactionKey) && !btnpressed)
        {
            Debug.Log("Interaction Key was pressed");

            btnpressed = true;

            myInteraction.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            onPlayerEnterInteractableArea.Raise(this, 0);

            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            onPlayerExitInteractableArea.Raise(this, 0);

            interactable = false;
            btnpressed = false;
        }
    }


}
