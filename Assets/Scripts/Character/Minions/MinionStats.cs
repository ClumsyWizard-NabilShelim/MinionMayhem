using System.Collections;
using UnityEngine;

public class MinionStats : CharacterStats
{
    protected override void Death()
    {
        GameManager.Instance.RemoveMinion((Minion)character);
        base.Death();
    }
}