using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Minion")]
public class MinionSO : CharacterSO
{
    [field: Header("Minion Stats")]
    [field: SerializeField] public int BloodCost { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Sprite Portrait { get; private set; }
}