using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent2 : UnityEvent<Component, object, object> { }

public class GameEventListener2 : GameEventListener
{
    public CustomGameEvent2 response2;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaise(Component sender, object data, object data2)
    {
        response2.Invoke(sender, data, data2);
    }
}
