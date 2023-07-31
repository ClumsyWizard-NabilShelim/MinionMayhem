using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Character/Human")]
public class HumanSO : CharacterSO
{
    [field: SerializeField] public int BloodCrystalDrop { get; private set; }
}
