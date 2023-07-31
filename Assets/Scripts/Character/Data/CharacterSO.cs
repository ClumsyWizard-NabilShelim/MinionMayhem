using System.Collections;
using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}