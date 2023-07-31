using System.Collections;
using UnityEngine;

public class HumanArcherModule : AggressiveCharacterModule
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;

    protected override void MoveToTarget()
    {
    }

    protected override void AttackTarget(Damageable damageable)
    {
        AudioManager.Instance.PlayAudio(SoundKey.BowTwang);
        Vector2 diff = damageable.transform.position - shootPoint.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);

        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
    }
}