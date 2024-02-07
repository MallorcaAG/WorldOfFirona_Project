using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();
    public List<GameEventListener2> listeners2 = new List<GameEventListener2>();


    //Raise event through different methods signatures
    public void Raise(Component sender, object data)
    {
        for(int i  = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaise(sender, data);
        }
    }

    public void Raise(Component sender, object data, object data2)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners2[i].OnEventRaise(sender, data, data2);
        }
    }

    //Manage Listeners
    public void RegisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        if(listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
