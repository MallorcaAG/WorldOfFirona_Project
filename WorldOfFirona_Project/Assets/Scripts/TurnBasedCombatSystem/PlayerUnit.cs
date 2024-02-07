using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    [Space]
    [SerializeField] private int maxMP;
    [SerializeField] private int currentMP;
    //[SerializeField][Range(1,6)] private int combatTurnOrder;

    [Header("Events")]
    public GameEvent onManaChanged;

    private void Start()
    {
        currentMP = maxMP;
    }

    private void Update()
    {
        if (currentMP > maxMP)
        {
            currentMP = maxMP;
        }

        if (currentMP < 0)
        {
            currentMP = 0;
        }
    }

    public int getMaxMP()
    {
        return maxMP;
    }

    public int getCurrentMP()
    {
        return currentMP;
    }

    public void subtractMP(int mp, PlayerUnit user)
    {
        currentMP -= mp;

        onManaChanged.Raise(this, user);
    }

    public void addMP(int mp, PlayerUnit user)
    {
        currentMP += mp;

        onManaChanged.Raise(this, user);
    }

}
