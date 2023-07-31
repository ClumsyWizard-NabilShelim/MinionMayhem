using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMinionModule : AggressiveCharacterModule
{
    protected override void AttackTarget(Damageable target)
    {
        target.Damage(((MinionSO)character.Data).Damage);
    }
}
