using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HumanSoldierModule : AggressiveCharacterModule
{
    [SerializeField] private int damage;

    protected override void AttackTarget(Damageable target)
    {
        target.Damage(damage);
    }
}
