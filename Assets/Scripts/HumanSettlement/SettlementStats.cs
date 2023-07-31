using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementStats : Damageable
{
    private HumanSettlement settlement;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int MaxHealth;
    private int currentHealth;

    public void Initialize(HumanSettlement settlement)
    {
        this.settlement = settlement;
        currentHealth = MaxHealth;
    }

    public override void Damage(int amount)
    {
        currentHealth -= amount;
       // AudioManager.Instance.PlayAudio(SoundKey.HitStructure);
        if (currentHealth < 0)
            Death();
    }

    protected virtual void Death()
    {
        AudioManager.Instance.PlayAudio(SoundKey.StructureDestroy);
        Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
        settlement.SettlementDestroyed();
        Destroy(gameObject);
    }
}
