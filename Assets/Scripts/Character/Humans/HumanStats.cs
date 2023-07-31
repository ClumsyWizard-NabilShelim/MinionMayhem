using System.Collections;
using UnityEngine;

public class HumanStats : CharacterStats
{
    private int bloodCrystalDrop;

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        bloodCrystalDrop = ((HumanSO)character.Data).BloodCrystalDrop;
    }

    protected override void Death()
    {
        PlayerManager.Instance.AddBloodCrystals(bloodCrystalDrop);
        GameManager.Instance.RemoveHuman((Human)character);
        base.Death();
    }
}