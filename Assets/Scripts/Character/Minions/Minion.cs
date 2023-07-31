using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class Minion : Character
{
    [SerializeField] private GameObject spawnEffect;

    public override void Initialize(CharacterSO data)
    {
        base.Initialize(data);
        GameManager.Instance.AddMinion(this);
        AudioManager.Instance.PlayAudio(SoundKey.Spawn);

        GameObject effect = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
    }
}