using System.Collections;
using UnityEngine;

public abstract class CharacterStats : Damageable
{
    protected Character character;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject deathEffect;
    private int currentHealth;

    public virtual void Initialize(Character character)
    {
        this.character = character;
        currentHealth = character.Data.Health;
    }

    public override void Damage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
        else if (hitEffect != null)
        {
            AudioManager.Instance.PlayAudio(SoundKey.HitCharacter);
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
        }
    }

    protected virtual void Death()
    {
        AudioManager.Instance.PlayAudio(SoundKey.CharacterDie);
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        CameraShake.Instance.ShakeObject(0.05f, ShakeMagnitude.Small);
        Destroy(gameObject);
    }
}