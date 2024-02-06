using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterPosition : MonoBehaviour
{
    [SerializeField] private int myCharacterNumber;

    public void moveForward(Component sender, object data)
    {
        if (myCharacterNumber != (int)data)
        {
            return;
        }
        transform.Translate(new Vector3(0, 0, 1.5f));
    }

    public void moveBackward(Component sender, object data)
    {
        if (myCharacterNumber != (int)data)
        {
            return;
        }
        transform.Translate(new Vector3(0, 0, -1.5f));
    }
}
