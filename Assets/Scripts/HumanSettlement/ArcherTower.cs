using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour, ISettlement
{
    private Human human;

    public void SettlersSpawned(List<Human> settlers)
    {
        human = settlers[0];
    }

    public void SettlementDestroyed()
    {
        human.Stats.Damage(100);
    }
}